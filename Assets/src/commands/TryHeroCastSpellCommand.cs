using Assets.src.battle;
using Assets.src.models;
using Assets.src.signals;
using Assets.src.utils;
using Assets.src.views;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TryHeroCastSpellCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public ISpellCastManager SpellCastManager { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        [Inject]
        public IGameDataService GameDataService { get; set; }

        [Inject]
        public OnResetSpellPreparationSignal OnResetSpellPreparationSignal { get; set; }

        public override void Execute() {
            if (SpellCastManager.IsReadyToCastSpell()) {
                var activeSpell = SpellCastManager.GetActiveSpell();
                if (activeSpell.data.type == SpellTypes.TARGET) {
                    var ray = Camera.main.ScreenPointToRay(Position);
                    var colls = Physics.RaycastAll(ray);
                    foreach (var col in colls) {
                        var targetView = col.collider.gameObject.GetComponent<ITargetView>();
                        if (targetView == null)
                            continue;
                        var target = targetView.GetModel<ITarget>();
                        if (!target.GetTargetBehaviour().IsDefender) {
                            SpellCastManager.CastCurrentActiveSpell(targetView.GetModel<ITarget>());
                            OnResetSpellPreparationSignal.Dispatch();
                        }
                        return;
                    }
                } else {
                    var ray = Camera.main.ScreenPointToRay(Position);
                    RaycastHit hitInfo;
                    if (Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"))) {
                        SpellCastManager.CastCurrentActiveSpell(new TempTarget(hitInfo.point));
                        OnResetSpellPreparationSignal.Dispatch();
                    }
                }
            }
        }
    }
}