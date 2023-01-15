using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform _mergeStage;

    private int _currentLevel;

    public int CurrentLevel { get => _currentLevel; }

    public event Action GameplayStarted;



    private void Awake()
    {
        _currentLevel = PlayerPrefs.GetInt(Prefs.CurrentLevel, 0);
        Instance = this;
    }

    public void StartGameplay()
    {
        GameplayStarted?.Invoke();
        _mergeStage.gameObject.SetActive(false);
    }

}