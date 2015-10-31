using Assets.src.battle;
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

        [Inject]
        public IBattleManager BattleManager { get; set; }

        [Inject]
        public IUnitFactory UnitFactory { get; set; }

        protected ICooldownItem heroRespawnCooldown;

        protected FontainView fontainView;

        [PostConstruct]
        public void Initialize() {
            BattleManager.RegisterFontaion(this);
            BattleManager.SaveCurrentHeroData(fontainView.hero);
        }

        public void SetView(IView view) {
            fontainView = view as FontainView;;
        }

        public IView GetView() {
            return fontainView;
        }

        public bool IsDefender { get { return true; } }

        public void StartHeroRespawn() {
            heroRespawnCooldown = CooldownService.AddCooldown(10, null, SpawnHero);
        }

        public void SpawnHero() {
            UnitFactory.CreateUnit(fontainView.spawnPoint.position, UnitTypes.HERO, BattleManager.GetCurrentHeroData(), true);
        }

        public void StopHeroRespawn() {
            
        }
    }
}