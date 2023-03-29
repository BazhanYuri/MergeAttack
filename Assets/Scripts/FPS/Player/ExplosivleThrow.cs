using UnityEngine;

namespace FPS
{
    public class ExplosivleThrow : MonoBehaviour
    {
        [SerializeField] private Arsenal _arsenal;
        [SerializeField] private Explosinable _explosinable;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private int _acceleration;
        [SerializeField, Range(0, 1)] private float _minYScreenToShoot;



        private void OnEnable()
        {
            _playerInput.TapEnded += Throw;
        }
        private void OnDisable()
        {
            _playerInput.TapEnded -= Throw;
        }
       

        
        private void Throw(Vector2 percentTap)
        {
            if (percentTap.y < _minYScreenToShoot)
            {
                return;
            }
            if (_arsenal.CurrentWeaponType != WeaponType.Explosinable)
            {
                return;
            }
            _explosinable.transform.parent = null;

            Vector3 wordlPosCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            _explosinable.Rigidbody.isKinematic = false;
            _explosinable.Rigidbody.AddRelativeForce(wordlPosCenter.normalized * _acceleration * -1);

            _arsenal.ChooseWeapon();
        }
    }
}
