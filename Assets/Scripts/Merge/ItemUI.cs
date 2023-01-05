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


        private Color _standartColor;

        public void ChooseItem()
        {
            _corners.color = _choosedColor;
        }
        public void UnchooseItem()
        {
            _corners.color = _standartColor;
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
