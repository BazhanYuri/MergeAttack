using System;
using UnityEngine;


namespace FPS
{
    public class Damagable : MonoBehaviour
    {
        [SerializeField] private float _health;

        public event Action Dead;


        public void GetDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Dead?.Invoke();
                print("Dead");
            }
        }
    }
}

