using UnityEngine;


namespace FPS
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float _startShootDelay;
        [SerializeField] private float _shootDelay;
        [SerializeField] private float _damage;
        [SerializeField, Range(0, 100)] private int _percentageOfAccuracy;

        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _shootParticle;

        [SerializeField] private Animator _animator;

        public float StartShootDelay { get => _startShootDelay;}
        public float ShootDelay { get => _shootDelay;}
        public Animator Animator { get => _animator;}
        public float Damage { get => _damage;}
        public int PercentageOfAccuracy { get => _percentageOfAccuracy;}
        public Transform ShootPoint { get => _shootPoint;}
        public ParticleSystem ShootParticle { get => _shootParticle;}
    }
}
