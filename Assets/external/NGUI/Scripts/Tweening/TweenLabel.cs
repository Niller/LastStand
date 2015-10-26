//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>

[AddComponentMenu("NGUI/Tween/Position")]
public class TweenLabel : UITweener
{
    public int from = 0;
    public int to = 100;

	UILabel mLabel;

    public UILabel cachedLabel { 
        get {
            if (mLabel == null)
                mLabel = gameObject.GetComponent<UILabel>();
            return mLabel; 
        } 
    }

	//public Vector3 position { get { return cachedSlider.localPosition; } set { cachedSlider.localPosition = value; } }

	override protected void OnUpdate (float factor, bool isFinished)  {
        cachedLabel.text = ((int)(from * (1f - factor) + to * factor)).ToString();
        //.sliderValue = from * (1f - factor) + to * factor;
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

    /*
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
    */
}