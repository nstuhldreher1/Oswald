
using TMPro;
using UnityEngine;
public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text experienceText;
    private void OnEnable()
    {
        if(PlayerManager.instance == null)
            return;
        PlayerManager.instance.PlayerStatsChanged += UpdateHUD;
        UpdateHUD(); // Update HUD immediately when enabled
    }

    private void OnDisable()
    {
        if(PlayerManager.instance == null)
            return;
        PlayerManager.instance.PlayerStatsChanged -= UpdateHUD;
    }

    private void UpdateHUD()
    {
        if (PlayerManager.instance == null)
            return;
        healthText.text = "HP: " + PlayerManager.instance.GetCurrentPlayerHealth();
        levelText.text = "Lvl: " + PlayerManager.instance.GetCurrentPlayerLevel();
        experienceText.text = "EXP: " + PlayerManager.instance.GetCurrentPlayerExperience() + "/" + PlayerManager.instance.GetExperienceToNextLevel();

    }

}