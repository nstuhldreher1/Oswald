using UnityEngine;

[CreateAssetMenu(fileName = "EncounterZoneData", menuName = "Scriptable Objects/EncounterZoneData")]
public class EncounterZoneData : ScriptableObject
{
    public string zoneName;
    public EnemyData[] enemyTypes;
}
