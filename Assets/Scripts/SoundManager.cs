using System.Collections;
using UnityEngine;





public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _itemBought;
    [SerializeField] private AudioSource _itemSelected;
    [SerializeField] private AudioSource _itemMerged;

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
            Destroy(audio);
        }
    }
}

