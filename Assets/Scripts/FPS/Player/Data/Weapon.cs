using UnityEngine;


namespace FPS
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _startShootDelay;
        [SerializeField] private float _shootDelay;

        [SerializeField] private Animator _animator;

        public float StartShootDelay { get => _startShootDelay;}
        public float ShootDelay { get => _shootDelay;}
        public Animator Animator { get => _animator;}
    }
}
