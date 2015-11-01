using JetBrains.Annotations;

namespace Assets.src.models {
    public interface ITarget : IModel {
        ITargetBehaviour GetTargetBehaviour();
    }
}