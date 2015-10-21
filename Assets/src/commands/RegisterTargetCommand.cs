using Assets.src.battle;
using strange.extensions.command.impl;

namespace Assets.src.commands {
    public class RegisterTargetCommand : Command {

        [Inject]
        public ITarget Target { get; set; }

        [Inject]
        public IBattleManager BattleManager { get; set; }

        public override void Execute() {
            BattleManager.RegisterTarget(Target);
        }
    }
}