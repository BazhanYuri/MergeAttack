using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Merge
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _corners;
        [SerializeField] private Color _choosedColor;

        public event System.Action<bool> Choosed;


        private Color _standartColor;

        public void ChooseItem()
        {
            _corners.color = _choosedColor;
            _corners.pixelsPerUnitMultiplier = 0.3f;
            Choosed?.Invoke(true);
        }
        public void UnchooseItem()
        {
            _corners.color = _standartColor;
            _corners.pixelsPerUnitMultiplier = 0.4f;
            Choosed?.Invoke(false);
        }

        public void HideCanvas()
        {
            _canvas.transform.DORewind();
            _canvas.transform.DOScale(Vector3.zero, 0.15f);
        }
        public void ShowCanvas()
        {
            _canvas.transform.DORewind();
            _canvas.transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 0.15f);
        }


        private void Start()
        {
            _standartColor = _corners.color;
        }
    }
}
