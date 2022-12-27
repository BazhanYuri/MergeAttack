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

        public ItemType ItemType { get => _itemType;}
        public ItemMovement ItemMovement { get => _itemMovement;}
        public Cell CurrentCell { get => _currentCell; set => _currentCell = value; }
        public ItemMerge ItemMerge { get => _itemMerge;}

        private Cell _currentCell;
    }
}
