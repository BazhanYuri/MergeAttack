using System.Collections;
using UnityEngine;





public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _itemBought;
    [SerializeField] private AudioSource _itemSelected;
    [SerializeField] private AudioSource _itemMerged;
    [SerializeField] private AudioSource _granateTook;
    [SerializeField] private AudioSource _weaponTook;

    [Header("Weapon"), Space, Space]
    [SerializeField] private AudioSource _pistolShoot;
    [SerializeField] private AudioSource _revolverShoot;
    [SerializeField] private Transform _uziShootRoot;
    [SerializeField] private Transform _ak47Root;
    [SerializeField] private Transform _machinegunRoot;
    [Header("Hits"), Space, Space]
    [SerializeField] private AudioSource _bodyShot;
    [SerializeField] private AudioSource _headShot;
    [SerializeField] private AudioSource _plastineShot;
    [SerializeField] private AudioSource _metalShot;

    [Header("Enemies"), Space, Space]
    [Header("Soldier"), Space, Space]
    [SerializeField] private AudioSource _soldierShoot;
    [SerializeField] private Transform _soldierHittedRoot;
    [SerializeField, Range(0, 100)] private int _chanceToPlayHittedSoldierSound;
    [SerializeField] private Transform _soldierDeadRoot;
    [SerializeField, Range(0, 100)] private int _chanceToPlayDeadSoldierSound;

    [Header("Jager"), Space, Space]
    [SerializeField] private AudioSource _jaherShoot;
    [SerializeField] private Transform _jaherSpawnedRoot;
    [SerializeField] private AudioSource _jaherDead;

    public static SoundManager Instance;




    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    private void Awake()
    {
        Instance = this;
    }


    public void ItemBought()
    {
        PlaySound(_itemBought, 3);
    }
    public void ItemSelected()
    {
        PlaySound(_itemSelected, 3);
    }
    public void ItemMerged(int level)
    {
        _itemMerged.pitch = 1 + (level * 0.2f);
        PlaySound(_itemMerged, 3);
    }
    public void GranateTook()
    {
        PlaySound(_granateTook, 3);
    }
    public void WeaponTook()
    {
        PlaySound(_weaponTook, 3);
    }
    public void BodyShot()
    {
        _bodyShot.pitch = Random.Range(0.95f, 1.1f);
        PlaySound(_bodyShot, 3);
    }
    public void HeadShot()
    {
        _headShot.pitch = Random.Range(0.95f, 1.1f);
        PlaySound(_headShot, 3);
    }
    public void PlastineShot()
    {
        _plastineShot.pitch = Random.Range(0.95f, 1.1f);

        PlaySound(_plastineShot, 3);
    }
    public void MetalShot()
    {
        _metalShot.pitch = Random.Range(0.8f, 0.9f);

        PlaySound(_metalShot, 3);
    }


    public void SoldierShoot(Transform parent)
    {
        _soldierShoot.pitch = Random.Range(0.9f, 1.1f);

        PlaySound(_soldierShoot, 3, parent);
    }
    public void SoldierHitted(Transform parent)
    {
        if (Random.Range(0, 100) < _chanceToPlayHittedSoldierSound)
        {
            AudioSource rand = RandomChild(_soldierHittedRoot);
            rand.pitch = Random.Range(0.9f, 1.1f);

            PlaySound(rand, 3, parent);
        }
    }
    public void SoldierDied(Transform parent)
    {
        if (Random.Range(0, 100) < _chanceToPlayDeadSoldierSound)
        {
            AudioSource rand = RandomChild(_soldierDeadRoot);
            rand.pitch = Random.Range(0.9f, 1.1f);

            PlaySound(rand, 3, parent);
        }
    }
    public void JahherShoot(Transform parent)
    {
        _jaherShoot.pitch = Random.Range(0.9f, 1.1f);

        PlaySound(_jaherShoot, 3, parent);
    }
    public void JahherSpawned(Transform parent)
    {
        AudioSource rand = RandomChild(_jaherSpawnedRoot);
        rand.pitch = Random.Range(0.7f, 1f);

        PlaySound(rand, 3, parent);
    }
    public void JahherDead(Transform parent)
    {
        _jaherDead.pitch = Random.Range(0.75f, 1f);

        PlaySound(_jaherDead, 3, parent);
    }

    public void Shoot(ShootableType shootableType)
    {
        switch (shootableType)
        {
            case ShootableType.Pistol:
                PlaySound(_pistolShoot, 3);
                break;
            case ShootableType.Revolver:
                PlaySound(_revolverShoot, 3);
                break;
            case ShootableType.Uzi:
                UziSound();
                break;
            case ShootableType.AK47:
                AK47Sound();
                break;
            case ShootableType.Machinegun:
                MachineGunSound();
                break;
            default:
                break;
        }
    }
    private void UziSound()
    {
        PlaySound(RandomChild(_uziShootRoot), 3);
    }
    private void AK47Sound()
    {
        PlaySound(RandomChild(_ak47Root), 3);
    }
    private void MachineGunSound()
    {
        PlaySound(RandomChild(_machinegunRoot), 3);
    }
    





    private AudioSource RandomChild(Transform parent)
    {
        return parent.GetChild(Random.Range(0, parent.childCount)).GetComponent<AudioSource>();
    }
    private void PlaySound(AudioSource audioSource, float timeToDestroy)
    {
        StartCoroutine(Play(audioSource, timeToDestroy));
    }
    private IEnumerator Play(AudioSource audioSource, float timeToDestroy)
    {
        AudioSource audio = Instantiate(audioSource);
        audio.Play();

        if (timeToDestroy != -1)
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy(audio.gameObject);
        }
    }


    private void PlaySound(AudioSource audioSource, float timeToDestroy, Transform parent)
    {
        StartCoroutine(Play(audioSource, timeToDestroy, parent));
    }
    private IEnumerator Play(AudioSource audioSource, float timeToDestroy, Transform parent)
    {
        AudioSource audio = Instantiate(audioSource);
        audio.transform.parent = null;
        audio.transform.position = parent.position;
        audio.Play();

        if (timeToDestroy != -1)
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy(audio.gameObject);
        }
    }
}

