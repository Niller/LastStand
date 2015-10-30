namespace Assets.src.models {
    public interface ITarget : IModel {
        ITargetBehaviour GetTargetBehaviour();
    }
}