using System.Collections;
using UnityEngine;


namespace FPS
{
    public enum EnemyType
    {
        Soldier,
        Jagernaut,
        Dron,
        Copter
    }
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private LevelsInfo _levelsInfo;
        [SerializeField] private Soldier _soldierPrefab;
        [SerializeField] private Soldier _jagernautPrefab;

        [SerializeField] private Enemy _dronPrefab;


        [SerializeField] private SpawnPoints _spawnPoints;



        private int _remainingSoldiers;
        private int _remainingJahegnauts;
        private int _remainingDrons;
        private int _remainingCopters;


        private float _minTimeToSpawn;
        private float _maxTimeToSpawn;


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
            SetRemainings();

            StartCoroutine(Spawning());
        }    
        private IEnumerator Spawning()
        {
            while (true)
            {
                EnemyType type = EnemyType.Soldier;
                bool isChoosed = false;

                if (CheckIsAllSpawned() == false)
                {
                    while (isChoosed == false)
                    {
                        type = (EnemyType)Random.Range(0, 3);

                        if (CheckIsThereIsPool(type) == true)
                        {
                            isChoosed = true;
                        }
                    }

                    yield return new WaitForSeconds(Random.Range(_minTimeToSpawn, _maxTimeToSpawn));
                    SpawnEnemy(type);
                }
                else
                {

                    StopAllCoroutines();
                    yield return null;
                }

            }
        }
        private void SpawnEnemy(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Soldier:
                    SpawnSoldier();
                    break;
                case EnemyType.Jagernaut:
                    SpawnJagernaut();
                    break;
                case EnemyType.Dron:
                    SpawnDron();
                    break;
                case EnemyType.Copter:
                    break;
                default:
                    break;
            }
        }
        private void SpawnSoldier()
        {
            Instantiate(_soldierPrefab).transform.position = _spawnPoints.GetRandomPoint().position;
            _remainingSoldiers--;
        }
        private void SpawnJagernaut()
        {
            Instantiate(_jagernautPrefab).transform.position = _spawnPoints.GetRandomPoint().position;
            _remainingJahegnauts--;
        }
        private void SpawnDron()
        {
            Instantiate(_dronPrefab).transform.position = _spawnPoints.GetRandomPoint().position;
            _remainingDrons--;
        }

        private void SetRemainings()
        {
            LevelInfo info = _levelsInfo.LevelInfos[GameManager.Instance.CurrentLevel];

            _remainingSoldiers = info.SoldierCount;
            _remainingJahegnauts = info.JahernautsCount;
            _remainingDrons = info.DronsCount;
            _remainingCopters = info.CoptersCount;

            GameManager.Instance.SetCountOfAllEnemies(_remainingSoldiers + _remainingJahegnauts + _remainingDrons + _remainingCopters);

            _minTimeToSpawn = info.MinTimeToSpawn;
            _maxTimeToSpawn = info.MaxTimeToSpawn;
        }

        private bool CheckIsThereIsPool(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Soldier:
                    return _remainingSoldiers > 0;
                    break;
                case EnemyType.Jagernaut:
                    return _remainingJahegnauts > 0;
                    break;
                case EnemyType.Dron:
                    return _remainingDrons > 0;
                    break;
                case EnemyType.Copter:
                    return _remainingCopters > 0;
                    break;
                default:
                    return true;
                    break;
            }
        }
        private bool CheckIsAllSpawned()
        {
            return _remainingSoldiers <= 0 && _remainingJahegnauts <= 0 && _remainingDrons <= 0 && _remainingCopters <= 0;
        }
    }
}

