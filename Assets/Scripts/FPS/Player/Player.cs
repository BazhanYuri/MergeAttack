using UnityEngine;


namespace FPS
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Damagable _damagable;


        public static Player Instance;
        public Damagable Damagable { get => _damagable;}




        private void Start()
        {
            Instance = this;
        }
    }

}
