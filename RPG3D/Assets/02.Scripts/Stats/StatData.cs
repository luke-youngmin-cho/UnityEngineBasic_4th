using UnityEngine;

[CreateAssetMenu(fileName = "new StatData", menuName = "RPG/StatData")]
public class StatData : ScriptableObject
{
    public int ID;
    public int Value;
    public int Min;
    public int Max;
}