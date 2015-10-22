using System.Collections.Generic;
using Assets.src.data;
using Assets.src.signals;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.battle {
    public class BattleManager : IBattleManager {

        [Inject]
        public OnCreateUnitSignal CreateUnitSignal { get; set; }

        private List<ITarget> attackers;

        private List<ITarget> defenders;

        public BattleManager() {
            attackers = new List<ITarget>();
            defenders = new List<ITarget>();
            //Initialize();
        }

        private List<ITarget> GetAppropriateListForTarget(ITarget target) {
            return target.IsDefender ? defenders : attackers;
        }

        public void Initialize() {
            CreateUnitSignal.Dispatch(new Vector3(50,0,50), UnitTypes.ENEMY_MELEE, new UnitData(), false);
        }

        public void RegisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Add(target);
        }

        public void UnregisterTarget(ITarget target) {
            GetAppropriateListForTarget(target).Remove(target);
        }
    }
}