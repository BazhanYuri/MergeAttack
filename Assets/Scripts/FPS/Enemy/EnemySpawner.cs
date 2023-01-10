using System.Collections;
using UnityEngine;


namespace FPS
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Soldier _soldierPrefab;


        [SerializeField] private SpawnPoints _spawnPoints;





        private void Start()
        {
            GameManager.Instance.GameplayStarted += StartSpawnEnemies;
        }
        private void OnDisable()
        {
            GameManager.Instance.GameplayStarted -= StartSpawnEnemies;
        }


        private void StartSpawnEnemies()
        {
            StartCoroutine(Spawning());
        }    
        private IEnumerator Spawning()
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);
                SpawnEnemy();
            }
        }
        private void SpawnEnemy()
        {
            Instantiate(_soldierPrefab).transform.position = _spawnPoints.GetRandomPoint().position;
        }
    }
}

