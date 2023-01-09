using System.Collections;
using UnityEngine;
using TMPro;


namespace FPS
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField] private MergeInfoContainer _mergeInfoContainer;
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private BulletsData _bulletsData;

        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private TextMeshProUGUI _ammoCountText;

        private int _weaponIndex = -1;


        private int _ammoCount;


        private bool _isShooting;
        private void Start()
        {
            _playerInput.TapStart += StartShooting;
            _playerInput.TapEnded += StopShoot;
            GameManager.Instance.GameplayStarted += SetUpWeapon;
        }
        private void OnDisable()
        {
            _playerInput.TapStart -= StartShooting;
            _playerInput.TapEnded -= StopShoot;
            GameManager.Instance.GameplayStarted -= SetUpWeapon;
        }

        private void SetUpWeapon()
        {
            _weaponIndex = _mergeInfoContainer.ChoosedWeaponIndex;
            _weapons[_weaponIndex].gameObject.SetActive(true);

            _ammoCount = _bulletsData.AmmoCounts[_mergeInfoContainer.ChoosedAmmoIndex];
            UpdateAmmoCountUI();
        }

        private void StartShooting()
        {
            if (_ammoCount <= 0)
            {
                return;
            }
            StartCoroutine(StartingShooting());
        }
        private IEnumerator StartingShooting()
        {
            yield return new WaitForSeconds(_weapons[_weaponIndex].StartShootDelay);
            _isShooting = true;

            while (_ammoCount > 0)
            {
                Shoot();
                yield return new WaitForSeconds(_weapons[_weaponIndex].ShootDelay);
            }
        }
        private void Shoot()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out DamagablePart damagable))
                {
                    damagable.GetDamage(_weapons[_weaponIndex].Damage);
                }
            }


            _weapons[_weaponIndex].Animator.SetTrigger("shoot");

            _ammoCount--;
            UpdateAmmoCountUI();

            CameraShaker.Instance.ShakeCamera(0.04f, 0.1f, _weapons[_weaponIndex].ShootDelay * 0.8f);
        }
        private void StopShoot()
        {
            _isShooting = false;
            StopAllCoroutines();
        }

        private void UpdateAmmoCountUI()
        {
            _ammoCountText.text = _ammoCount.ToString();
        }
    }
}

