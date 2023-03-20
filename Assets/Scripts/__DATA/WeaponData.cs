using UnityEngine;

public class WeaponData : MonoBehaviour
{
    [SerializeField] private float _startShootDelay;
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _damage;
    [SerializeField, Range(0, 100)] private int _percentageOfAccuracy;



    public float StartShootDelay { get => _startShootDelay; }
    public float ShootDelay { get => _shootDelay; }
    public float Damage { get => _damage; }
    public int PercentageOfAccuracy { get => _percentageOfAccuracy; }
}
