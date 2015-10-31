using Assets.src.data;
using Assets.src.managers;
using Assets.src.models;
using Assets.src.utils;
using strange.extensions.command.impl;
using UnityEngine;

namespace Assets.src.commands {
    public class CreateUnitCommand : Command {
        //[Inject(GameElements.GAME_FIELD)]
        //public GameObject GameField { get; set; }

        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public UnitTypes Type { get; set; }

        [Inject]
        public UnitData Data { get; set; }

        [Inject]
        public bool IsDefender { get; set; }

        [Inject]
        public IViewModelManager ViewModelManager { get; set; }

        public override void Execute() {
            //IPool<GameObject> currentPool = injectionBinder.GetInstance<IPool<GameObject>>(Type);
            //if (currentPool != null) {
            
            // var prefab = ViewModelManager.GetView<UnitModel>((int) Type);
            //var unitGO = Object.Instantiate(prefab);
            //var unitInformer = unitGO.AddComponent<UnitInformer>();

            // unitGO.SetActive(true);
            // unitGO.transform.position = Position;
            // unitGO.transform.parent = GameObject.Find("game").transform;
            //} else {
            //    Debug.LogError("Unit pool isn't exist");  
            //}
        }
    }
}