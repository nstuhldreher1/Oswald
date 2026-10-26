
using TMPro;
using UnityEngine;
public class EnemyHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text nameText;
    private void OnEnable()
    {
        if(EnemyManager.instance == null)
            return;
        EnemyManager.instance.EnemyStatsChanged += UpdateHUD;
        UpdateHUD(); 
    }

    private void OnDisable()
    {
        if(EnemyManager.instance == null)
            return;
        EnemyManager.instance.EnemyStatsChanged -= UpdateHUD;
    }

    private void UpdateHUD()
    {
        if (EnemyManager.instance == null)
            return;
        healthText.text = "HP: " + EnemyManager.instance.GetCurrentEnemyHealth();
        levelText.text = "Lvl: " + EnemyManager.instance.GetCurrentEnemyLevel();
        nameText.text = EnemyManager.instance.GetCurrentEnemyName();

    }

}