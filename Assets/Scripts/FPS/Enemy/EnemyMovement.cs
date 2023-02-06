using UnityEngine;
using UnityEngine.AI;


namespace FPS
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyDead _enemyDead;
        [SerializeField] private NavMeshAgent _meshAgent;



        private void OnEnable()
        {
            _enemyDead.Dead += OnDead;
        }
        private void OnDisable()
        {
            _enemyDead.Dead -= OnDead;
        }

        public void StopMoving()
        {
            _meshAgent.isStopped = true;
        }
        public void StartMoving()
        {
            _meshAgent.isStopped = false;
        }
        public void SetDestination(Vector3 position)
        {
            _meshAgent.SetDestination(position);
        }

        protected virtual void Start()
        {
            SetDestination(new Vector3(0, 0, -12.64f));
        }
        protected virtual void OnDead()
        {
            StopMoving();
            StopAllCoroutines();
        }
    }

}
