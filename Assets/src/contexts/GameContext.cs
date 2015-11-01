using Assets.src.battle;
using Assets.src.commands;
using Assets.src.managers;
using Assets.src.models;
using Assets.src.services;
using Assets.src.signals;
using Assets.src.utils;
using Assets.src.views;
using strange.examples.strangerocks;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.injector.impl;
using strange.extensions.pool.api;
using strange.extensions.pool.impl;
using UnityEditorInternal;

namespace Assets.src.contexts {
    public class GameContext : MVCSContext {

        private RootContext rootContext;

        public static GameContext instance;

        public GameContext(MonoBehaviour view) : base(view) {
            rootContext = view as RootContext;
            InitServices();
        }


        public override IContext Start() {
            instance = this;
            base.Start();

            return this;
        }

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents() {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }


        protected override void mapBindings() {
            
            //singletons
            injectionBinder.Bind<IGameManager>().To<GameManager>().ToSingleton();
            injectionBinder.Bind<IBattleManager>().To<BattleManager>().ToSingleton();
            injectionBinder.Bind<ISelectionManager>().To<SelectionManager>().ToSingleton();
            injectionBinder.Bind<IHUDManager>().To<HUDManager>().ToSingleton();
            injectionBinder.Bind<IViewModelManager>().To<ViewModelManager>().ToSingleton();
            injectionBinder.Bind<IUnitFactory>().To<UnitFactory>().ToSingleton();
            injectionBinder.Bind<ISpellFactory>().To<SpellFactory>().ToSingleton();
            injectionBinder.Bind<ISpellCastManager>().To<SpellCastManager>().ToSingleton();

            //signals
            injectionBinder.Bind<DeselectAllSignal>().ToSingleton();
            injectionBinder.Bind<OnDragStartSignal>().ToSingleton();            
            injectionBinder.Bind<OnDragSignal>().ToSingleton();
            injectionBinder.Bind<OnSpellSlotActivated>().ToSingleton();

            commandBinder.Bind<OnClickSignal>().To<TrySelectUnitCommand>().To<TryHeroCastSpellCommand>();
            commandBinder.Bind<OnDragEndSignal>().To<TrySelectUnitGroupCommand>();
            commandBinder.Bind<OnAlternativeClickSignal>().To<TryManualMoveToPositionCommand>().To<TrySetPriorityTargetCommand>();
            commandBinder.Bind<OnPreparationTargetSignal>().To<ResetSpellPreparationCommand>().To<TargetSpellPreparationCommand>();
            commandBinder.Bind<OnPreparationAreaSignal>().To<ResetSpellPreparationCommand>().To<AreaSpellPreparationCommand>();
            commandBinder.Bind<OnResetSpellPreparationSignal>().To<ResetSpellPreparationCommand>();
            commandBinder.Bind<OnSpellCast>().To<CastSpellCommand>();

            //pools

            //units
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(UnitTypes.ENEMY_MELEE);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(UnitTypes.MINION_MELEE);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(UnitTypes.ENEMY_RANGE);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(UnitTypes.MINION_RANGE);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(UnitTypes.HERO);

            //bullets
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(BulletTypes.MELEE_BULLET);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(BulletTypes.RANGE_BULLET);

            //spells
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(Spells.ICE_BOLT);
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(Spells.METEOR);

            //hud
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(HudTypes.TARGET_POINTER);
            
            
            //services
            injectionBinder.Bind<IInputService>().To<InputService>().ToSingleton();
            injectionBinder.Bind<IGameDataService>().To<GameDataService>().ToSingleton();
            injectionBinder.Bind<ICooldownService>().To<CooldownService>().ToSingleton();
            injectionBinder.Bind<IGameResourcesService>().To<GameResourcesService>().ToSingleton();
        }

        protected void InitServices() {
            rootContext.AddService(injectionBinder.GetInstance<IInputService>());
            rootContext.AddService(injectionBinder.GetInstance<ICooldownService>());
            rootContext.AddService(injectionBinder.GetInstance<IGameDataService>());
            rootContext.AddService(injectionBinder.GetInstance<IGameResourcesService>());
        }

