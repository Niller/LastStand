using UnityEngine;

namespace Assets.External.NGUI.Examples.Scripts.Other {
    [AddComponentMenu("NGUI/Examples/Load Level On Click")]
    public class LoadLevelOnClick : MonoBehaviour
    {
        public string levelName;

        void OnClick ()
        {
            if (!string.IsNullOrEmpty(levelName))
            {
                Application.LoadLevel(levelName);
            }
        }
    }
}