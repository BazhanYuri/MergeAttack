using UnityEngine;
using DG.Tweening;

namespace FPS
{
    [System.Serializable]
    public class Hidden
    {
        [SerializeField] private Vector3 _hiddenPos;
        [SerializeField] private Vector3 _hiddenRotation;
        [SerializeField] private EventsButton _crouchButton;


        private Transform _player;
        private Vector3 _standartPos;
        private Vector3 _standartRot;


        public void SubscripeEvents()
        {
            _crouchButton.OnClickedStart += Crouching;
            _crouchButton.OnClickedEnded += GetUp;
        }
        public void UnSubscribeEvents()
        {
            _crouchButton.OnClickedStart -= Crouching;
            _crouchButton.OnClickedStart -= GetUp;
        }
        public void Init(Transform player)
        {
            _player = player;
            _standartPos = _player.position;
            _standartRot = _player.rotation.eulerAngles;
        }

        private void Crouching()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_player.DOMove(_hiddenPos, 0.2f));
            seq.Join(_player.DORotate(_hiddenRotation, 0.2f));

            Debug.Log("I'm sitting");
        }
        private void GetUp()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_player.DOMove(_standartPos, 0.2f));
            seq.Join(_player.DORotate(_standartRot, 0.2f));
            Debug.Log("I'm sitting");
        }



    }
}

