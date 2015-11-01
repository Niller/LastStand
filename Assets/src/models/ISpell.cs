using Assets.src.data;

namespace Assets.src.models {
    public interface ISpell {
        void Apply();
        void Initialize(IAttackableTarget source, ITarget targetParam, SpellData data);
    }
}