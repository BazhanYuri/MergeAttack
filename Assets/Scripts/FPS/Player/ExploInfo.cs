using UnityEngine;


namespace FPS
{
    public class ExploInfo : MonoBehaviour
    {
        [SerializeField] private float _timeToExplode;
        [SerializeField] private float _explosinableRadius;
        [SerializeField] private float _startDamage;
        [SerializeField] private float _endDamage;


        public float ExplosinableRadius { get => _explosinableRadius;}
        public float StartDamage { get => _startDamage;}
        public float EndDamage { get => _endDamage;}
        public float TimeToExplode { get => _timeToExplode;}
    }
}
