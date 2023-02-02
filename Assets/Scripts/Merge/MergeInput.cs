using System;
using UnityEngine;


namespace Merge
{
    public class MergeInput : MonoBehaviour
    {
        [SerializeField] private MergeInfoContainer _mergeInfoContainer;

        public event Action StartSlide;
        public event Action<Item> ItemSelected;


        private Vector2 _tapPosition;
        private Item _slidedItem;

        private bool _isItemDetected;
        private bool _isCanControll = true;


        private void OnEnable()
        {
            GameManager.Instance.GameplayStarted += DisableControl;
        }
        private void OnDisable()
        {
            GameManager.Instance.GameplayStarted -= DisableControl;
        }

        private void Update()
        {
            if (_isCanControll == false)
            {
                return;
            }

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
                        if ((_tapPosition - touch.position).magnitude < 20)
                        {
                            return;
                        }
                        if (_isItemDetected == false)
                        {
                            return;
                        }
                        MoveSelectedItem(touch);
                        break;
                    case TouchPhase.Ended:
                        if ((_tapPosition - touch.position).magnitude < 20)
                        {
                            CheckTapOnItem(touch.position);
                            return;
                        }
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
                    return true;
                }
            }

            return false;
        }

        private void MoveSelectedItem(Touch touch)
        {
            _slidedItem.ItemMovement.StartMoveItem();

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
                    if (_slidedItem.ItemMerge.IsMax() == true || cell.CurrentItem.ItemMerge.IsMax())
                    {
                        _slidedItem.ItemMovement.MoveBack();
                        return;
                    }
                    else if (cell.CurrentItem.ItemMerge.Index == _slidedItem.ItemMerge.Index && cell.CurrentItem.ItemType == _slidedItem.ItemType)
                    {
                        cell.CurrentItem.ItemMerge.Merge(_slidedItem);
                        SoundManager.Instance.ItemMerged(_slidedItem.ItemMerge.Index);
                    }
                }
            }

            _slidedItem.ItemMovement.MoveBack();
        }



        private Item _choosedWeapon;
        private Item _choosedAmmo;
        private Item _choosedExplo;

        private void CheckTapOnItem(Vector2 touchPos)
        {
            if (RayCheck(touchPos).TryGetComponent(out ColliderTypeDetect collideType))
            {
                if (collideType.Type == GameObjectType.Item)
                {
                    Item item = collideType.Root.GetComponent<Item>();
                    int index = item.ItemMerge.Index;

                    if (item.IsChoosed == false)
                    {
                        SoundManager.Instance.ItemSelected();
                        item.Canvas.ChooseItem();

                        switch (item.ItemType)
                        {
                            case ItemType.Firearms:
                                CheckIsTouchAnother(_choosedWeapon, item);
                                _choosedWeapon = item;
                                break;
                            case ItemType.Explosives:
                                CheckIsTouchAnother(_choosedExplo, item);
                                _choosedExplo = item;
                                break;
                            case ItemType.Ammo:
                                CheckIsTouchAnother(_choosedAmmo, item);
                                _choosedAmmo = item;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        item.Canvas.UnchooseItem();
                        index = -1;
                    }

                    item.IsChoosed = !item.IsChoosed;


                    switch (item.ItemType)
                    {
                        case ItemType.Firearms:
                            _mergeInfoContainer.SetChoosedWeaponIndex(index);
                            break;
                        case ItemType.Explosives:
                            _mergeInfoContainer.SetExplosivnesIndex(index);
                            break;
                        case ItemType.Ammo:
                            _mergeInfoContainer.SetChoosedAmmoIndex(index);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private void CheckIsTouchAnother(Item item, Item temp)
        {
            if (item == null)
            {
                return;
            }
            if (item.GetHashCode() != temp.GetHashCode())
            {
                item.Canvas.UnchooseItem();
                item.IsChoosed = false;
            }
        }

        private void DisableControl()
        {
            _isCanControll = false;
        }
    }
}
