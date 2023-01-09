using UnityEngine;
using Merge;
using DG.Tweening;


public class MergeInfoContainer : MonoBehaviour
{
    [SerializeField] private Alert _weaponAlert;
    [SerializeField] private Alert _ammoAlert;


    private int _choosedWeaponIndex = - 1;
    private int _choosedAmmoIndex = -1;
    private int _choosedExplosivesIndex = -1;

    public int ChoosedWeaponIndex { get => _choosedWeaponIndex; }
    public int ChoosedAmmoIndex { get => _choosedAmmoIndex; }
    public int ChoosedExplosivnesIndex { get => _choosedExplosivesIndex; }



    public bool IsAllEquiped()
    {
        return IsWeaponEquiped() && IsAmmoEquiped();
    }
    private bool IsWeaponEquiped()
    {
        if (ChoosedWeaponIndex == -1)
        {
            _weaponAlert.ShowAlert();
            return false;
        }
        return true;
    }
    private bool IsAmmoEquiped()
    {
        if (ChoosedAmmoIndex == -1)
        {
            _ammoAlert.ShowAlert();
            return false;
        }
        return true;
    }


    public void SetChoosedWeaponIndex(int indexOfWeapon)
    {
        _choosedWeaponIndex = indexOfWeapon;
    }
    public void SetChoosedAmmoIndex(int indexOfAmmo)
    {
        _choosedAmmoIndex = indexOfAmmo;
    }
    public void SetExplosivnesIndex(int explosivesIndex)
    {
        _choosedExplosivesIndex = explosivesIndex;
    }
}