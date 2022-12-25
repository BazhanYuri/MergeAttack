using DG.Tweening;
using UnityEngine;


namespace Merge
{
    public class ItemMovement : MonoBehaviour
    {
        [SerializeField] private Item _item;

        private int _currentXPos;
        private int _currentZPos;


        public void StartMoveItem()
        {
            _item.transform.GetChild(0).DOLocalMoveZ(-0.5f, 0.2f);
        }
        public void MoveItem(Touch touch)
        {
            _item.transform.localPosition = new Vector3(
                   _item.transform.localPosition.x + touch.deltaPosition.x * 0.0035f,
                   _item.transform.localPosition.y + touch.deltaPosition.y * 0.0035f,
                   _item.transform.localPosition.z);
        }
        public void MoveToCell(Vector3 cellPos)
        {
            Vector3 movePos = new Vector3(cellPos.x, cellPos.y, _item.transform.localPosition.z);
            _item.transform.DOLocalMove(movePos, 0.2f);
            
            EndMoveItem();
        }
        public void MoveBack()
        {

        }
        private void EndMoveItem()
        {
            _item.transform.GetChild(0).DOLocalMoveZ(0, 0.3f);
        }

    }
}
