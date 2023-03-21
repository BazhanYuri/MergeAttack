using UnityEngine;


public enum ShootableType
{
    Pistol,
    Revolver,
    Uzi,
    AK47,
    Machinegun
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] private float _startShootDelay;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _damage;
    [SerializeField, Range(0, 100)] private int _percentageOfAccuracy;
    [SerializeField] private float _indexOfAmmo;
    [SerializeField] private ShootableType _shootableType;



    public float StartShootDelay { get => _startShootDelay; }
    public float ShootDelay { get => _shootDelay; }
    public float Damage { get => _damage; }
    public int PercentageOfAccuracy { get => _percentageOfAccuracy; }
    public ShootableType ShootableType { get => _shootableType; }
    public float IndexOfAmmo { get => _indexOfAmmo;}
}
