using UnityEngine;


namespace Merge
{
    public class AmmoStats : MonoBehaviour
    {
        [SerializeField] private Item _item;


        [SerializeField] private BulletsData _bulletsData;

        public static event System.Action<int> AmmoChoosed;


        private void OnEnable()
        {
            _item.ItemUI.Choosed += OnAmmoSelected;
        }
        private void OnDisable()
        {
            _item.ItemUI.Choosed -= OnAmmoSelected;
        }

        private void OnAmmoSelected(bool value)
        {
            if (value == true)
            {
                AmmoChoosed.Invoke(_bulletsData.AmmoCounts[_item.ItemMerge.Index]);
            }
        }
    }

}
