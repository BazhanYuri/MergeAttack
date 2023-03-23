using System.Collections;
using UnityEngine;
using TMPro;


namespace FPS
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField] private GameplayScreen _gameplayScreen;
        [SerializeField] private MergeInfoContainer _mergeInfoContainer;
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private BulletsData _bulletsData;
        [SerializeField] private Arsenal _arsenal;

        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private ShootHitEffect _shootHitEffectPrefab;
        [SerializeField] private HitPointer _hitPointerUI;
        [SerializeField] private TextMeshProUGUI _ammoCountText;

        [SerializeField] private ParticleSystem _shootHoleParticle;


        private BulletTrailEffect _bulletTrailEffect;

        public static event System.Action OutOfAmmo;


        private int _weaponIndex = -1;
        private int _ammoCount;

        private bool _isShooting;







        private void Awake()
        {
            _bulletTrailEffect = new BulletTrailEffect();
        }
        private void Start()
        {
            _playerInput.TapStart += StartShooting;
            _playerInput.TapEnded += StopShoot;
            GameManager.GameplayStarted += SetUpWeapon;
            GameManager.LevelCompleted += StopShoot;
            FirstEnemySeen.StartedCinematic += StopShoot;
        }
        private void OnDisable()
        {
            _playerInput.TapStart -= StartShooting;
            _playerInput.TapEnded -= StopShoot;
            GameManager.GameplayStarted -= SetUpWeapon;
            GameManager.LevelCompleted -= StopShoot;
            FirstEnemySeen.StartedCinematic -= StopShoot;
        }

        private void SetUpWeapon()
        {
            _weaponIndex = _mergeInfoContainer.ChoosedWeaponIndex.ItemMerge.Index;
           _weapons[_weaponIndex].gameObject.SetActive(true);

            _ammoCount = (int)(_bulletsData.AmmoCounts[_mergeInfoContainer.ChoosedAmmoIndex.ItemMerge.Index] * _weapons[_weaponIndex].WeaponData.IndexOfAmmo);
            UpdateAmmoCountUI();

            SoundManager.Instance.WeaponTook();
        }

        private void StartShooting()
        {
            if (_ammoCount <= 0)
            {
                OutOfAmmo?.Invoke();
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
            StartCoroutine(ShootDelay());
            yield return new WaitUntil(() => _isShooting == true);
            while (_ammoCount > 0)
            {
                Shoot();

                float elapsedTime = 0;
                float percentage = 0;
                while (elapsedTime < _weapons[_weaponIndex].WeaponData.ShootDelay)
                {
                    elapsedTime += Time.deltaTime;
                    percentage = elapsedTime / _weapons[_weaponIndex].WeaponData.ShootDelay;
                    _gameplayScreen.SetPercentageOfShoot(percentage);
                    yield return null;
                }
            }
        }
        private IEnumerator ShootDelay()
        {
            float elapsedTime = 0;
            float percentage;
            while (true)
            {
                yield return null;
                elapsedTime += Time.deltaTime;
                percentage =  elapsedTime / _weapons[_weaponIndex].WeaponData.StartShootDelay;
                _gameplayScreen.SetPercentageOfShoot(percentage);
                if (elapsedTime > _weapons[_weaponIndex].WeaponData.StartShootDelay)
                {
                    break;
                }
            }
            _isShooting = true;
        }
        private void Shoot()
        {
            ShootFireLightParticle();
            Vector3 accuracy = GetAccuracy();
            Ray ray = Camera.main.ViewportPointToRay(accuracy);
            RaycastHit hit;
            _gameplayScreen.SetPercentageOfShoot(0);

            _bulletTrailEffect.ShowTrail(_weapons[_weaponIndex].WeaponData.BulletPrefab, _weapons[_weaponIndex].ShootPoint.position, ray.GetPoint(50));

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out DamagablePart damagable))
                {
                    damagable.GetDamage(_weapons[_weaponIndex].WeaponData.Damage);
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

            CameraVisualEffects.Instance.ShakeCamera(0.1f, 0.2f, _weapons[_weaponIndex].WeaponData.ShootDelay * 1f);
            SoundManager.Instance.Shoot(_weapons[_weaponIndex].WeaponData.ShootableType);
        }
        private void StopShoot()
        {
            _isShooting = false;
            StopAllCoroutines();
            _gameplayScreen.SetPercentageOfShoot(0);
        }
        private Vector3 GetAccuracy()
        {
            float randomAngle;
            randomAngle = Random.Range(0f, 0.1f - (_weapons[_weaponIndex].WeaponData.PercentageOfAccuracy / 1000f));

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

