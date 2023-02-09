using UnityEngine;


namespace FPS
{
    public class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _pointForFirstSpawning;

        public Transform[] Points { get => _spawnPoints; }



        public Transform GetRandomPoint()
        {
            return Points[Random.RandomRange(0, Points.Length)];
        }
        public Transform PointForFirstSpawning()
        {
            return _pointForFirstSpawning;
        }
    }
}
