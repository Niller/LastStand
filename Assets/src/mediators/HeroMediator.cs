using Assets.src.models;
using Assets.src.views;

namespace Assets.src.mediators {
    public class HeroMediator : BaseUnitMediator<HeroModel> {
        [Inject]
        public HeroView View { get; set; }

        protected override BaseUnitView GetUnitView() {
            return View;
        }
    }
}