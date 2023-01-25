using UnityEngine;
using UnityEngine.UI;





namespace FPS
{
    public class EnemiesKilledIndicator : MonoBehaviour
    {
        [SerializeField] private Slider _slider;



       
        private void OnEnable()
        {
            EnemyDead.DeadEnemy += AddOneEnemy;

        }
        private void OnDisable()
        {
            EnemyDead.DeadEnemy -= AddOneEnemy;
        }

        


        public void SetUpSlider(int count)
        {
            _slider.maxValue = count;
        }
        private void AddOneEnemy()
        {
            _slider.value += 1;
        }
    }

}
