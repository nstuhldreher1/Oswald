using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public void FightButtonClicked()
    {
        BattleManager.instance.PlayerAttack();
    }
    public void RunButtonClicked()
    {
        StartCoroutine(BattleManager.instance.RunFromBattle());
    }
}