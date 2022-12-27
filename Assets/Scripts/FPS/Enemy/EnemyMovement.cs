using UnityEngine;
using UnityEngine.AI;


namespace FPS
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _meshAgent;




        public void SetDestination(Vector3 position)
        {
            _meshAgent.SetDestination(position);
        }

        private void Start()
        {
            SetDestination(Vector3.zero);
        }
    }

}
