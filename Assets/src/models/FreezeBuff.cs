using System;
using Assets.src.data;
using Assets.src.services;
using Assets.src.utils;

namespace Assets.src.models {
    public class FreezeBuff : IBuff {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        protected FreezeBuffData data;

        protected IUnit unit;

        public void Initialize(BaseBuffData dataParam) {
            data = dataParam as FreezeBuffData;
            CooldownService.AddCooldown(data.duration, null, End, 0, 0.1f);
        }

        private void End() {
            Free();
            OnEnd.TryCall(this);
        }

        public void Apply(IUnit unitParam) {
            unit = unitParam;
            unit.GetUnitData().movementSpeed *= data.movementCoef;
            unit.GetUnitData().attackSpeed *= data.attackSpeedCoef;
        }

        public void Free() {
            unit.GetUnitData().movementSpeed /= data.movementCoef;
            unit.GetUnitData().attackSpeed /= data.attackSpeedCoef;
        }

        public Action<IBuff> OnEnd { get; set; }
    }
}