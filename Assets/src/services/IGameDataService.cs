using UnityEngine;
using System.Collections;
using Assets.src.utils;

public interface IGameDataService {
    BulletTypes GetBulletType(UnitTypes uniType);
}
