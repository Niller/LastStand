namespace Assets.src.models {
    public class UnitModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {
        public override bool IsManualControl { get { return false; } }
    }
}