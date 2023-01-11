using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace FPS 
{
    public class HitPointer : MonoBehaviour
    {
        [SerializeField] private Image _hitSign;

        public void ShowPointer(Vector3 worldPosition)
        {
            _hitSign.rectTransform.position = Camera.main.WorldToScreenPoint(worldPosition);

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

    }
}
