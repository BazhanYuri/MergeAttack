using System.Collections;
using UnityEngine;
using UnityEngine.UI;



namespace FPS
{
    public class CompletedScreen : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private CompletedEnemyConter _soldiersCount;
        [SerializeField] private CompletedEnemyConter _jagersCount;
        [SerializeField] private CompletedEnemyConter _dronsCount;

        [SerializeField] private EnemyInfo _solider;
        [SerializeField] private EnemyInfo _jager;
        [SerializeField] private EnemyInfo _dron;





        private IEnumerator Start()
        {
            _nextLevelButton.onClick.AddListener(GameManager.Instance.NextLevel);
            yield return new WaitForSeconds(1.5f);
            ShowRewards();
        }
        private void OnDisable()
        {
            _nextLevelButton.onClick.RemoveListener(GameManager.Instance.NextLevel);
        }

        public async void ShowRewards()
        {
            await _soldiersCount.SetCountOfKills(_gameManager.CurrentLevelInfo.SoldierCount, _solider.Reward);
            await _jagersCount.SetCountOfKills(_gameManager.CurrentLevelInfo.JahernautsCount, _jager.Reward);
            await _dronsCount.SetCountOfKills(_gameManager.CurrentLevelInfo.DronsCount, _dron.Reward);
        }


    }
}

