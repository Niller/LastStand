﻿using UnityEngine;
using System.Collections;
using Assets.src.contexts;
using Assets.src.utils;

public interface IGameDataService : IService {
    BulletTypes GetBulletType(UnitTypes uniType);
    SpellTypes GetSpellType(Spells spell);
}
