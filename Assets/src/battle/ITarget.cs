namespace Assets.src.battle {
    public interface ITarget {
        void SetDamage(int damage);
        bool IsDefender { get; }
    }
}
