using System.Collections;
using UnityEngine;


namespace FPS
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private PlayerInput _playerInput;

        private int _weaponIndex = 0;



        private bool _isShooting;
        private void OnEnable()
        {
            _playerInput.TapStart += StartShooting;
            _playerInput.TapEnded += StopShoot;
        }
        private void OnDisable()
        {
            _playerInput.TapStart -= StartShooting;
            _playerInput.TapEnded -= StopShoot;
        }
        private void StartShooting()
        {
            StartCoroutine(StartingShooting());
        }
        private IEnumerator StartingShooting()
        {
            yield return new WaitForSeconds(_weapons[_weaponIndex].StartShootDelay);
            _isShooting = true;

            while (true)
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

            }


            _weapons[_weaponIndex].Animator.SetTrigger("shoot");
        }
        private void StopShoot()
        {
            _isShooting = false;
            StopAllCoroutines();
        }
    }
}

