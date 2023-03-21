using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



namespace FPS
{
    public class GameplayScreen : MonoBehaviour
    {
        [SerializeField] private Image _readyToShootSign;



        public void SetPercentageOfShoot(float percent)
        {
            _readyToShootSign.fillAmount = percent;
        }
    }
}


