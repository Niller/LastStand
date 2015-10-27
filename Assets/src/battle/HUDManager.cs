using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using Assets.src.utils;
using strange.extensions.injector.api;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.src.battle {
    public class HUDManager : IHUDManager {

        public class HudInfo {
            public HudInfo(BaseHUD h) {
                hud = h;
                count = 1;
            }
            public BaseHUD hud;
            public int count;
        }

        [Inject]
        public IInjectionBinder InjectionBinder { get; set; }
        
        protected Dictionary<GameObject, Dictionary<HudTypes, HudInfo>> currentHUDs = new Dictionary<GameObject, Dictionary<HudTypes, HudInfo>>(); 

        public void AddHUD(GameObject go, HudTypes type) {

            if (currentHUDs.ContainsKey(go)) {
                if (currentHUDs[go].ContainsKey(type)) {
                    currentHUDs[go][type].count++;
                    return;
                }
            }

            IPool<GameObject> currentPool = InjectionBinder.GetInstance<IPool<GameObject>>(type);
            
            if (currentPool != null) {
                var hudGO = currentPool.GetInstance(); 
                UICamera cam = UICamera.FindCameraForLayer(hudGO.layer);

                if (cam != null) {
                    GameObject camGO = cam.gameObject;
                    UIAnchor anchor = camGO.GetComponent<UIAnchor>();
                    if (anchor != null) camGO = anchor.gameObject;

                    
                    Transform t = hudGO.transform;
                    t.parent = camGO.transform;
                    t.localPosition = Vector3.zero;
                    t.localRotation = Quaternion.identity;
                    t.localScale = Vector3.one;

                    BaseHUD hud = hudGO.GetComponent<BaseHUD>();
                    if (hud != null) hud.target = go.transform;
                    if (!currentHUDs.ContainsKey(go)) {
                        currentHUDs.Add(go, new Dictionary<HudTypes, HudInfo>());
                        currentHUDs[go].Add(type, new HudInfo(hud));
                    }
                    else {
                        currentHUDs[go].Add(type, new HudInfo(hud));
                    }
                }
                else {
                    Debug.LogWarning("No camera found for layer " + LayerMask.LayerToName(hudGO.layer), go);
                }
            }
        }

        public void RemoveHUD(GameObject go, HudTypes type) {
            if (currentHUDs.ContainsKey(go)) {
                if (currentHUDs[go].ContainsKey(type)) {
                    if (currentHUDs[go].ContainsKey(type)) {
                        currentHUDs[go][type].count--;
                        if (currentHUDs[go][type].count <= 0) {
                            GameObject.Destroy(currentHUDs[go][type].hud.gameObject);
                            currentHUDs[go].Remove(type);
                        }
                        return;
                    }
                    
                }
            }
            Debug.LogError("Try to remove HUD, which not exist");
        }
    }
}