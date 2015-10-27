using Assets.src.models;
using Assets.src.views;
using strange.extensions.injector.api;

namespace Assets.src.mediators {
    public class UnitMediator : BaseUnitMediator<UnitModel> {
        [Inject]
        public UnitView View { get; set; }

        protected override BaseUnitView GetUnitView() {
            return View;
        }
    }
}