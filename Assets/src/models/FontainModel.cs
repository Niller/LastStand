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

        [Inject]
        public IGameDataService GameDataService { get; set; }

        protected ICooldownItem heroRespawnCooldown;

        protected FontainView fontainView;

        protected ICooldownItem fontainHealCooldown;

        [PostConstruct]
        public void Initialize() {
            BattleManager.SaveCurrentHeroData(fontainView.hero);
            BattleManager.RegisterFontaion(this);
            fontainHealCooldown = CooldownService.AddCooldown(0.5f, null, Heal, 0, 0.1f);
        }

        private void Heal() {
            var colls = Physics.OverlapSphere(fontainView.GetPosition(), fontainView.fontainHealRadius);
            foreach (var collider in colls) {
                var targetView = collider.GetComponent<ITargetView>();
                if (targetView != null) {
                    var targetModel = targetView.GetModel<ITarget>();
                    if (targetModel.GetTargetBehaviour().IsDefender) {
                        targetModel.GetTargetBehaviour().SetDamage(-fontainView.fontainHealPower);
                    }
                }
            }
            fontainHealCooldown = CooldownService.AddCooldown(0.5f, null, Heal, 0, 0.1f);
        }

        public bool CheckInFontaionRadius(Vector3 position) {
            return Vector3.Distance(position, fontainView.GetPosition()) <= fontainView.fontainHealRadius;
        }

        public void SetView(IView view) {
            fontainView = view as FontainView;;
        }

        public IView GetView() {
            return fontainView;
        }

        public bool IsDefender { get { return true; } }

        public void StartHeroRespawn() {
            heroRespawnCooldown = CooldownService.AddCooldown(GameDataService.GetConfig().heroRespawnTime, null, SpawnHero);
        }

        public void SpawnHero() {
            //UnitFactory.CreateUnit(fontainView.spawnPoint.position, UnitTypes.HERO, BattleManager.GetCurrentHeroData().Copy(), true);
            BattleManager.OnHeroRespawned();
        }

        public ICooldownItem GetHeroRespawnCooldown() {
            return heroRespawnCooldown;
        }

        public void StopHeroRespawn() {
            
        }
    }
}