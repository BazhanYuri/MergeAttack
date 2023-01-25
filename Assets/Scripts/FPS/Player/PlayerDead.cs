using UnityEngine;


namespace FPS
{
    public class PlayerDead : MonoBehaviour
    {
        [SerializeField] private PlayerDamagable _damagable;
        [SerializeField] private Rigidbody _cameraRigid;


        private void OnEnable()
        {
            _damagable.Dead += Dead;
            _damagable.HealthChanged += ShowBlodOnScreen;
        }
        private void OnDisable()
        {
            _damagable.Dead -= Dead;
            _damagable.HealthChanged -= ShowBlodOnScreen;
        }

        private void Dead()
        {
            _cameraRigid.isKinematic = false;
        }
        private void ShowBlodOnScreen(float value)
        {
            CameraVisualEffects.Instance.SetHpBloodyScren(1f - (value / 100f));
        }
    }
}
