//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>

[AddComponentMenu("NGUI/Tween/Position")]
public class TweenSlider : UITweener
{
    public float from = 0;
    public float to = 1;

	UISlider mSlider;

    public UISlider cachedSlider { 
        get {
            if (mSlider == null)
                mSlider = gameObject.GetComponent<UISlider>();
            return mSlider; 
        } 
    }

	//public Vector3 position { get { return cachedSlider.localPosition; } set { cachedSlider.localPosition = value; } }

	override protected void OnUpdate (float factor, bool isFinished)  {
        cachedSlider.value = from * (1f - factor) + to * factor;
    }

    /*void OnDisable() {
        if (tweenFactor == 1) {
            Destroy(this);
        }
    }*/

	/// <summary>
	/// Comix0 the tweening operation.
	/// </summary>
    /// 


	static public TweenPosition Begin (GameObject go, float duration, Vector3 pos)
	{
		TweenPosition comp = UITweener.Begin<TweenPosition>(go, duration);
		comp.from = comp.value;
		comp.to = pos;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
}