namespace Assets.src.models {
    public class HeroModel : BaseUnitModel<DistanceTargetSelector, AllEnemiesTargetProvider> {
        public override bool IsManualControl { get { return true; } }
    }
}