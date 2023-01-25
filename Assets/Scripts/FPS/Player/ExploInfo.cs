using UnityEngine;


namespace FPS
{
    public class ExploInfo : MonoBehaviour
    {
        [SerializeField] private float _timeToExplode;
        [SerializeField] private float _explosinableRadius;
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        [SerializeField] private ParticleSystem _explosionParticle;


        public float ExplosinableRadius { get => _explosinableRadius;}
        public float TimeToExplode { get => _timeToExplode;}
        public float Damage { get => Random.RandomRange(_minDamage, _maxDamage); }
        public ParticleSystem ExplosionParticle { get => _explosionParticle;}
    }
}
