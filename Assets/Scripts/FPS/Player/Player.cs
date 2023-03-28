using UnityEngine;


namespace FPS
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Damagable _damagable;
        [SerializeField] private Hidden _hidden;


        public static Player Instance;
        public Damagable Damagable { get => _damagable;}



        private void OnDisable()
        {
            _hidden.UnSubscribeEvents();
        }
        private void Start()
        {
            Instance = this;
            _hidden.Init(transform);
            _hidden.SubscripeEvents();
        }
    }

}
