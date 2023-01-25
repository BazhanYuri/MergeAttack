using UnityEngine;


namespace FPS
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Death death;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyInfo _enemyInfo;

        [SerializeField] private EnemyType _enemyType;

        public EnemyMovement EnemyMovement { get => _enemyMovement; }
        public virtual Death Death { get => death;}
        public EnemyInfo EnemyInfo { get => _enemyInfo;}
        public EnemyType EnemyType { get => _enemyType;}
    }

}
