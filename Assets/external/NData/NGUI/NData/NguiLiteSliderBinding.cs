using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class NguiLiteSliderBinding : NguiBinding {
    private string Format = "{0}";
    virtual protected string LocalizedFormat {
        get {
            return NguiUtils.LocalizeFormat(Format);
        }
    }

    private readonly Dictionary<Type, EZData.Property> _properties = new Dictionary<Type, EZData.Property>();

    private bool _ignoreChanges = false;
    private string _prevFrameInputText = "";

    private UISlider _UiSliderReceiver;

    [HideInInspector]
    public delegate void OnValueChangeDelegate(string newValue);

    [HideInInspector]
    public event OnValueChangeDelegate OnValueChange;

    public override void Awake() {
        base.Awake();

        _UiSliderReceiver = gameObject.GetComponent<UISlider>();
    }

    protected override void Unbind() {
        base.Unbind();

        foreach (var p in _properties) {
            p.Value.OnChange -= OnChange;
        }
        _properties.Clear();
    }

    protected override void Bind() {
        base.Bind();

        FillTextProperties(_properties, Path);

        foreach (var p in _properties) {
            p.Value.OnChange += OnChange;
        }
    }

    public void SetValue(string newValue) {
        SetTextValue(_properties, newValue);

    }

    /*
    void Update() {
        if (_UiInputReceiver != null) {
#if NGUI_2
			var text = _UiInputReceiver.text;
#else
            var text = _UiInputReceiver.value;
#endif
            if (text != _prevFrameInputText) {
                _prevFrameInputText = text;
                _ignoreChanges = true;
                SetValue(text);
                _ignoreChanges = false;
            }
        }
    }
    */

    protected virtual object GetRawValue() {
        return GetTextValue(_properties);
    }

    protected virtual string GetValue() {
        var newValue = string.Format(LocalizedFormat, GetRawValue());
        return newValue; 
    }

    protected override void OnChange() {
        base.OnChange();

        if (_ignoreChanges)
            return;

        var newValue = GetValue();

        if (OnValueChange != null) {
            OnValueChange(newValue);
        }

        ApplyNewValue(newValue);
    }

    protected virtual void ApplyNewValue(string newValue) {
        float newVal = 0;
        if (float.TryParse(newValue, out newVal)) {
            
        } else {
            newVal = 0;    
        }
        

        if (_UiSliderReceiver != null) {
#if NGUI_2
			_UiSliderReceiver.sliderValue = newVal;
#else
            //Debug.LogError(newValue);
            _UiSliderReceiver.value = newVal;
#endif
        }
    }
}
