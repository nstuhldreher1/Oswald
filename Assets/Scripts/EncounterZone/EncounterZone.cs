using UnityEngine;

public class EncounterZone : MonoBehaviour
{
    [SerializeField] private EncounterZoneData encounterZoneData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player entered encounter zone: " + encounterZoneData.zoneName);
            EncounterZoneManager.instance.SetIsPlayerInEncounterZone(true);
            EncounterZoneManager.instance.SetCurrentEncounterZoneData(encounterZoneData);
            EncounterZoneManager.instance.PreRandomizeEnemy();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player left encounter zone: " + encounterZoneData.zoneName);
            EncounterZoneManager.instance.SetIsPlayerInEncounterZone(false);
            EncounterZoneManager.instance.SetCurrentEncounterZoneData(null);
        }
    }
}