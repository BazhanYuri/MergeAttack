using System;
using UnityEngine;




namespace FPS
{
    public class DisconectedPart : DamagablePart
    {
        [SerializeField] private float _health;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _forceVector;


        public event Action Disconected;

        private bool _isDisconected = false;

        public override void GetDamage(float damage)
        {
            if (_isDisconected == true)
            {
                return;
            }

            base.GetDamage(damage);
            _health -= damage;

            DisconectPart();
        }
        private void DisconectPart()
        {
            if (_health <= 0)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.AddRelativeForce(_forceVector);
                _isDisconected = true;
                _rigidbody.transform.parent = null;
                Disconected?.Invoke();
            }
        }
    }
}

