using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


namespace Merge
{
    public class WeaponStats : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private RectTransform _stats;
        [SerializeField] private WeaponData[] _datas;


        [SerializeField] private Slider _damage;
        [SerializeField] private Slider _shootRate;
        [SerializeField] private TextMeshProUGUI _ammoCount;




        private int _currentAmmo = 0;



        private void OnEnable()
        {
            _item.ItemUI.Choosed += SetActivityOfStats;
            AmmoStats.AmmoChoosed += SetAmmo;
        }
        private void OnDisable()
        {
            _item.ItemUI.Choosed -= SetActivityOfStats;
            AmmoStats.AmmoChoosed -= SetAmmo;
        }



        private void SetActivityOfStats(bool value)
        {
            _stats.gameObject.SetActive(value);
            SetWeaponDamage();
            SetShootRate();
            SetAmmoCount();
        }
        private void SetWeaponDamage()
        {
            _damage.value = 0;
            _damage.DOValue(_datas[_item.ItemMerge.Index].Damage, 0.3f);
        }
        private void SetShootRate()
        {
            _shootRate.value = 0;
            _shootRate.DOValue(_shootRate.maxValue - _datas[_item.ItemMerge.Index].ShootDelay, 0.3f);
        }
        private void SetAmmoCount()
        {
            _ammoCount.text = "X" + (int)(_currentAmmo * _datas[_item.ItemMerge.Index].IndexOfAmmo);
        }
        private void SetAmmo(int count)
        {
            _currentAmmo = count;
            SetAmmoCount();
        }
    }
}

