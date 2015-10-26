using UnityEngine;

namespace Assets.External.NGUI.Examples.Scripts.Other {
    public class OpenURLOnClick : MonoBehaviour
    {
        void OnClick ()
        {
            UILabel lbl = GetComponent<UILabel>();
		
            if (lbl != null)
            {
                string url = lbl.GetUrlAtPosition(UICamera.lastHit.point);
                if (!string.IsNullOrEmpty(url)) Application.OpenURL(url);
            }
        }
    }
}
