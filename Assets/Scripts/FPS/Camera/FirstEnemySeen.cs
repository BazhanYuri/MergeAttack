using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;



namespace FPS
{
    public class FirstEnemySeen : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _standartCamera;
        [SerializeField] private CinemachineVirtualCamera _followedEnemyCamera;

        [SerializeField] private PostProcessProfile _processProfile;


        [SerializeField] private int _timeToFocusOnEnemy;


        public static event Action StartedCinematic;
        public static event Action EndedCinematic;

        private Vignette _vignette;
        private Grain _grain;
        private ChromaticAberration _chromaticAberration;
        private bool _isActivated = false;

        public bool IsActivated { get => _isActivated;}

        private void OnEnable()
        {
            EnemySpawner.EnemySpawnedFirst += FocusCameraOnNewEnemy;
        }
        private void OnDisable()
        {
            EnemySpawner.EnemySpawnedFirst -= FocusCameraOnNewEnemy;
        }


        private void FocusCameraOnNewEnemy(Transform enemy, EnemyType enemyType)
        {
            StartCoroutine(LookAtEnemy(enemy));
        }
        private IEnumerator LookAtEnemy(Transform enemy)
        {
            yield return new WaitForSeconds(2);
            StartedCinematic?.Invoke();
            _isActivated = true;
            _vignette.color.Override(Color.black);
            _vignette.intensity.value = 0.8f;
            _grain.intensity.value = 1;
            _chromaticAberration.intensity.value = 1;

            _followedEnemyCamera.enabled = true;
            _followedEnemyCamera.LookAt = enemy;
            yield return new WaitForSeconds(_timeToFocusOnEnemy);
            EndedCinematic?.Invoke();
            _followedEnemyCamera.enabled = false;

            _vignette.intensity.value = 0;
            _grain.intensity.value = 0;
            _chromaticAberration.intensity.value = 0;
            _isActivated = false;
        }

        private void Start()
        {
            SetUpPostProcessing();
        }
        private void SetUpPostProcessing()
        {
            _vignette = _processProfile.GetSetting<Vignette>();
            _grain = _processProfile.GetSetting<Grain>();
            _chromaticAberration = _processProfile.GetSetting<ChromaticAberration>();
        }
    }
}

