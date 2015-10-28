using Assets.src.battle;
using Assets.src.mediators;
using Assets.src.services;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class TargetSpellPreparationCommand : Command {
        [Inject]
        public IGameResourcesService GameResourcesService { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        public override void Execute() {
            GameManager.BlockControl();
            Cursor.SetCursor(GameResourcesService.GetTargetCursor(), new Vector2(32f, 32f), CursorMode.Auto);
        }

    }
}