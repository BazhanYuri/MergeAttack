using UnityEngine;


namespace FPS
{
    public class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        public Transform[] Points { get => _spawnPoints; }



        public Transform GetRandomPoint()
        {
            return Points[Random.RandomRange(0, Points.Length)];
        }
    }
}
