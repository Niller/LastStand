using Assets.src.battle;
using Assets.src.models;
using Assets.src.signals;
using Assets.src.utils;
using Assets.src.views;
using JetBrains.Annotations;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TryHeroCastSpellCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public ISelectionManager SelectionManager { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        [Inject]
        public OnResetSpellPreparationSignal OnResetSpellPreparationSignal { get; set; }

        public override void Execute() {
            var currentSelectedUnits = SelectionManager.GetSelectedObjects();
            foreach (var selectable in currentSelectedUnits) {
                var hero = selectable.GetView().GetMediator().GetModel<HeroModel>();
                if (hero != null) {
                    if (hero.GetActiveSpellType() == null) {
                        return;
                    }
                    if (hero.GetActiveSpellType() == SpellTypes.TARGET) {
                        var ray = Camera.main.ScreenPointToRay(Position);
                        var colls = Physics.RaycastAll(ray);
                        foreach (var col in colls) {
                            var targetView = col.collider.gameObject.GetComponent<ITargetView>();
                            if (targetView == null)
                                continue;
                            hero.TryCastSpell(targetView.GetMediator().GetModel<BaseTargetModel>());
                            OnResetSpellPreparationSignal.Dispatch();
                            return;
                        }
                    } else {
                        var ray = Camera.main.ScreenPointToRay(Position);
                        RaycastHit hitInfo;
                        if (Physics.Raycast(ray, out hitInfo, 1 << LayerMask.NameToLayer("Terrain"))) {
                            hero.TryCastSpell(new TargetMock(hitInfo.point));
                            OnResetSpellPreparationSignal.Dispatch();
                            return;
                        }
                    }
                }
            }
        }
    }
}