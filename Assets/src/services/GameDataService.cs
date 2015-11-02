using System.Collections;
using System.Collections.Generic;
using Assets.src.data;
using Assets.src.utils;
using UnityEngine;

namespace Assets.src.services {
    public class GameDataService : IGameDataService {

        private Dictionary<UnitTypes, BulletTypes> bullets = new Dictionary<UnitTypes, BulletTypes>() {
            {UnitTypes.ENEMY_MELEE, BulletTypes.MELEE_BULLET },
            {UnitTypes.MINION_MELEE, BulletTypes.MELEE_BULLET },
            {UnitTypes.ENEMY_RANGE, BulletTypes.RANGE_BULLET },
            {UnitTypes.MINION_RANGE, BulletTypes.RANGE_BULLET },
            {UnitTypes.BOSS, BulletTypes.MELEE_BULLET },
            {UnitTypes.HERO, BulletTypes.MELEE_BULLET }
        };

        
        private Dictionary<Spells, SpellTypes> spellsTypes = new Dictionary<Spells, SpellTypes>() {
            {Spells.ICE_BOLT, SpellTypes.TARGET},
            {Spells.METEOR, SpellTypes.AREA }
        };

        private Dictionary<Spells, string> iconsBySpells = new Dictionary<Spells, string>() {
            {Spells.ICE_BOLT, "icebolt"},
            {Spells.METEOR, "meteor"}
        };

        protected Config config;

        public BulletTypes GetBulletType(UnitTypes uniType) {
            return bullets[uniType];
        }

        /*
        public SpellTypes GetSpellType(Spells spell) {
            return spellsTypes[spell];
        }
        */

        public string GetIconBySpell(Spells spell) {
            return iconsBySpells[spell];
        }

        public Config GetConfig() {
            return config ?? (config = GameObject.FindObjectOfType<Config>());
        }

        public void Initialize() {
            config = GameObject.FindObjectOfType<Config>();
        }

        public void OnUpdate() {
            
        }
    }
}