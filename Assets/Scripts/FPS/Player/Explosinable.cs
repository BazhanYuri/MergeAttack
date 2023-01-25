using System.Collections;
using UnityEngine;




namespace FPS
{
    public class Explosinable : MonoBehaviour
    {
        [SerializeField] private ExploInfo[] _exploInfos;

        [SerializeField] private Rigidbody _rigidbody;


        private ExploInfo _currentInfo;
        


        public Rigidbody Rigidbody { get => _rigidbody; }


        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(StartExpo());
        }
        


        public void SetUpExplo(int index)
        {
            _currentInfo = _exploInfos[index];
            _currentInfo.gameObject.SetActive(true);
        }

        private IEnumerator StartExpo()
        {
            yield return new WaitForSeconds(_currentInfo.TimeToExplode);
            Explode();
        }
        private void Explode()
        { 
            Instantiate(_currentInfo.ExplosionParticle).transform.position = transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _currentInfo.ExplosinableRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out DamagablePart damagable))
                {
                    damagable.GetDamage(_currentInfo.Damage);
                }
            }

            Destroy(gameObject);
        }
    }
}
