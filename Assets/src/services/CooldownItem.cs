﻿using System;
using UnityEngine;
using System.Collections;
using Assets.Common.Extensions;

namespace Assets.src.services {
    public class CooldownItem : ICooldownItem {
        public int Id { get; private set; }
        public float Duration { get; private set; }
        //сколько прошло времени
        public float ElapsedTime { get; private set; }
        public bool Test { get; set; }

        public float TickInterval { get; private set; }

        public Action OnTick { get; private set; }
        public Action OnEnd { get; private set; }


        public CooldownItem(int id, float duration, Action onTick, Action onEnd, float elapsedTime = 0, float tickInterval = 1) {
            this.Id = id;
            this.Duration = duration;

            this.ElapsedTime = elapsedTime;

            TickInterval = tickInterval;

            this.OnEnd = onEnd;
            this.OnTick = onTick;
        }

        public void AddDuration(float delta) {
            Duration += delta;
        }

        public float GetPCT() {
            return ElapsedTime / Duration;
        }

        public float GetTimeLeft() {
            return Duration - ElapsedTime;
        }

        public float GetDuration() {
            return Duration;
        }

        public void Tick() {
            if (Test) {
                Debug.LogError("asds");
            }

            ElapsedTime += TickInterval;
            OnTick.TryCall();

            if (GetPCT() >= 1)
                OnEnd.TryCall();
        }
    }
}