using UnityEngine;
using UnityEngine.UI;


namespace FPS
{
    public class GranateChooser : MonoBehaviour
    {
        [SerializeField] private Arsenal _arsenal;
        [SerializeField] private Button _button;


        private void OnEnable()
        {
            _button.onClick.AddListener(TakeGranate);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(TakeGranate);
        }
        private void TakeGranate()
        {
            _arsenal.ChooseGranade();
            _button.gameObject.SetActive(false);
        }
    }
}

