using UnityEngine;




namespace FPS
{
    public class DronDead : EnemyDead
    {
        [SerializeField] private Rigidbody _rotatable;
        [SerializeField] private SpringJoint _mainPart;

        public override void OnDead()
        {
            base.OnDead();
            _rotatable.isKinematic = false;
            Destroy(_mainPart);
            InvokeDeadAction();
        }
    }

}
