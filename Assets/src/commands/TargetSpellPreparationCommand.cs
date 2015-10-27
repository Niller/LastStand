using Assets.src.mediators;
using Assets.src.services;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TargetSpellPreparationCommand : Command {
        [Inject]
        public IGameResourcesService GameResourcesService { get; set; }

        public override void Execute() {
            Cursor.SetCursor(GameResourcesService.GetTargetCursor(), new Vector2(32f, 32f), CursorMode.Auto);
        }

    }
}