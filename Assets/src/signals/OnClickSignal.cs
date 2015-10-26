using strange.extensions.signal.impl;
using UnityEngine;

namespace Assets.src.signals {
    public class OnClickSignal : Signal<Vector3> {}
    public class OnDragStartSignal : Signal<Vector3> { }
    public class OnDragSignal : Signal<Vector3> { }
    public class OnDragEndSignal : Signal<Vector3[]> { }

    public class DeselectAllSignal : Signal { }

}