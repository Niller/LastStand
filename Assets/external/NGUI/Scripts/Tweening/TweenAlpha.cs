//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using System.Runtime.Serialization.Formatters;
using UnityEngine;

/// <summary>
/// Tween the object's alpha.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Alpha")]
public class TweenAlpha : UITweener
{
#if UNITY_3_5
	public float from = 1f;
	public float to = 1f;
#else
	[Range(0f, 1f)] public float from = 1f;
	[Range(0f, 1f)] public float to = 1f;
#endif

	UIRect mRect;

    public bool useSpriteRenderer;

	public UIRect cachedRect
	{
		get
		{
			if (mRect == null)
			{
				mRect = GetComponent<UIRect>();
				if (mRect == null) mRect = GetComponentInChildren<UIRect>();
			}
			return mRect;
		}
	}

    SpriteRenderer mCachedSpriteRenderer;

    public SpriteRenderer cachedSpriteRenderer {
        get {
            if (mCachedSpriteRenderer == null) {
                mCachedSpriteRenderer = GetComponent<SpriteRenderer>();
                if (mCachedSpriteRenderer == null) mCachedSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            return mCachedSpriteRenderer;
        }
    }

	[System.Obsolete("Use 'value' instead")]
	public float alpha { get { return this.value; } set { this.value = value; } }

    /// <summary>
    /// Tween's current value.
    /// </summary>

    public float value {
        get {
            if (!useSpriteRenderer) {
                return cachedRect.alpha;
            } else {
                return cachedSpriteRenderer.color.a;
            }
        }
        set {
            if (!useSpriteRenderer) {
                cachedRect.alpha = value;
            } else {
                cachedSpriteRenderer.color = new Color(cachedSpriteRenderer.color.r, cachedSpriteRenderer.color.g, cachedSpriteRenderer.color.b, value);
            }
        }
    }

    /// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

    /// <summary>
    /// Comix0 the tweening operation.
    /// </summary>

    public static TweenAlpha Begin(GameObject go, float duration, float alpha, bool useSpriteRenderer = false) {
        TweenAlpha comp = UITweener.Begin<TweenAlpha>(go, duration);
        comp.useSpriteRenderer = useSpriteRenderer;
        comp.from = comp.value;
        comp.to = alpha;
        
        if (duration <= 0f) {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }

    public override void SetStartToCurrentValue () { from = value; }
	public override void SetEndToCurrentValue () { to = value; }
}
