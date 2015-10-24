using Assets.src.data;
using Assets.src.mediators;
using Assets.src.utils;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
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

           
        /*
        [Inject(UnitTypes.ENEMY_MELEE)]
        public IPool<GameObject> EnemyMeleePool { get; set; }

        protected IPool<GameObject> currentPool;
        */

        public override void Execute() {
            
            IPool<GameObject> currentPool = injectionBinder.GetInstance<IPool<GameObject>>(Type);
            if (currentPool != null) {
                GameObject unitGO = currentPool.GetInstance();
                var unitInformer = unitGO.AddComponent<UnitInformer>();
                unitInformer.data = Data;
                unitInformer.isDefender = IsDefender;
                unitGO.SetActive(true);
                unitGO.transform.position = Position;
                //unitGO.transform.parent =
            } else {
                Debug.LogError("Unit pool isn't exist");  
            }
           
        }

        /*
        private void InitPool() {
            switch (Type) {
                case UnitTypes.ENEMY_MELEE:
                    currentPool = EnemyMeleePool;
                    break;
            }
        }
        */
    }
}