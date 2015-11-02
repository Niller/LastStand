namespace Assets.src.models {
    public class SpellCastQueueItem {

        public SpellCastQueueItem(SpellSlot slotParam, ITarget targetParam) {
            slot = slotParam;
            target = targetParam;
        } 

        public SpellSlot slot;
        public ITarget target;
    }
}