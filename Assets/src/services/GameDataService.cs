using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.src.utils;

public class GameDataService : IGameDataService {

    private Dictionary<UnitTypes, BulletTypes> bullets = new Dictionary<UnitTypes, BulletTypes>() {
        {UnitTypes.ENEMY_MELEE, BulletTypes.MELEE_BULLET },
        {UnitTypes.MINION_MELEE, BulletTypes.MELEE_BULLET },
        {UnitTypes.ENEMY_RANGE, BulletTypes.RANGE_BULLET },
        {UnitTypes.MINION_RANGE, BulletTypes.RANGE_BULLET },
        {UnitTypes.HERO, BulletTypes.MELEE_BULLET }
    };

    public BulletTypes GetBulletType(UnitTypes uniType) {
        return bullets[uniType];
    }
}
