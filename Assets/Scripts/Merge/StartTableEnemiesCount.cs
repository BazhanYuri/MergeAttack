using UnityEngine;
using TMPro;



namespace Merge
{
    public class StartTableEnemiesCount : MonoBehaviour
    {
        [SerializeField] private LevelsInfo _levelsInfo;

        [SerializeField] private TextMeshProUGUI _soldierCount;
        [SerializeField] private TextMeshProUGUI _jagernautCount;
        [SerializeField] private TextMeshProUGUI _dronCount;
        [SerializeField] private TextMeshProUGUI _copterCount;


        private LevelInfo _currentLevel;




        private void Start()
        {
            _currentLevel = _levelsInfo.LevelInfos[GameManager.Instance.CurrentLevel];
            CheckCounts();
        }
        private void CheckCounts()
        {
            CheckIsThereSoldiers();
            CheckIsThereJagernauts();
            CheckIsThereDrons();
            CheckIsThereCopters();
        }
        private void CheckIsThereSoldiers()
        {
            CheckCount(_soldierCount, _currentLevel.SoldierCount);
        }
        private void CheckIsThereJagernauts()
        {
            CheckCount(_jagernautCount, _currentLevel.JahernautsCount);
        }
        private void CheckIsThereDrons()
        {
            CheckCount(_dronCount, _currentLevel.DronsCount);
        }
        private void CheckIsThereCopters()
        {
            CheckCount(_copterCount, _currentLevel.CoptersCount);
        }
        private void CheckCount(TextMeshProUGUI text, int count)
        {
            if (count <= 0)
            {
                text.transform.parent.gameObject.SetActive(false);
                return;
            }
            text.text = "x" + count;

        }
    }
}
