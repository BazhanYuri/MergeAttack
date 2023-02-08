using UnityEngine;
using DG.Tweening;




namespace FPS
{
    public class CopterMovement : DronMovement
    {

        protected override void Start()
        {
            base.Start();
            FlyUp();
        }
        private void FlyUp()
        {
            _visualPart.DOLocalMoveY(0, 5).OnComplete(() => StartDronMovement());
        }
    }

}
