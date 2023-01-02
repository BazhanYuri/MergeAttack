using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Transform _mergeStage;

    public event Action GameplayStarted;



    private void Awake()
    {
        Instance = this;
    }

    public void StartGameplay()
    {
        GameplayStarted?.Invoke();
        _mergeStage.gameObject.SetActive(false);
    }

}