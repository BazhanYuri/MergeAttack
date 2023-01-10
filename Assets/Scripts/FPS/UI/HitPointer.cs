using UnityEngine;
using DG.Tweening;


namespace FPS 
{
    public class HitPointer : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.DOPunchScale(Vector3.one * 1.4f, 0.2f, 2).OnComplete(() => Destroy(gameObject));
        }

    }
}
