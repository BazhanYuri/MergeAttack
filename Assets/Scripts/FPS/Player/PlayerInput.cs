using System;
using UnityEngine;


[Serializable]
public class Clamps
{
    public float min;
    public float max;
}

namespace FPS
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Clamps _clampX;
        [SerializeField] private Clamps _clampY;
        [SerializeField] private int _xSpeed; 
        [SerializeField] private int _ySpeed;

        public event Action TapStart; 
        public event Action TapEnded;


        private bool _isCanControl = false;


        private void OnEnable()
        {
            GameManager.Instance.GameplayStarted += EnableControl;
        }
        private void OnDisable()
        {
            GameManager.Instance.GameplayStarted -= EnableControl;
        }

        private void Update()
        {
            if (_isCanControl == false)
            {
                return;
            }
            CheckInput();
        }
        private void CheckInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        TapStart?.Invoke();
                        break;
                    case TouchPhase.Moved:
                        RotatePlayerByTouch(touch);
                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:
                        TapEnded?.Invoke();
                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }
            }
        }


        private float _angleX = 0.0f;
        private float _angleY = 0.0f;
        private void RotatePlayerByTouch(Touch touch)
        {
            _angleX += touch.deltaPosition.y * -_xSpeed * Time.deltaTime;
            _angleX = Mathf.Clamp(_angleX, _clampX.min, _clampX.max);

            _angleY += touch.deltaPosition.x * _ySpeed * Time.deltaTime;
            _angleY = Mathf.Clamp(_angleY, _clampY.min, _clampY.max);

            _player.transform.rotation = Quaternion.Euler(_angleX, _angleY, 0.0f);
        }

        private void EnableControl()
        {
            _isCanControl = true;
        }
                
    }
    
}

