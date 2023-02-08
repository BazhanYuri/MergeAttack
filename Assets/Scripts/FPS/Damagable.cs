using System;
using UnityEngine;


namespace FPS
{
    public class Damagable : MonoBehaviour
    {
        [SerializeField] protected float _health;

        public event Action Dead;
        public event Action<float> HealthChanged;

        protected float _currentHealth;

        public void GetDamage(float damage)
        {
            if (_currentHealth <= 0)
            {
                return;
            }

            _currentHealth -= damage;
            HealthUpdated();
            if (_currentHealth <= 0)
            {
                Dead?.Invoke();
            }
        }
        public virtual void HealthUpdated()
        {
            HealthChanged?.Invoke(_currentHealth);
        }

        private void Awake()
        {
            _currentHealth = _health;
        }

    }
}

