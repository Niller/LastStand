using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.src.services {
    public class CooldownService :  ICooldownService {

        private readonly Dictionary<float, Dictionary<int, ICooldownItem>> cooldowns = new Dictionary<float, Dictionary<int, ICooldownItem>>();
        private readonly Dictionary<float, float> lastTickTimes = new Dictionary<float, float>(); 

        private int count;

        private readonly List<ICooldownItem> forRemove = new List<ICooldownItem>(); 

        public int GetUniqueId() {
            return count + 1;
        }

        private void AddCooldown(ICooldownItem cooldown) {
            if (cooldowns.ContainsKey(cooldown.TickInterval)) {
                cooldowns[cooldown.TickInterval].Add(cooldown.Id, cooldown);
            } else {
                cooldowns[cooldown.TickInterval] = new Dictionary<int, ICooldownItem> { {cooldown.Id,cooldown}};
            }

            if (lastTickTimes.ContainsKey(cooldown.TickInterval)) {
                cooldown.AddDuration(Time.time - lastTickTimes[cooldown.TickInterval]);
            } else {
                lastTickTimes.Add(cooldown.TickInterval, Time.time);
            }

            count++;
        }

        public ICooldownItem AddCooldown(float duration, Action onTick, Action onEnd, float elapsedTime = 0, float tickInterval = 1) {
            ICooldownItem item = new CooldownItem(GetUniqueId(), duration, onTick, onEnd, elapsedTime, tickInterval);
            AddCooldown(item);
            return item;
        }

        public void RemoveCooldown(ICooldownItem item) {
            //RemoveCooldown(item.InitSortId);
            if (item == null)
                return;
            if (cooldowns.ContainsKey(item.TickInterval)) {
                cooldowns[item.TickInterval].Remove(item.Id);
                if (cooldowns[item.TickInterval].Count == 0)
                    lastTickTimes.Remove(item.TickInterval);
            }
        }

        public ICooldownItem GetById(int id) {
            foreach (var cooldownPair in cooldowns) {
                if (cooldownPair.Value.ContainsKey(id))
                    return cooldownPair.Value[id];
            }
            return null;
        }

        public void OnUpdate() {
            var listKeys = lastTickTimes.Keys.ToList();
            foreach (var lastTick in listKeys) {
                if (!lastTickTimes.ContainsKey(lastTick))
                    continue;
                
                if (Time.time - lastTickTimes[lastTick] >= lastTick) {
                    Tick(cooldowns[lastTick]);
                    //lastTickTime = Time.time;
                    lastTickTimes[lastTick] = Time.time;
                }    
            }

            foreach (var cooldownItem in forRemove) {
                RemoveCooldown(cooldownItem);
            }
            forRemove.Clear();
            
        }

        void Tick(Dictionary<int, ICooldownItem> cooldownsForTick) {
            var listKeys = cooldownsForTick.Keys.ToList();
            for (int i = listKeys.Count - 1; i >= 0; i--) {
                if (!cooldownsForTick.ContainsKey(listKeys[i]))
                    continue;
                ((CooldownItem)cooldownsForTick[listKeys[i]]).Tick();
                
                //Случается, когда мы на OnEnd в кулдауне делаем, например, очистку всех кулдаунов
                if (!cooldownsForTick.ContainsKey(listKeys[i]))
                    continue;

                if (cooldownsForTick[listKeys[i]].GetPCT() >= 1) {
                    //CooldownsForTick.Remove(listKeys[i]);
                    forRemove.Add(cooldownsForTick[listKeys[i]]);
                }
            }
        }

        public void Clear() {
            lastTickTimes.Clear();
            cooldowns.Clear();
        }
    }
}