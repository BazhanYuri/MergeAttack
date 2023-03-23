using UnityEngine;



[CreateAssetMenu(fileName = "EnemyInfo", menuName = "ScriptableObjects/EnemyInfo", order = 1)]
public class EnemyInfo : ScriptableObject
{
    public float DamagePerHit;
    public int Reward;

    public int TimeToStartShoot;
    public int MinTimeToMove;
    public int MaxTimeToMove;
               
    public int MinCountOfShoot;
    public int MaxCountOfShoot;
    public float MinDelayOfShoot;
    public float MaxDelayOfShoot;

    public Transform BulletPrefab;

}
