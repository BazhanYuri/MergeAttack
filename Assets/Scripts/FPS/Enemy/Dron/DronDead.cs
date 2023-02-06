using System.Collections;
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
            InvokeDeadAction();
            StartCoroutine(DeleteJoint());
        }
        private IEnumerator DeleteJoint()
        {
            yield return new WaitForSeconds(2);
            Destroy(_mainPart);
        }
    }

}
