using Assets.src.contexts;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

namespace Assets.src.views {
    public class UnitView : BaseUnitView {

        protected ISelectableBehaviour selectableBehaviour;

        public ISelectableBehaviour SelectableBehaviour { get { return selectableBehaviour = selectableBehaviour ?? GetComponent<ISelectableBehaviour>();} }
      
    }
}