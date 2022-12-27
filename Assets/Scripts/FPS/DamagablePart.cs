using UnityEngine;



namespace FPS
{
    public enum PartType
    {
        Head,
        Body,
        Arm,
        Leg
    }

    public class DamagablePart : MonoBehaviour
    {
        [SerializeField] private Damagable _damagable;
        [SerializeField] private PartType _partType;

        private float _damageIndex;


        public void GetDamage(float damage)
        {
            switch (_partType)
            {
                case PartType.Head:
                    _damageIndex = 2;
                    break;
                case PartType.Body:
                    _damageIndex = 1;
                    break;
                case PartType.Arm:
                    _damageIndex = 0.5f;
                    break;
                case PartType.Leg:
                    _damageIndex = 0.8f;
                    break;
                default:
                    break;
            }

            _damagable.GetDamage(damage * _damageIndex);
            print(damage * _damageIndex);
        }
    }
}

