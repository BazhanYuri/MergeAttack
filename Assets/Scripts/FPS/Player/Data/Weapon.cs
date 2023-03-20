using UnityEngine;


namespace FPS
{
    public enum ShootableType
    {
        Pistol,
        Revolver,
        Uzi,
        AK47,
        Machinegun
    }
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ParticleSystem _shootParticle;

        [SerializeField] private Animator _animator;
        [SerializeField] private ShootableType _shootableType;

        
        public Animator Animator { get => _animator;}
        
        public Transform ShootPoint { get => _shootPoint;}
        public ParticleSystem ShootParticle { get => _shootParticle;}
        public ShootableType ShootableType { get => _shootableType;}
        public WeaponData WeaponData { get => _weaponData;}
    }
}
