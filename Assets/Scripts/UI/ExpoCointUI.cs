using UnityEngine;
using DG.Tweening;


public class ExpoCointUI : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _expoRadius;
    [SerializeField] private float _timeToExpo;
    [SerializeField] private RectTransform _pointTOMove;
    [SerializeField] private int _price;

    public int Price { get => _price; }

    public static event System.Action<int> CoinGotMoney;


    private void Start()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOMove(_rectTransform.position + new Vector3(Random.RandomRange(-_expoRadius, _expoRadius), Random.RandomRange(-_expoRadius, _expoRadius), 0), _timeToExpo));

        sequence.Join(_rectTransform.DOPunchScale(Vector3.one * 1.4f, 0.4f));

        sequence.Append(_rectTransform.DOMove(_pointTOMove.position, Random.Range(0.2f, 0.35f)).OnComplete(() => OnCoinGetMoney()));
    }
    private void OnCoinGetMoney()
    {
        gameObject.SetActive(false);
        CoinGotMoney?.Invoke(_price);
    }
}

