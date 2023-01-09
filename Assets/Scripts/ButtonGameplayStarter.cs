using UnityEngine;


public class ButtonGameplayStarter : MonoBehaviour
{
    [SerializeField] private MergeInfoContainer _mergeInfoContainer;
    [SerializeField] private AnyAnimationStart _animationStart;
    [SerializeField] private MergeUiHidder _mergeUiHidder;



    public void TryStartLevel()
    {
        if (_mergeInfoContainer.IsAllEquiped() == false)
        {
            return;
        }

        _mergeUiHidder.HideUI();
        _animationStart.PlayAnimation();
    }
    
}