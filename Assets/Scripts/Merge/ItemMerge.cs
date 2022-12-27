using UnityEngine;


namespace Merge
{
    public class ItemMerge : MonoBehaviour
    {
        [SerializeField] private Transform[] _skins;

        private int _index = 0;

        public int Index { get => _index; }



        public void Merge(Item item)
        {
            Destroy(item.gameObject);
            _index++;

            ChangeVisual(_index);
        }

        private void ChangeVisual(int index)
        {
            for (int i = 0; i < _skins.Length; i++)
            {
                if (index == i)
                {
                    _skins[i].gameObject.SetActive(true);
                }
                else
                {
                    _skins[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

