using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;



namespace FPS
{
    public class CameraVisualEffects : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private PostProcessProfile _processProfile;
        [SerializeField] private FirstEnemySeen _firstEnemySeen;

        public static CameraVisualEffects Instance;

        private Vignette _vignette;
        private Bloom _bloom;


        public void SetHpBloodyScren(float value)
        {
            if (_firstEnemySeen.IsActivated == true)
            {
                return;
            }
            _vignette.intensity.value = value;
            _bloom.intensity.value = value;
        }
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
            SetUpPostProcessing();
        }
        private void SetUpPostProcessing()
        {
            _vignette = _processProfile.GetSetting<Vignette>();
            _bloom = _processProfile.GetSetting<Bloom>();
        }

    }
}
