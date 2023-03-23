using UnityEngine;
using DG.Tweening;

public class BulletTrailEffect
{
    public void ShowTrail(Transform bulletVisual, Vector3 pos, Vector3 dir)
    {
        Transform bullet = Object.Instantiate(bulletVisual, pos, Quaternion.LookRotation(dir));
        bullet.DOMove(dir, 0.45f).SetEase(Ease.Linear).OnComplete(() => Object.Destroy(bullet.gameObject));
    }
}
