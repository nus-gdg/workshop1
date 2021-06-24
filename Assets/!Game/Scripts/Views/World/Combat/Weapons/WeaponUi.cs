using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Views.Combat
{
    public class WeaponUi : MonoBehaviour
    {
        public Transform Transform { get; private set; }

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private bool canShoot = true;

        [SerializeField]
        private bool autofire;

        [SerializeField]
        public float chargeTime;

        [SerializeField]
        private WeaponEvent onAttack;

        [SerializeField]
        private WeaponEvent onUnleash;

        private Coroutine _cooldown;
        private float _chargeStartTime;

        public Transform FirePoint
        {
            get => firePoint;
            set => firePoint = value;
        }

        public void Awake()
        {
            Transform = transform;
        }

        public void Attack()
        {
            if (canShoot)
            {
                onAttack?.Invoke(this);
            }
        }

        public void Charge()
        {
            if (canShoot)
            {
                _chargeStartTime = Time.time;
            }
        }

        public void Unleash()
        {
            if (Time.time - _chargeStartTime >= chargeTime)
            {
                chargeTime = 0f;
                onUnleash?.Invoke(this);
            }
        }

        public void Autofire()
        {
            if (autofire && canShoot)
            {
                onAttack?.Invoke(this);
            }
        }

        public void Aim(Vector2 target)
        {
            Vector2 lookDir = target - (Vector2)Transform.position;
            float rotateAngle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
            Transform.rotation = Quaternion.Euler(0, 0, -rotateAngle);
        }

        public void Cooldown(float time)
        {
            if (!canShoot)
            {
                StopCoroutine(_cooldown);
            }
            _cooldown = StartCoroutine(ShootDelay(time));
        }

        private IEnumerator ShootDelay(float time)
        {
            canShoot = false;
            yield return new WaitForSeconds(time);
            canShoot = true;
        }

        [Serializable]
        protected class WeaponEvent : UnityEvent<WeaponUi> { }
    }
}
