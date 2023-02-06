using System.Collections;
using UnityEngine;




namespace FPS
{
    public enum ExpoType
    {
        RGD,
        F1,
        Dynamit
    }
    public class Explosinable : MonoBehaviour
    {
        [SerializeField] private ExploInfo[] _exploInfos;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private ExplosinableSound _explosinableSound;

        private ExploInfo _currentInfo;
        private ExpoType _expoType;

        private bool _isExploded = false;

        public Rigidbody Rigidbody { get => _rigidbody; }


        private void OnCollisionEnter(Collision collision)
        {
            if (_isExploded == true)
            {
                return;
            }
            StartCoroutine(StartExpo());
        }
        


        public void SetUpExplo(int index)
        {
            _currentInfo = _exploInfos[index];
            _currentInfo.gameObject.SetActive(true);

            _expoType = (ExpoType)(index);
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
            _explosinableSound.PlayExplosionableSound(_expoType);
            transform.GetChild(0).gameObject.SetActive(false);
            _isExploded = true;
            Destroy(gameObject, 5);
        }
    }
}
