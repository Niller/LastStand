using Assets.src.battle;
using Assets.src.services;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class ResetSpellPreparationCommand : Command {

        [Inject]
        public IGameResourcesService GameResourcesService { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        public override void Execute() {
            GameManager.UnblockControl();
            Cursor.visible = true;
            Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
            GameResourcesService.GetAreaSelector().Deactive();
        }
    }
}