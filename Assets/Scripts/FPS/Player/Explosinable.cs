using System.Collections;
using UnityEngine;




namespace FPS
{
    public class Explosinable : MonoBehaviour
    {
        [SerializeField] private ExploInfo[] _exploInfos;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particle;


        private int _currentIndex;


        public Rigidbody Rigidbody { get => _rigidbody; }


        private void OnCollisionEnter(Collision collision)
        {
            StartCoroutine(StartExpo());
        }
        


        public void SetUpExplo(int index)
        {
            _currentIndex = index;
            _exploInfos[_currentIndex].gameObject.SetActive(true);
        }

        private IEnumerator StartExpo()
        {
            yield return new WaitForSeconds(_exploInfos[_currentIndex].TimeToExplode);
            Explode();
        }
        private void Explode()
        {
            Instantiate(_particle).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
