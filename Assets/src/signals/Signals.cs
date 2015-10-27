using Assets.src.data;
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

    public class DeselectAllSignal : Signal { }
    public class OnCreateUnitSignal : Signal<Vector3, UnitTypes, UnitData, bool> { }

}