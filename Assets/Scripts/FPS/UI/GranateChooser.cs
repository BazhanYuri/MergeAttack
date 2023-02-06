using UnityEngine;
using UnityEngine.UI;


namespace FPS
{
    public class GranateChooser : MonoBehaviour
    {
        [SerializeField] private MergeInfoContainer _mergeInfoContainer;
        [SerializeField] private Arsenal _arsenal;
        [SerializeField] private Button _button;


        private void OnEnable()
        {
            _button.onClick.AddListener(TakeGranate);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(TakeGranate);
            GameManager.Instance.GameplayStarted -= CheckToShowButton;
        }

        private void Start()
        {
            GameManager.Instance.GameplayStarted += CheckToShowButton;
        }

        private void TakeGranate()
        {
            _arsenal.ChooseGranade();
            _button.gameObject.SetActive(false);
            SoundManager.Instance.GranateTook();
        }
        private void CheckToShowButton()
        {
            if (_mergeInfoContainer.ChoosedExplosivnesIndex < 0)
            {
                return;
            }
            _button.gameObject.SetActive(true);
        }
    }
}

