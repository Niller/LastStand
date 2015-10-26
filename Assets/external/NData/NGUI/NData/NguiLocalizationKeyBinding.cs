using UnityEngine;

[System.Serializable]
public class NguiLocalizationKeyBinding : NguiTextBinding
{
	private UILocalize _localize;
    bool isAwake = false;
	public override void Awake()
	{
		base.Awake();
		
		_localize = GetComponent<UILocalize>();
        isAwake = true;
	}
	
	protected override void ApplyNewValue (string newValue)
	{
        if (!isAwake)
            Awake();

		_localize.key = newValue;
        _localize.Localize();
#if NGUI_2
		_localize.Localize();
#endif
	}
}
