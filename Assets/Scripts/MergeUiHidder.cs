using UnityEngine;
using UnityEngine.UI;

public class MergeUiHidder : MonoBehaviour
{
    [SerializeField] private Button _startButton;




    
    public void HideUI()
    {
        gameObject.SetActive(false);
    }


}