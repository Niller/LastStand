using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.src.services{
    public class CooldownService : ICooldownService {
        
        private int count = 0;

        private readonly Dictionary<int, ICooldownItem> items = new Dictionary<int, ICooldownItem>();
        private readonly Dictionary<int, float> lastTickTimes = new Dictionary<int, float>();

        private float expectedSyncTime = 0;
        private bool isSync = false;
        private readonly DateTime startTime = DateTime.Now;

        public int GetUniqueId() {
            count++;
            return count + 1;
        }


        float GetTime() {
            float rez = (float)(DateTime.Now - startTime).TotalSeconds;
            return rez;
        }

        private void AddCooldown(ICooldownItem cooldown) {
            items.Add(cooldown.Id, cooldown);
            lastTickTimes[cooldown.Id] = GetTime();
        }

        public ICooldownItem AddCooldown(float duration, Action onTick, Action onEnd, float elapsedTime = 0, float tickInterval = 1) {
            ICooldownItem item = new CooldownItem(GetUniqueId(), duration, onTick, onEnd, elapsedTime, tickInterval);
            AddCooldown(item);
            return item;
        }


        public void RemoveCooldown(ICooldownItem item) {
            if (item == null)
                return;

            items.Remove(item.Id);
            lastTickTimes.Remove(item.Id);
        }

        public ICooldownItem GetById(int id) {
            if (items.ContainsKey(id))
                return items[id];
            return null;
        }


        public void UpdateOfTime(float currentTime) {
            var listKeys = items.Keys.ToList();
            for (int i = listKeys.Count - 1; i >= 0; i--) {
                int id = listKeys[i];
                if (lastTickTimes.ContainsKey(id)) {
                    float lastTickTime = lastTickTimes[id];
                    CooldownItem cooldown = (CooldownItem) items[id];

                    float deltaTime = currentTime - lastTickTime;

                    if (deltaTime >= cooldown.TickInterval) {
                        cooldown.Tick(deltaTime);
                        lastTickTimes[id] = currentTime;

                        if (cooldown.CheckEnd()) {
                            RemoveCooldown(cooldown);
                        }
                    }
                }
            }
        }

        public void Clear() {
            items.Clear();
            lastTickTimes.Clear();
        }

        public void Initialize() {
            
        }

        public void OnUpdate() {
            UpdateOfTime(GetTime());
        }
    }
}