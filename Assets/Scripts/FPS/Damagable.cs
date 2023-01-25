using System;
using UnityEngine;


namespace FPS
{
    public class Damagable : MonoBehaviour
    {
        [SerializeField] protected float _health;

        public event Action Dead;
        public event Action<float> HealthChanged;


        public void GetDamage(float damage)
        {
            if (_health <= 0)
            {
                return;
            }

            _health -= damage;
            HealthUpdated();
            if (_health <= 0)
            {
                Dead?.Invoke();
            }
        }
        public virtual void HealthUpdated()
        {
            HealthChanged?.Invoke(_health);
        }


    
    }
}

