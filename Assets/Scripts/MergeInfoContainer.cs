using UnityEngine;

public class MergeInfoContainer : MonoBehaviour
{
    private int _choosedWeaponIndex;
    private int _choosedAmmoIndex;
    private int _choosedExplosivesIndex;

    public int ChoosedWeaponIndex { get => _choosedWeaponIndex; }

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