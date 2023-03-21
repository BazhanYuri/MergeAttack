using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FPS;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private LevelsInfo _levelsInfo;

    [SerializeField] private CanvasGroup _FPSUI;
    [SerializeField] private CanvasGroup _levelCompletedUI;

    [SerializeField] private Transform _mergeStage;
    [SerializeField] private EnemiesKilledIndicator _killedIndicator;

    private LevelVisual _levelVisual;

    private int _currentLevel;
    private int _countOfAllEnemies;

    public LevelInfo CurrentLevelInfo { get => _levelsInfo.LevelInfos[_currentLevel]; }
    public int CurrentLevel { get => _currentLevel; }
    public int CountOfAllEnemies { get => _countOfAllEnemies; }
    public LevelVisual LevelVisual { get => _levelVisual;}

    public static event Action GameplayStarted;
    public static event Action LevelCompleted;





    private void OnEnable()
    {
        EnemyDead.DeadEnemy += CheckIsLevelPassed;
    }
    private void OnDisable()
    {
        EnemyDead.DeadEnemy -= CheckIsLevelPassed;
    }



    private void Awake()
    {
        _currentLevel = PlayerPrefs.GetInt(Prefs.CurrentLevel, 0);

        _levelVisual = Instantiate(_levelsInfo.LevelInfos[_currentLevel].LevelVisualPrefab);
        RenderSettings.skybox = _levelsInfo.LevelInfos[_currentLevel].SkyBox;
        Instance = this;
    }


    
    public void NextLevel()
    {
        _currentLevel++;
        PlayerPrefs.SetInt(Prefs.CurrentLevel, _currentLevel);
        SceneManager.LoadScene(0);
    }
    public void SetCountOfAllEnemies(int count)
    {
        _countOfAllEnemies = count;
        _killedIndicator.SetUpSlider(count);
    }
    public void StartGameplay()
    {
        GameplayStarted?.Invoke();
        _mergeStage.gameObject.SetActive(false);

        StartCoroutine(ShowUI(_FPSUI));
    }

    private IEnumerator ShowUI(CanvasGroup canvasGroup)
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSecondsRealtime(0.005f);
            canvasGroup.alpha = i / 100f;
        }
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    private IEnumerator HideUI(CanvasGroup canvasGroup)
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSecondsRealtime(0.005f);
            canvasGroup.alpha = 1 - (i / 100f);
        }
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    private void CheckIsLevelPassed()
    {
        _countOfAllEnemies--;
        if (_countOfAllEnemies <= 0)
        {
            StartCoroutine(ShowCompletedUI());
            
            LevelCompleted?.Invoke();
        }
    }
    private IEnumerator ShowCompletedUI()
    {
        yield return StartCoroutine(HideUI(_FPSUI));
        StartCoroutine(ShowUI(_levelCompletedUI));
    }
}