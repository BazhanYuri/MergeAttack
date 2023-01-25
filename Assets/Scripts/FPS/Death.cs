using System;
using UnityEngine;


namespace FPS
{
    public class Death : MonoBehaviour
    {
        [SerializeField] protected Damagable _damagable;

        public event Action Dead;
        public bool IsDead { get; private set; }


        private void OnEnable()
        {
            _damagable.Dead += OnDead;
        }
        private void OnDisable()
        {
            _damagable.Dead -= OnDead;
        }

        public virtual void OnDead()
        {
            IsDead = true;
            Dead?.Invoke();
        }
    }
}


