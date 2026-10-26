using UnityEngine;


[System.Serializable]
public class EncounterEntry
{
    public EnemyData enemy;
    [Min(0)] public int weight = 1;
}

[CreateAssetMenu(fileName = "EncounterZoneData", menuName = "Scriptable Objects/EncounterZoneData")]
public class EncounterZoneData : ScriptableObject
{
    public string zoneName;
    public EncounterEntry[] encounterEntries;

    public int levelMin;
    public int levelMax;
}
