using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FPS
{
    public class SoldierDead : EnemyDead
    {
        [SerializeField] private Soldier _soldier;
        [SerializeField] private SoldierAnimationController _animationController;


        public override void OnDead()
        {
            base.OnDead();
            _animationController.DeathAnimation();
            InvokeDeadAction();
        }
    }
}

