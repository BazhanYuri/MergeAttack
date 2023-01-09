using System.Collections;
using UnityEngine;
using Cinemachine;


public class CameraShaker : MonoBehaviour
{
    public CinemachineVirtualCamera camera;


    public static CameraShaker Instance;




    public void ShakeCamera(float aplitudeGain, float freqGain, float time)
    {
        StartCoroutine(ShakeWithTime(aplitudeGain, freqGain, time));
    }
    private IEnumerator ShakeWithTime(float aplitudeGain, float freqGain, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = aplitudeGain;
        perlin.m_FrequencyGain = freqGain;
        yield return new WaitForSeconds(time);
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }


    private void Start()
    {
        Instance = this;
    }

}