        protected override void postBindings() {
            base.postBindings();
            
            InitPools();

            injectionBinder.GetInstance<IGameManager>().Initialize();
            
        }

        protected void InitPools() {

            //units
            IPool<GameObject> enemyMeleePool = injectionBinder.GetInstance<IPool<GameObject>>(UnitTypes.ENEMY_MELEE);
            enemyMeleePool.instanceProvider = new ResourceInstanceProvider("prefabs/EnemyMeleeUnit",
                LayerMask.NameToLayer("enemy"));
            enemyMeleePool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> minionMeleePool = injectionBinder.GetInstance<IPool<GameObject>>(UnitTypes.MINION_MELEE);
            minionMeleePool.instanceProvider = new ResourceInstanceProvider("prefabs/MinionMeleeUnit",
                LayerMask.NameToLayer("minion"));
            minionMeleePool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> enemyRangePool = injectionBinder.GetInstance<IPool<GameObject>>(UnitTypes.ENEMY_RANGE);
            enemyRangePool.instanceProvider = new ResourceInstanceProvider("prefabs/EnemyRangeUnit",
                LayerMask.NameToLayer("enemy"));
            enemyRangePool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> minionRangePool = injectionBinder.GetInstance<IPool<GameObject>>(UnitTypes.MINION_RANGE);
            minionRangePool.instanceProvider = new ResourceInstanceProvider("prefabs/MinionRangeUnit",
                LayerMask.NameToLayer("minion"));
            minionRangePool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> heroPool = injectionBinder.GetInstance<IPool<GameObject>>(UnitTypes.HERO);
            heroPool.instanceProvider = new ResourceInstanceProvider("prefabs/Hero",
                LayerMask.NameToLayer("minion"));
            heroPool.inflationType = PoolInflationType.INCREMENT;

            //bullets
            IPool<GameObject> bulletMeleePool = injectionBinder.GetInstance<IPool<GameObject>>(BulletTypes.MELEE_BULLET);
            bulletMeleePool.instanceProvider = new ResourceInstanceProvider("prefabs/InstantBullet",
                LayerMask.NameToLayer("bullet"));
            bulletMeleePool.inflationType = PoolInflationType.INCREMENT;

            IPool<GameObject> bulletRangePool = injectionBinder.GetInstance<IPool<GameObject>>(BulletTypes.RANGE_BULLET);
            bulletRangePool.instanceProvider = new ResourceInstanceProvider("prefabs/DirectTargetBullet",
                LayerMask.NameToLayer("bullet"));
            bulletRangePool.inflationType = PoolInflationType.INCREMENT;

            //spells
            IPool<GameObject> iceBoltPool = injectionBinder.GetInstance<IPool<GameObject>>(Spells.ICE_BOLT);
            iceBoltPool.instanceProvider = new ResourceInstanceProvider("prefabs/IceBolt",
                LayerMask.NameToLayer("bullet"));
            iceBoltPool.inflationType = PoolInflationType.INCREMENT;

            InitPool(Spells.METEOR, "prefabs/Meteor");

            //hud
            IPool<GameObject> targetPointerPool = injectionBinder.GetInstance<IPool<GameObject>>(HudTypes.TARGET_POINTER);
            targetPointerPool.instanceProvider = new ResourceInstanceProvider("prefabs/gui/TargetPointer",
                LayerMask.NameToLayer("bullet"));
            targetPointerPool.inflationType = PoolInflationType.INCREMENT;
        }

        protected void InitPool(object name, string prefabPath) {
            IPool<GameObject> spellPool = injectionBinder.GetInstance<IPool<GameObject>>(name);
            spellPool.instanceProvider = new ResourceInstanceProvider(prefabPath,
                LayerMask.NameToLayer("bullet"));
            spellPool.inflationType = PoolInflationType.INCREMENT;
        }

        
    }
}