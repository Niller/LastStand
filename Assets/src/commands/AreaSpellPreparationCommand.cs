using Assets.src.services;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class AreaSpellPreparationCommand : Command {

        [Inject]
        public IGameResourcesService GameResourcesService { get; set; }

        [Inject]
        public float Radius { get; set; }

        public override void Execute() {
            Cursor.visible = false;
            GameResourcesService.GetAreaSelector().Activate();
        }
    }
}