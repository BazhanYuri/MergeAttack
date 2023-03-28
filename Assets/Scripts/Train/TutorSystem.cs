using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;




public enum TutorActivness
{
    None,
    WeaponBought,
    AmmoBought,
    AmmoMerged,
    PlayPressed
}
public class TutorSystem : Singleton<TutorSystem>
{
    [SerializeField] private Tutor[] _tutors;
    [SerializeField] private CursorTutor _cursorTutor;


    public static event Action<TutorActivness> TutorCompleted;




    private void OnDisable()
    {
        for (int i = 0; i < _tutors.Length; i++)
        {
            _tutors[i].OnDisabled();
        }
    }
    private void Awake()
    {
        ShowTutor();
    }
    public void InvokeTutor(TutorActivness tutorActivness)
    {
        TutorCompleted?.Invoke(tutorActivness);
    }
    private async void ShowTutor()
    {
        if (PlayerPrefs.GetInt(Prefs.TutorPassed, 0) == 1)
        {
            return;
        }

        int tutorsCount = _tutors.Length;

        for (int i = 0; i < tutorsCount; i++)
        {
            _tutors[i].OnEnabled();
            await _tutors[i].ShowWindow(_cursorTutor);
        }


        PlayerPrefs.SetInt(Prefs.TutorPassed, 1);
    }
}



[Serializable]
public class Tutor
{
    [SerializeField] private RectTransform _window;
    [SerializeField] private RectTransform _cursorPoint;
    [SerializeField] private RectTransform _cursorPoint2;
    [SerializeField] protected TextMeshProUGUI _textTutor;
    [SerializeField] protected float _textShowTime;
    [SerializeField] protected float _pause;
    [SerializeField] private TutorActivness _activenesToPassTheTutor;
    [SerializeField] private int _actionCount;


    private int _currentActionCount = 0;
    private bool _isTutorCompleted = false;


    public void OnEnabled()
    {
        TutorSystem.TutorCompleted += TaskComleted;
    }
    public void OnDisabled()
    {
        TutorSystem.TutorCompleted -= TaskComleted;
    }

    public async Task ShowWindow(CursorTutor cursor)
    {
        cursor.ChangePoint(_cursorPoint, _cursorPoint2);

        _window.gameObject.SetActive(true);
        string text = _textTutor.text;
        _textTutor.text = "";
        Sequence seq = DOTween.Sequence();
        seq.Append(_textTutor.DOText(text, _textShowTime));
        seq.AppendInterval(_pause);

        
        if (_activenesToPassTheTutor == TutorActivness.None)
        {
            seq.AppendCallback(() => _window.gameObject.SetActive(false));
            await seq.AsyncWaitForCompletion();
        }
        else
        {

            while (_isTutorCompleted == false)
            {
                await Task.Yield();
            }
            _window.gameObject.SetActive(false);
        }
    }
    private void TaskComleted(TutorActivness activness)
    {
        Debug.Log("Task completed");
        if (activness == _activenesToPassTheTutor)
        {
            _currentActionCount++;
            if (_currentActionCount >= _actionCount)
            {
                _isTutorCompleted = true;
            }
        }
    }
}

[Serializable]
public class CursorTutor
{
    [SerializeField] private RectTransform _mouseRect;
    Sequence seq = DOTween.Sequence();

    
    public void ChangePoint(RectTransform rect, RectTransform rect2)
    {
        _mouseRect.gameObject.SetActive(true);
        seq.Kill();
        seq = DOTween.Sequence();
        _mouseRect.DORewind();

        seq.Append(_mouseRect.DOMove(rect.position, 1));
        seq.Join(_mouseRect.DORotate(rect.rotation.eulerAngles, 0.3f));

        if (rect2 != null)
        {
            seq.Append(_mouseRect.DOMove(rect2.position, 1));
            seq.Join(_mouseRect.DORotate(rect2.rotation.eulerAngles, 0.3f));

            seq.SetLoops(-1);
        }
    }
}

