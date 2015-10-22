using Assets.src.models;
using Assets.src.views;

namespace Assets.src.mediators {
    public class UnitMediator : ViewModelMediator<UnitModel> {
        [Inject]
        public UnitView View { get; set; }
    }
}