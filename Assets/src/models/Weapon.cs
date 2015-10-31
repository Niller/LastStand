using Assets.src.battle;
using Assets.src.views;
using strange.extensions.pool.api;
using UnityEngine;

namespace Assets.src.models {
    public class Weapon {

        protected int damage;

        protected IPool<GameObject> bulletPool;

        public Weapon(int damageParam, IPool<GameObject> bulletPoolParam) {
            damage = damageParam;
            bulletPool = bulletPoolParam;
        }

        public void ShootTo(Vector3 from, ITarget to) {
            var bulletGO = bulletPool.GetInstance();
            var view = bulletGO.GetComponent<BulletView>();
            view.Initialize(from, to, DoDamage);
        }

        protected void DoDamage(ITarget target) {
            if (!target.GetTargetBehaviour().IsUnvailableForAttack())
                target.GetTargetBehaviour().SetDamage(damage);
        }
    }
}