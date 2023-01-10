using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FPS
{
    public class SoldierDead : Death
    {
        [SerializeField] private Enemy _enemy;

        public override void Dead()
        {
            Destroy(_enemy.gameObject);
        }
    }
}

