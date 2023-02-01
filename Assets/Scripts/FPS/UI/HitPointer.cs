using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace FPS 
{
    public class HitPointer : MonoBehaviour
    {
        [SerializeField] private Image _hitSign;

        public void ShowPointer(Vector3 worldPosition, HitType hitType)
        {
            _hitSign.rectTransform.position = Camera.main.WorldToScreenPoint(worldPosition);

            SetColor(hitType);
            Show();
            _hitSign.transform.DORewind();
            _hitSign.transform.DOPunchScale(Vector3.one * 1.4f, 0.2f, 2).OnComplete(() => Hide());
        }
        private void Show()
        {
            _hitSign.enabled = true;
        }
        private void Hide()
        {
            _hitSign.enabled = false;
        }
        private void SetColor(HitType hitType)
        {
            Color color = Color.magenta;

            switch (hitType)
            {
                case HitType.Head:
                    color = new Color(1, 0, 0, 0.7f);
                    break;
                case HitType.Body:
                    color = new Color(1, 0, 0, 0.7f);
                    break;
                case HitType.ProtectedPart:
                    color = new Color(0.7f, 0.7f, 0.7f, 0.8f);
                    break;
                case HitType.Metal:
                    color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
                    break;
                default:
                    break;
            }

            _hitSign.color = color;
        }

    }
}
