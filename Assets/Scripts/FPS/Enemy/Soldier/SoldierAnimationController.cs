using System.Collections;
using UnityEngine;



namespace FPS
{
    public class SoldierAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;



        private bool _isDead = false;

        public void DeathAnimation()
        {
            if (_isDead == true)
            {
                return;
            }
            StopCoroutine(SetAsDead());
            _animator.SetTrigger("Death" + Random.Range(1, 3));
            StartCoroutine(SetAsDead());
        }
        public void Aiming()
        {
            _animator.SetTrigger("Aiming");
        }
        public void Shoot()
        {
            _animator.SetTrigger("Shoot");
        }
        public void Run()
        {
            _animator.SetTrigger("Run");
        }
        private IEnumerator SetAsDead()
        {
            yield return new WaitForSeconds(1);
            _isDead = true;
        }
    }
}
