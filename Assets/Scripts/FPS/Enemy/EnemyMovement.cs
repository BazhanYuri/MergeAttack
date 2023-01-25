using UnityEngine;
using UnityEngine.AI;


namespace FPS
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _meshAgent;



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

        private void Start()
        {
            SetDestination(new Vector3(0, 0, -12.64f));
        }
    }

}
