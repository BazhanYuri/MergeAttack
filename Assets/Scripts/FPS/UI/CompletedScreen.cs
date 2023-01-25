using UnityEngine;
using UnityEngine.UI;



namespace FPS
{
    public class CompletedScreen : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;




        private void Start()
        {
            _nextLevelButton.onClick.AddListener(GameManager.Instance.NextLevel);
        }
        private void OnDisable()
        {
            _nextLevelButton.onClick.RemoveListener(GameManager.Instance.NextLevel);
        }
    }
}

