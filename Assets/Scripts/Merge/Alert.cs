using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Merge
{
    public class Alert : MonoBehaviour
    {
        public void ShowAlert()
        {

            StartCoroutine(VisualEffect());
        }

        private IEnumerator VisualEffect()
        {
            transform.DOScale(Vector3.one, 0.1f);
            yield return new WaitForSeconds(1f);
            transform.DOScale(Vector3.one * 0.01f, 0.1f);
        }
    }
}
