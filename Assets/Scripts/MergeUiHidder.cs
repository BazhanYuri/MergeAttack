using UnityEngine;
using UnityEngine.UI;

public class MergeUiHidder : MonoBehaviour
{
    [SerializeField] private Button _startButton;




    private void OnEnable()
    {
        _startButton.onClick.AddListener(HideUI);
    }
    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(HideUI);
    }
    private void HideUI()
    {
        gameObject.SetActive(false);
    }


}