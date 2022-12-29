using UnityEngine;
public class AnyAnimationStart : MonoBehaviour
{
    [SerializeField] private string _trigger;
    [SerializeField] private Animator _animator;



    public void PlayAnimation()
    {
        _animator.SetTrigger(_trigger);
    }


}