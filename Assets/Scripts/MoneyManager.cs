using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField] private TextMeshProUGUI _moneyCountText;

    private int _moneyCount = 100_000_0;

    public int MoneyCount { get => _moneyCount;}

    private void Awake()
    {
        Instance = this;
        UpdateMoney();
    }

    public void AddMoney(int count)
    {
        _moneyCount += count;

        UpdateMoney();
    }
    public void TakeMoney(int count)
    {
        _moneyCount -= count;

        UpdateMoney();
    }
    private void UpdateMoney()
    {
        _moneyCountText.text = _moneyCount + " $";
    }
}