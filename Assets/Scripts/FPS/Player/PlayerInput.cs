using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;


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
        [SerializeField] private UnityEngine.InputSystem.PlayerInput _playerInput;
        [SerializeField] private Player _player;
        [SerializeField] private Clamps _clampX;
        [SerializeField] private Clamps _clampY;
        [SerializeField] private int _xSpeed;
        [SerializeField] private int _ySpeed;




        public event Action<Vector2> TapStart;
        public event Action<Vector2> TapEnded;

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
        private void OnEnable()
        {

            TouchSimulation.Enable();
            EnhancedTouchSupport.Enable();
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += SwipeStarted;
            UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += SwipeCanceled;
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
            SwipePerformed(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0]);
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
                //Touch touch = Input.GetTouch(0);

               /* switch (touch.phase)
                {
                    case TouchPhase.Began:
                        TapStart?.Invoke(new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.height));
                        break;
                    case TouchPhase.Moved:
                        GetDeltas(touch);
                        break;
                    case TouchPhase.Stationary:
                        _isDragging = false;
                        break;
                    case TouchPhase.Ended:
                        TapEnded?.Invoke(new Vector2(touch.position.x / Screen.width, touch.position.y / Screen.height));
                        _isDragging = false;
                        break;
                    case TouchPhase.Canceled:
                        _isDragging = false;
                        break;
                    default:
                        break;
                }*/
            }
        }


        private void SwipeStarted(Finger finger)
        {
            if (_isCanControl == false)
            {
                return;
            }

            Vector2 pos = new Vector2(finger.screenPosition.x, finger.screenPosition.y);
            TapStart?.Invoke(new Vector2(pos.x / Screen.width, pos.y / Screen.height));
        }
        private void SwipePerformed(UnityEngine.InputSystem.EnhancedTouch.Touch touch)
        {
            if (_isCanControl == false)
            {
                return;
            }
            Vector2 pos = new Vector2(touch.delta.x, touch.delta.y);
            GetDeltas(pos);
            print(pos);

        }
        private void SwipeCanceled(Finger finger)
        {
            if (_isCanControl == false)
            {
                return;
            }
            Vector2 pos = new Vector2(finger.screenPosition.x, finger.screenPosition.y);

            TapEnded?.Invoke(new Vector2(pos.x / Screen.width, pos.y / Screen.height));
            _isDragging = false;
        }




        private float _angleX = 0.0f;
        private float _angleY = 0.0f;

        private void GetDeltas(Vector2 touchDelta)
        {
            _isDragging = true;
            _delta = new Vector2(touchDelta.x, touchDelta.y);
            RotatePlayerByTouch();
        }
        private void RotatePlayerByTouch()
        {
            print(_angleX);
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

