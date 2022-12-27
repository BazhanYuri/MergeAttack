using UnityEngine;

namespace Merge
{
    public class Cell : MonoBehaviour
    {
        private Item _currentItem;

        public Item CurrentItem { get => _currentItem;}


        public bool isEmpty()
        {
            return _currentItem == null;
        }
        public void SetItem(Item item)
        {
            _currentItem = item;
        }
        public void ClearItem()
        {
            _currentItem = null;
        }
    }
}

