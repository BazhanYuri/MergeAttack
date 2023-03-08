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
        [SerializeField] private Arsenal _arsenal;

        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private ShootHitEffect _shootHitEffectPrefab;
        [SerializeField] private HitPointer _hitPointerUI;
        [SerializeField] private TextMeshProUGUI _ammoCountText;

        [SerializeField] private ParticleSystem _shootHoleParticle;

        private int _weaponIndex = -1;
        private int _ammoCount;

        private bool _isShooting;







        private void Start()
        {
            _playerInput.TapStart += StartShooting;
            _playerInput.TapEnded += StopShoot;
            GameManager.Instance.GameplayStarted += SetUpWeapon;
            GameManager.LevelCompleted += StopShoot;
            FirstEnemySeen.StartedCinematic += StopShoot;
        }
        private void OnDisable()
        {
            _playerInput.TapStart -= StartShooting;
            _playerInput.TapEnded -= StopShoot;
            GameManager.Instance.GameplayStarted -= SetUpWeapon;
            GameManager.LevelCompleted -= StopShoot;
            FirstEnemySeen.StartedCinematic -= StopShoot;
        }

        private void SetUpWeapon()
        {
            _weaponIndex = _mergeInfoContainer.ChoosedWeaponIndex;
            _weapons[_weaponIndex].gameObject.SetActive(true);

            _ammoCount = _bulletsData.AmmoCounts[_mergeInfoContainer.ChoosedAmmoIndex];
            UpdateAmmoCountUI();

            SoundManager.Instance.WeaponTook();
        }

        private void StartShooting()
        {
            if (_ammoCount <= 0)
            {
                return;
            }
            if (_arsenal.CurrentWeaponType != WeaponType.Weapon)
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
            ShootFireLightParticle();
            Ray ray = Camera.main.ViewportPointToRay(GetAccuracy());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out DamagablePart damagable))
                {
                    damagable.GetDamage(_weapons[_weaponIndex].Damage);
                    HitType hitType = HitType.Head;

                    switch (damagable.PartType)
                    {
                        case PartType.Head:
                            hitType = HitType.Head;
                            break;
                        case PartType.Body:
                            hitType = HitType.Body;
                            break;
                        case PartType.Arm:
                            hitType = HitType.Body;
                            break;
                        case PartType.Leg:
                            hitType = HitType.Body;
                            break;
                        case PartType.DronMain:
                            hitType = HitType.Metal;
                            break;
                        case PartType.Weapon:
                            hitType = HitType.Metal;
                            break;
                        case PartType.Protected:
                            hitType = HitType.ProtectedPart;
                            break;
                        default:
                            break;
                    }

                    SpawnHitEffect(hit.point, hitType);

                    ShowPointer(hit.point, hitType);
                    PlayHitSound(hitType);
                }
                else
                {
                    Instantiate(_shootHoleParticle).transform.position = hit.point;
                }

            }


            _weapons[_weaponIndex].Animator.SetTrigger("shoot");

            _ammoCount--;
            UpdateAmmoCountUI();

            CameraVisualEffects.Instance.ShakeCamera(0.1f, 0.2f, _weapons[_weaponIndex].ShootDelay * 1f);
            SoundManager.Instance.Shoot(_weapons[_weaponIndex].ShootableType);
        }
        private void StopShoot()
        {
            _isShooting = false;
            StopAllCoroutines();
        }
        private Vector3 GetAccuracy()
        {
            float randomAngle;
            randomAngle = Random.Range(0f, 0.1f - (_weapons[_weaponIndex].PercentageOfAccuracy / 1000f));

            return new Vector3(0.5F - randomAngle, 0.5F + randomAngle, 0);
        }

        private void UpdateAmmoCountUI()
        {
            _ammoCountText.text = _ammoCount.ToString();
        }
        private void ShowPointer(Vector3 position, HitType hitType)
        {
            _hitPointerUI.ShowPointer(position, hitType);
        }
        private void ShootFireLightParticle()
        {
            ParticleSystem particleSystem = Instantiate(_weapons[_weaponIndex].ShootParticle);
            particleSystem.transform.position = _weapons[_weaponIndex].ShootPoint.position;
            particleSystem.transform.parent = _weapons[_weaponIndex].ShootPoint;
        }
        private void SpawnHitEffect(Vector3 position, HitType hitType)
        {
            ShootHitEffect shootHitEffect = Instantiate(_shootHitEffectPrefab);
            shootHitEffect.transform.position = position;
            shootHitEffect.ChooseEffect(hitType);
        }
        private void PlayHitSound(HitType hitType)
        {
            switch (hitType)
            {
                case HitType.Head:
                    SoundManager.Instance.HeadShot();
                    break;
                case HitType.Body:
                    SoundManager.Instance.BodyShot();
                    break;
                case HitType.ProtectedPart:
                    SoundManager.Instance.PlastineShot();
                    break;
                case HitType.Metal:
                    SoundManager.Instance.MetalShot();
                    break;
                default:
                    break;
            }
        }
    }
}

