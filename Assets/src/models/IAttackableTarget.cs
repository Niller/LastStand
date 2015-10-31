namespace Assets.src.models {
    public interface IAttackableTarget : ITarget {
        void DoDamage(ITarget target, int damage);
    }
}