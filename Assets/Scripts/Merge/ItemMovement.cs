using DG.Tweening;
using UnityEngine;


namespace Merge
{
    public class ItemMovement : MonoBehaviour
    {
        [SerializeField] private Item _item;

        private int _currentXPos;
        private int _currentZPos;

        private bool _isMoving = false;

        public void StartMoveItem()
        {
            if (_isMoving == true)
            {
                return;
            }
            _item.transform.GetChild(0).DOLocalMoveZ(-0.5f, 0.2f);
            _isMoving = true;
        }
        public void MoveItem(Touch touch)
        {
            _item.transform.localPosition = new Vector3(
                   _item.transform.localPosition.x + touch.deltaPosition.x * 0.0035f,
                   _item.transform.localPosition.y + touch.deltaPosition.y * 0.0035f,
                   _item.transform.localPosition.z);
        }
        public void MoveToCell(Cell cell)
        {
            ClearCurrentCell();

            Vector3 movePos = new Vector3(cell.transform.localPosition.x, cell.transform.localPosition.y, _item.transform.localPosition.z);
            _item.transform.DOLocalMove(movePos, 0.2f);

            _item.CurrentCell = cell;
            cell.SetItem(_item);

            EndMoveItem();
        }
        public void MoveBack()
        {
            MoveToCell(_item.CurrentCell);
        }
        private void EndMoveItem()
        {
            _item.transform.GetChild(0).DOLocalMoveZ(0, 0.3f);
            _isMoving = false;
        }
        private void ClearCurrentCell()
        {
            if (_item.CurrentCell == null)
            {
                return;
            }
            _item.CurrentCell.ClearItem();
            _item.CurrentCell = null;
        }
    }
}
