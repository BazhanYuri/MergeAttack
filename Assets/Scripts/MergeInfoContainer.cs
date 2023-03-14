using UnityEngine;
using Merge;
using DG.Tweening;


public class MergeInfoContainer : MonoBehaviour
{
    [SerializeField] private Alert _weaponAlert;
    [SerializeField] private Alert _ammoAlert;


    private Item _choosedWeaponIndex =null;
    private Item _choosedAmmoIndex = null;
    private Item _choosedExplosivesIndex = null;

    public Item ChoosedWeaponIndex { get => _choosedWeaponIndex; }
    public Item ChoosedAmmoIndex { get => _choosedAmmoIndex; }
    public Item ChoosedExplosivnesIndex { get => _choosedExplosivesIndex; }






    private void OnEnable()
    {
        GameManager.GameplayStarted += DeleteChoosedWeapons;
    }
    private void OnDisable()
    {
        GameManager.GameplayStarted -= DeleteChoosedWeapons;
    }

    public bool IsAllEquiped()
    {
        return IsWeaponEquiped() && IsAmmoEquiped();
    }
    private bool IsWeaponEquiped()
    {
        if (ChoosedWeaponIndex == null)
        {
            _weaponAlert.ShowAlert();
            return false;
        }
        return true;
    }
    private bool IsAmmoEquiped()
    {
        if (ChoosedAmmoIndex == null)
        {
            _ammoAlert.ShowAlert();
            return false;
        }
        return true;
    }


    public void SetChoosedWeapon(Item item)
    {
        _choosedWeaponIndex = item;
    }
    public void SetChoosedAmmo(Item item)
    {
        _choosedAmmoIndex = item;
    }
    public void SetExplosivnes(Item item)
    {
        _choosedExplosivesIndex = item;
    }


    private void DeleteChoosedWeapons()
    {
        _choosedWeaponIndex?.CurrentCell.ClearItem();
        _choosedAmmoIndex?.CurrentCell.ClearItem();
        _choosedExplosivesIndex?.CurrentCell.ClearItem();
    }
}