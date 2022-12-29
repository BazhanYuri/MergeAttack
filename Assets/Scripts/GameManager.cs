using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action GameplayStarted;



    private void Awake()
    {
        Instance = this;
    }

    public void StartGameplay()
    {
        GameplayStarted?.Invoke();
    }

}