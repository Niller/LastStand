using Assets.src.battle;
using Assets.src.data;
using Assets.src.utils;
using Assets.src.views;
using strange.extensions.command.impl;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.src.commands {
    public class CastSpellCommand : Command {
        [Inject]
        public Vector3 Position { get; set; }

        [Inject]
        public Spells Type { get; set; }

        [Inject]
        public SpellData Data { get; set; }

        [Inject]
        public ITarget Target { get; set; }

        public override void Execute() {
            var currentPool = injectionBinder.GetInstance<IPool<GameObject>>(Type);
            if (currentPool != null) {
                var spellGO = currentPool.GetInstance();
                var spellView = spellGO.GetComponent<SpellView>();
                spellGO.transform.parent = GameObject.Find("game").transform;
                spellView.Initialize(Position, Target, Data);
            }
        }
    }
}