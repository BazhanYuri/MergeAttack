using UnityEngine;
using UnityEngine.UI;


namespace FPS
{
    public class GranateChooser : MonoBehaviour
    {
        [SerializeField] private MergeInfoContainer _mergeInfoContainer;
        [SerializeField] private Arsenal _arsenal;
        [SerializeField] private Button _button;



        public static event System.Action ExpoEquiped;


        private void OnEnable()
        {
            _button.onClick.AddListener(TakeGranate);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(TakeGranate);
            GameManager.GameplayStarted -= CheckToShowButton;
        }

        private void Start()
        {
            GameManager.GameplayStarted += CheckToShowButton;
        }

        private void TakeGranate()
        {
            _arsenal.ChooseGranade();
            _button.gameObject.SetActive(false);
            SoundManager.Instance.GranateTook();
        }
        private void CheckToShowButton()
        {
            if (_mergeInfoContainer.ChoosedExplosivnesIndex == null)
            {
                return;
            }
            if (_mergeInfoContainer.ChoosedExplosivnesIndex.ItemMerge.Index < 0)
            {
                return;
            }
            _button.gameObject.SetActive(true);
            ExpoEquiped?.Invoke();
        }
    }
}

