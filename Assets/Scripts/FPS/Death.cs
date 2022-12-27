using UnityEngine;


namespace FPS
{
    public class Death : MonoBehaviour
    {
        [SerializeField] protected Damagable _damagable;



        private void OnEnable()
        {
            _damagable.Dead += Dead;
        }
        private void OnDisable()
        {
            _damagable.Dead -= Dead;
        }

        public virtual void Dead()
        {

        }
    }
}


