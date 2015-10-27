using Assets.src.battle;
using Assets.src.data;
using Assets.src.mediators;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.utils;
using Assets.src.views;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.src.models {
    public class FontainModel : IModel {

        [Inject]
        public ICooldownService CooldownService { get; set; }

        //[Inject(UnitTypes.HERO)]
        //public IPool<GameObject> Pool { get; set; } 

        [Inject]
        public OnCreateUnitSignal CreateUnitSignal { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

        public FontainMediator Mediator { protected get; set; }

        protected ICooldownItem heroRespawnCooldown;

        public void Initialize() {
            BattleManager.RegisterFontaion(this);
        }

        public void SetView(IView view) {
            
        }

        public IView GetView() {
            return null;
        }

        public bool IsDefender { get { return true; } }

        public void StartHeroRespawn() {
            heroRespawnCooldown = CooldownService.AddCooldown(10, null, SpawnHero);
        }

        public void SpawnHero() {
            CreateUnitSignal.Dispatch(Mediator.Informer.spawnPoint.position, UnitTypes.HERO, Mediator.Informer.heroData, true);
        }

        public void StopHeroRespawn() {
            
        }
    }
}