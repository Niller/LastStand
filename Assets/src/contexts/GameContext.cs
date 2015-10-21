using Assets.src.mediators;
using Assets.src.model;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Assets.src.services;

namespace Assets.src.contexts {
    public class GameContext : MVCSContext {

        private RootContext rootContext;

        public static GameContext instance;

        public GameContext(MonoBehaviour view) : base(view) {
            rootContext = view as RootContext;
			InitServices();
            
        }


        override public IContext Start() {
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
            
			//signals
            injectionBinder.Bind<OnClickSignal>().ToSingleton();
            injectionBinder.Bind<IMapModel>().To<MapModel>();
            
			//services
            injectionBinder.Bind<IInputService>().To<InputService>().ToSingleton();
			injectionBinder.Bind<IGameDataService>().To<GameDataService>().ToSingleton();
			injectionBinder.Bind<ICooldownService>().To<CooldownService>().ToSingleton();
            //mediators
			mediationBinder.Bind<GameView>().To<GameMediator>();
			mediationBinder.Bind<MapView>().To<MapMediator>();
            mediationBinder.Bind<RoadView>().To<RoadMediator>();
            //singletons
            commandBinder.Bind<OnBuildRoadSignal>().To<BuildRoadCommand>();

        }

		protected void InitServices() {
			rootContext.AddService(injectionBinder.GetInstance<IInputService>());
			rootContext.AddService(injectionBinder.GetInstance<ICooldownService>());
		}

        public void BindRoad() {
            //mediationBinder.Bind<RoadView>().To<RoadMediator>();
        }
    }
}