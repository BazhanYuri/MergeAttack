using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace FPS
{
    public class DronExplosines : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionParticle;
        [SerializeField] private float _damage;
        [SerializeField] private float _radius;
        [SerializeField] private float _timeToExplode;




        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(StartExpo());
        }


        private IEnumerator StartExpo()
        {
            yield return new WaitForSeconds(_timeToExplode);
            Explode();
        }
        private void Explode()
        {
            Instantiate(_explosionParticle).transform.position = transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out DamagablePart damagable))
                {
                    damagable.GetDamage(_damage);
                }
            }
            Destroy(transform.parent.parent.gameObject);
        }
    }
}

