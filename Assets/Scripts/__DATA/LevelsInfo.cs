using UnityEngine;


[CreateAssetMenu(fileName = "LevelsInfo", menuName = "ScriptableObjects/LevelsInfo", order = 1)]

public class LevelsInfo : ScriptableObject
{
    public LevelInfo[] LevelInfos;
}