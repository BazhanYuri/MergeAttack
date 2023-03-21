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
            HideCanvas();
            _item.transform.GetChild(0).DOLocalMoveZ(-0.5f, 0.2f);
            _isMoving = true;
        }
        public void MoveItem(Touch touch)   
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 1f);
            Vector3 screenTouch = screenCenter + new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0f);

            Vector3 worldCenterPosition = Camera.main.ScreenToWorldPoint(screenCenter);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(screenTouch);

            Vector3 worldDeltaPosition = worldTouchPosition - worldCenterPosition;

            _item.transform.localPosition = new Vector3(
                   _item.transform.localPosition.x + worldDeltaPosition.x * 7, 
                   _item.transform.localPosition.y + worldDeltaPosition.y * 7,
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
            ShowCanvas();
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
        private void HideCanvas()
        {
            _item.ItemUI.HideCanvas();
        }
        private void ShowCanvas()
        {
            _item.ItemUI.ShowCanvas();
        }
    }
}
