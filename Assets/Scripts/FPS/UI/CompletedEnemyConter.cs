using UnityEngine;
using TMPro;
using DG.Tweening;



namespace FPS
{
    public class CompletedEnemyConter : MonoBehaviour
    {
        [SerializeField] private RectTransform _counter;
        [SerializeField] private TextMeshProUGUI _textCount;



        private void Awake()
        {
            SetCountOfKills(20);
        }
        public void SetCountOfKills(int count)
        {
            _textCount.DOCounter(0, count, 3);
        }
    }
}

