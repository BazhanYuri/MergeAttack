using UnityEngine;


namespace FPS
{
  
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _shootParticle;

        [SerializeField] private Animator _animator;

        
        public Animator Animator { get => _animator;}
        
        public Transform ShootPoint { get => _shootPoint;}
        public ParticleSystem ShootParticle { get => _shootParticle;}
        public WeaponData WeaponData { get => _weaponData;}
    }
}
