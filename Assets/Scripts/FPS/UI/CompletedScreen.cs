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





        private void Start()
        {
            _nextLevelButton.onClick.AddListener(GameManager.Instance.NextLevel);
            ShowRewards();
        }
        private void OnDisable()
        {
            _nextLevelButton.onClick.RemoveListener(GameManager.Instance.NextLevel);
        }

        public async void ShowRewards()
        {
            await _soldiersCount.SetCountOfKills(_gameManager.CurrentLevelInfo.SoldierCount, _solider.Reward);
            await _jagersCount.SetCountOfKills(7, _jager.Reward);
            await _dronsCount.SetCountOfKills(15, _dron.Reward);
        }


    }
}

