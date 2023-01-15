using UnityEngine;


namespace FPS
{
    public class PlayerDead : MonoBehaviour
    {
        [SerializeField] private Damagable _damagable;
        [SerializeField] private Rigidbody _cameraRigid;


        private void OnEnable()
        {
            _damagable.Dead += Dead;
        }
        private void OnDisable()
        {
            _damagable.Dead -= Dead;
        }

        private void Dead()
        {
            _cameraRigid.isKinematic = false;
        }
    }
}
