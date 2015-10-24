using Assets.src.battle;
using Assets.src.commands;
using Assets.src.mediators;
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
using strange.extensions.pool.api;
using strange.extensions.pool.impl;

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

            //signals
            injectionBinder.Bind<OnClickSignal>().ToSingleton();

            commandBinder.Bind<OnCreateUnitSignal>().To<CreateUnitCommand>();
            //pools
            injectionBinder.Bind<IPool<GameObject>>().To<Pool<GameObject>>().ToSingleton().ToName(UnitTypes.ENEMY_MELEE);
            //services
            injectionBinder.Bind<IInputService>().To<InputService>().ToSingleton();
            injectionBinder.Bind<IGameDataService>().To<GameDataService>().ToSingleton();
            injectionBinder.Bind<ICooldownService>().To<CooldownService>().ToSingleton();

            //mediators
            mediationBinder.Bind<SofaView>().To<SofaMediator>();
            mediationBinder.Bind<UnitView>().To<UnitMediator>();
        }

        protected void InitServices() {
            rootContext.AddService(injectionBinder.GetInstance<IInputService>());
            rootContext.AddService(injectionBinder.GetInstance<ICooldownService>());
        }

        protected override void postBindings() {
            
            IPool<GameObject> enemyMeleePool = injectionBinder.GetInstance<IPool<GameObject>>(UnitTypes.ENEMY_MELEE);
            enemyMeleePool.instanceProvider = new ResourceInstanceProvider("prefabs/EnemyMeleeUnit",
                LayerMask.NameToLayer("enemy"));
            enemyMeleePool.inflationType = PoolInflationType.INCREMENT;
            
            injectionBinder.GetInstance<IBattleManager>().Initialize();
            base.postBindings();
        }

        
    }
}