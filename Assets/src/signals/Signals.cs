using Assets.src.battle;
using Assets.src.data;
using Assets.src.models;
using Assets.src.utils;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.src.signals {
    public class OnClickSignal : Signal<Vector3> {}
    public class OnAlternativeClickSignal : Signal<Vector3> { }
    public class OnDragStartSignal : Signal<Vector3> { }
    public class OnDragSignal : Signal<Vector3> { }
    public class OnDragEndSignal : Signal<Vector3[]> { }
    public class OnSpellSlotActivated : Signal<int> { }
    public class OnResetSpellPreparationSignal : Signal { }
    
    public class OnPreparationTargetSignal : Signal { }
    public class OnPreparationAreaSignal : Signal<float> { }

    public class OnSpellCast : Signal<Vector3, Spells, ITarget, SpellData> { }

    public class DeselectAllSignal : Signal { }

}