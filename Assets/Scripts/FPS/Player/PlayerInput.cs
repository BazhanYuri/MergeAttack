using System;
using UnityEngine;


[Serializable]
public class Clamps
{
    public float min;
    public float max;



    public float GetRandomValue()
    {
        return UnityEngine.Random.Range(min, max);
    }
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

        private Vector2 _delta;
        private bool _isCanControl = false;
        private bool _isDragging = false;

        private void Start()
        {
            GameManager.GameplayStarted += EnableControl;
            GameManager.LevelCompleted += DisableControl;
            FirstEnemySeen.StartedCinematic += DisableControl;
            FirstEnemySeen.EndedCinematic += EnableControl;
        }
        private void OnDisable()
        {
            GameManager.GameplayStarted -= EnableControl;
            GameManager.LevelCompleted -= DisableControl;
            FirstEnemySeen.StartedCinematic -= DisableControl;
            FirstEnemySeen.EndedCinematic -= EnableControl;
        }

        private void Update()
        {
            if (_isCanControl == false)
            {
                return;
            }
            CheckInput();
        }
        private void FixedUpdate()
        {
            if (_isDragging == false)
            {
                return;
            }
            // RotatePlayerByTouch();
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
                        GetDeltas(touch);
                        break;
                    case TouchPhase.Stationary:
                        _isDragging = false;
                        break;
                    case TouchPhase.Ended:
                        TapEnded?.Invoke();
                        _isDragging = false;
                        break;
                    case TouchPhase.Canceled:
                        _isDragging = false;
                        break;
                    default:
                        break;
                }
            }
        }


        private float _angleX = 0.0f;
        private float _angleY = 0.0f;

        private void GetDeltas(Touch touch)
        {
            _isDragging = true;
            _delta = new Vector2(touch.deltaPosition.x, touch.deltaPosition.y);
            RotatePlayerByTouch();
        }
        private void RotatePlayerByTouch()
        {


            _angleX += _delta.y * -_xSpeed * Time.deltaTime;
            _angleX = Mathf.Clamp(_angleX, _clampX.min, _clampX.max);

            _angleY += _delta.x * _ySpeed * Time.deltaTime;
            _angleY = Mathf.Clamp(_angleY, _clampY.min, _clampY.max);

            _player.transform.rotation = Quaternion.Euler(_angleX, _angleY, 0.0f);
        }

        private void EnableControl()
        {
            _isCanControl = true;
        }
        private void DisableControl()
        {
            _isCanControl = false;
        }
    }
}

