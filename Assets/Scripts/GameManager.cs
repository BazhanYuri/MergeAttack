using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private CanvasGroup _FPSUI;
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

        StartCoroutine(ShowFPSUI());
    }

    private IEnumerator ShowFPSUI()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.005f);
            _FPSUI.alpha = i / 100f;
        }
        _FPSUI.interactable = true;
        _FPSUI.blocksRaycasts = true;
    }
}