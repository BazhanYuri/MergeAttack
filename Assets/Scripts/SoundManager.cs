using System.Collections;
using UnityEngine;





public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _itemBought;
    [SerializeField] private AudioSource _itemSelected;
    [SerializeField] private AudioSource _itemMerged;
    [SerializeField] private AudioSource _granateTook;
    [SerializeField] private AudioSource _pistolShoot;

    public static SoundManager Instance;




    private void OnEnable()
    {
    }
    private void OnDisable()
    {
    }

    private void Start()
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
    public void Shoot(FPS.ShootableType shootableType)
    {
        switch (shootableType)
        {
            case FPS.ShootableType.Pistol:
                break;
            case FPS.ShootableType.Revolver:
                break;
            case FPS.ShootableType.Uzi:
                break;
            case FPS.ShootableType.AK47:
                break;
            case FPS.ShootableType.Machinegun:
                break;
            default:
                break;
        }
        PlaySound(_pistolShoot, 3);
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
}

