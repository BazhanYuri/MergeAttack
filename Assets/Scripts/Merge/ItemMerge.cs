using UnityEngine;
using DG.Tweening;


namespace Merge
{
    public class ItemMerge : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private Transform[] _skins;
        [SerializeField] private int _maxIndex;


        private int _index = 0;

        public int Index { get => _index; }
        public int MaxIndex { get => _maxIndex;}

        public bool IsMax()
        {
            return Index == MaxIndex;
        }
        public void Merge(Item item)
        {
            Destroy(item.gameObject);
            _index++;
            _skins[_index - 1].DOScale(Vector3.zero, 0.2f).OnComplete(() => ChangeVisual(_index));
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
            _skins[_index].localScale = Vector3.zero;
            _skins[_index].DOScale(Vector3.one, 0.3f);
        }
    }
}

