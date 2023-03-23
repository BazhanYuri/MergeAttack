using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Threading.Tasks;


namespace FPS
{
    public class CompletedEnemyConter : MonoBehaviour
    {
        [SerializeField] private RectTransform _counter;
        [SerializeField] private TextMeshProUGUI _textCount;
        [SerializeField] private ExpoCointUI _expoCointUI;


        public async Task SetCountOfKills(int count, int reward)
        {
            int allReward = count * reward;
            Sequence seq = DOTween.Sequence();
            seq.Append(_textCount.DOCounter(0, count, 0.5f).OnUpdate(() => _textCount.text = "X" + _textCount.text));
            seq.AppendInterval(0.3f);
            seq.Append(_textCount.DOText(allReward.ToString(), 0.3f).OnUpdate(() => _textCount.text = "+" + _textCount.text + " C"));
            seq.AppendInterval(0.1f);
            seq.AppendCallback(() => SpawnMoneyEffect(allReward));
            seq.Join(_textCount.DOColor(Color.yellow, 0.1f));

            await seq.AsyncWaitForCompletion();

        }
        private void SpawnMoneyEffect(int allReward)
        {
            Transform coin;
            int countOfCoins = allReward / _expoCointUI.Price;
            for (int i = 0; i < countOfCoins; i++)
            {
                coin = Instantiate(_expoCointUI, _textCount.rectTransform.position, Quaternion.identity).transform;
                coin.transform.parent = _expoCointUI.transform.parent;

                coin.gameObject.SetActive(true);
            }
        }
    }
}

