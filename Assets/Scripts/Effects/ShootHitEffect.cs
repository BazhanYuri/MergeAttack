using UnityEngine;



namespace FPS
{
    public enum HitType
    {
        Head,
        Body,
        ProtectedPart,
        Metal
    }
    public class ShootHitEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _effects;

        public void ChooseEffect(HitType hitType)
        {
            switch (hitType)
            {
                case HitType.Head:
                    _effects[0].gameObject.SetActive(true);
                    break;
                case HitType.Body:
                    _effects[0].gameObject.SetActive(true);
                    break;
                case HitType.ProtectedPart:
                    _effects[1].gameObject.SetActive(true);
                    break;
                case HitType.Metal:
                    _effects[2].gameObject.SetActive(true);
                    break;
                default:
                    break;
            }

            Destroy(gameObject, 5);
        }
    }
}

