using UnityEngine;




namespace FPS
{
    public class LevelVisual : MonoBehaviour
    {
        [SerializeField] private SpawnPoints _soldierSpawnPoints;
        [SerializeField] private SpawnPoints _copterSpawnPoints;

        public SpawnPoints SoldierSpawnPoints { get => _soldierSpawnPoints;}
        public SpawnPoints CopterSpawnPoints { get => _copterSpawnPoints;}
    }
}

