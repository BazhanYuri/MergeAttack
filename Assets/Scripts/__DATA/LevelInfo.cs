using UnityEngine;


[CreateAssetMenu(fileName = "LevelInfo", menuName = "ScriptableObjects/LevelInfo", order = 1)]

public class LevelInfo : ScriptableObject
{
    public FPS.LevelVisual LevelVisualPrefab;
    public int SoldierCount;
    public int DronsCount;
    public int JahernautsCount;
    public int CoptersCount;
    public float MinTimeToSpawn;
    public float MaxTimeToSpawn;
    public Material SkyBox;
}