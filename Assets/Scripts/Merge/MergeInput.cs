using System;
using UnityEngine;


namespace Merge
{
    public class MergeInput : MonoBehaviour
    {
        public event Action StartSlide;
        public event Action<Item> ItemSelected;


        private Vector2 _tapPosition;
        private Item _slidedItem;

        private bool _isItemDetected;

        private void Update()
        {
            CheckSlide();
        }

        private void CheckSlide()
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _isItemDetected = Detect(touch.position);
                        break;
                    case TouchPhase.Moved:
                        if (_isItemDetected == false)
                        {
                            return;
                        }
                        MoveSelectedItem(touch);
                        break;
                    case TouchPhase.Ended:

                        if (_isItemDetected == false)
                        {
                            return;
                        }
                        FinishMoving(touch.position);
                        break;
                    default:
                        break;
                }
            }

        }



        private bool Detect(Vector2 touchPos)
        {
            if (RayCheck(touchPos).TryGetComponent(out ColliderTypeDetect collideType))
            {
                if (collideType.Type == GameObjectType.Item)
                {
                    _slidedItem = collideType.Root.GetComponent<Item>();
                    _tapPosition = touchPos;
                    _slidedItem.ItemMovement.StartMoveItem();
                    return true;
                }
            }

            return false;
        }

        private void MoveSelectedItem(Touch touch)
        {
            _slidedItem.ItemMovement.MoveItem(touch);
        }
        
        private Collider RayCheck(Vector2 touchPos)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider;
            }

            return null;
        }
        private Collider RayCheck<T>(Vector2 touchPos)
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            hits = Physics.RaycastAll(ray.origin, ray.direction, 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.GetComponent<T>() != null)
                {
                    return hits[i].collider;
                }
            }
            
            return null;
        }


        private void FinishMoving(Vector2 touchPos)
        {
            _isItemDetected = false;

            if (RayCheck<Cell>(touchPos) != null)
            {
                if (RayCheck<Cell>(touchPos).TryGetComponent(out Cell cell))
                {
                    if (cell.isEmpty() == true || cell.CurrentItem.GetHashCode() == _slidedItem.GetHashCode())
                    {
                        _slidedItem.ItemMovement.MoveToCell(cell);
                        return;
                    }
                    else if (cell.CurrentItem.ItemMerge.Index == _slidedItem.ItemMerge.Index && cell.CurrentItem.ItemType == _slidedItem.ItemType)
                    {
                        cell.CurrentItem.ItemMerge.Merge(_slidedItem);
                    }
                }
            }
            


            _slidedItem.ItemMovement.MoveBack();
        }
    }
}
