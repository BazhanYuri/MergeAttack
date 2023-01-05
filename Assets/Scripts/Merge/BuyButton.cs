using UnityEngine;
using UnityEngine.UI;


namespace Merge
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int _price;

        public int Price { get => _price;}
        public Button Button { get => _button;}

        public void SetAsActive()
        {
        }
        public void SetAsInactive()
        {
        }


    }
}

