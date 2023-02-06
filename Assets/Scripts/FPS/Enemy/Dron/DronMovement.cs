using System.Collections;
using UnityEngine;
using DG.Tweening;


namespace FPS
{
    public class DronMovement : EnemyMovement
    {
        [Header ("Sides movement")]
        [SerializeField] private Transform _sidesImulse;
        [SerializeField] private Clamps _timeToMoveSides;
        [SerializeField] private Clamps _countToMoveSides;
        [SerializeField] private Clamps _speedTimeToMoveSides;
        [SerializeField] private Clamps _minMaxPositionToMoveSides;

        [Space]
        [Space]
        [Space]
        [Header("Up Down movement")]
        [SerializeField] private Clamps _timeToMoveUpDown;
        [SerializeField] private Clamps _countToMoveUpDown;
        [SerializeField] private Clamps _speedTimeToMoveUpDown;
        [SerializeField] private Clamps _minMaxPositionToMoveUpDown;


        protected override void Start()
        {
            base.Start();
            StartCoroutine(MoveSides());
            StartCoroutine(MoveUpDown());
        }


        private IEnumerator MoveSides()
        {
            Sequence sideMovement;

            int timesToMoveSide;
            bool animationPlaying = false;

            while (true)
            {
                if (animationPlaying == false)
                {
                    yield return new WaitForSeconds(_timeToMoveSides.GetRandomValue());
                    sideMovement = DOTween.Sequence();
                    timesToMoveSide = (int)_countToMoveSides.GetRandomValue();

                    for (int i = 0; i < timesToMoveSide; i++)
                    {
                        sideMovement.Append(_sidesImulse.DOLocalMoveX(_minMaxPositionToMoveSides.GetRandomValue(), _speedTimeToMoveSides.GetRandomValue()));
                    }
                    animationPlaying = true;
                    sideMovement.OnComplete(() => animationPlaying = false);
                    sideMovement.Play();
                }
                yield return null;
            }
        }
        private IEnumerator MoveUpDown()
        {
            Sequence upDownMovement;

            int timesToMoveSide;
            bool animationPlaying = false;

            while (true)
            {
                if (animationPlaying == false)
                {
                    yield return new WaitForSeconds(_timeToMoveUpDown.GetRandomValue());
                    upDownMovement = DOTween.Sequence();
                    timesToMoveSide = (int)_countToMoveUpDown.GetRandomValue();

                    for (int i = 0; i < timesToMoveSide; i++)
                    {
                        upDownMovement.Append(_sidesImulse.DOLocalMoveY(_minMaxPositionToMoveUpDown.GetRandomValue(), _speedTimeToMoveUpDown.GetRandomValue()));
                    }
                    animationPlaying = true;
                    upDownMovement.OnComplete(() => animationPlaying = false);
                    upDownMovement.Play();
                }
                yield return null;
            }
        }

        protected override void OnDead()
        {
            base.OnDead();
            StopAllCoroutines();
        }
    }
}

