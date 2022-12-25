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

        public ItemType ItemType { get => _itemType;}
        public ItemMovement ItemMovement { get => _itemMovement;}
    }
}
