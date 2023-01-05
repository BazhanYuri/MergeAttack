using UnityEngine;


public enum ItemType
{
    Firearms,
    Explosives,
    Ammo
}

namespace Merge
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        [SerializeField] private ItemMovement _itemMovement;
        [SerializeField] private ItemMerge _itemMerge;
        [SerializeField] private ItemUI _canvas;

        public ItemType ItemType { get => _itemType;}
        public ItemMovement ItemMovement { get => _itemMovement;}
        public Cell CurrentCell { get => _currentCell; set => _currentCell = value; }
        public ItemMerge ItemMerge { get => _itemMerge;}
        public ItemUI Canvas { get => _canvas;}
        public bool IsChoosed { get => _isChoosed; set => _isChoosed = value; }

        private Cell _currentCell;
        private bool _isChoosed = false;


    }
}
