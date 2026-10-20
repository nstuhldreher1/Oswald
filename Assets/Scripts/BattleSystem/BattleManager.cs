using System.Collections;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance { get; private set; }
    private EnemyData currentEnemyData;
    private GameObject dialoguePanel;
    private TMP_Text dialogueText;
    private GameObject battleOptionsPanel;
    private TMP_Text playerHealthText;
    private TMP_Text enemyHealthText;
    private SpriteRenderer currentEnemySprite;
    private Animator attackAnimator;
    
    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        dialoguePanel = GameObject.Find("Battle Dialogue");
        dialogueText = dialoguePanel.transform.Find("Dialogue Text").GetComponent<TMP_Text>();
        battleOptionsPanel = GameObject.Find("Character Panel");
        currentEnemySprite = GameObject.Find("Enemy Sprite").GetComponent<SpriteRenderer>();
        enemyHealthText = GameObject.Find("Enemy Health").GetComponent<TMP_Text>();
        playerHealthText = GameObject.Find("HP Number").GetComponent<TMP_Text>();
        attackAnimator = GameObject.Find("AttackAnimation").GetComponent<Animator>();

        instance = this;
    }

    private void Start()
    {
        currentEnemyData = EncounterZoneManager.instance?.GetCurrentEnemyData();
        if (currentEnemyData == null)
        {
            Debug.LogWarning("No current enemy data found.");
            return;
        }

        EnemyManager.instance.SetEnemyData(currentEnemyData);

        currentEnemySprite.sprite = currentEnemyData.enemySprite;
        UpdateEnemyHealthUI();
        UpdatePlayerHealthUI();



        dialoguePanel.SetActive(true);
        battleOptionsPanel.SetActive(false);

        if (EnemyManager.instance.GetCurrentEnemySpeed() > PlayerManager.instance.GetCurrentPlayerSpeed())
        {
            isPlayerTurn = true;
        }
        else
        {
            isPlayerTurn = false;
        }

        StartCoroutine(BattleIntro());
        Debug.Log("Enemy: " + EnemyManager.instance.GetCurrentEnemyName());
    }

    private IEnumerator BattleIntro()
    {
        yield return StartCoroutine(TypeWriterEffect(
            "A wild " + EnemyManager.instance.GetCurrentEnemyName() + " appeared!",
            0.05f));
        NextTurn();
    }

    public void NextTurn()
    {
        Debug.Log("Battle started between player and " + EnemyManager.instance.GetCurrentEnemyName());
        // Implement battle logic here
        if (isPlayerTurn)
        {
            Debug.Log("It's the player's turn.");
            // Player's turn logic
            dialoguePanel.SetActive(false);
            battleOptionsPanel.SetActive(true);
        }
        else
        {
            Debug.Log("It's the enemy's turn.");
            // Enemy's turn logic
            StartCoroutine(EnemyTurn());
        }
    }

    private void UpdatePlayerHealthUI()
    {
        playerHealthText.text = "HP: " + PlayerManager.instance.GetCurrentPlayerHealth().ToString();
    }

    private void UpdateEnemyHealthUI()
    {
        enemyHealthText.text = "HP: " + EnemyManager.instance.GetCurrentEnemyHealth().ToString();
    }

    public void PlayerAttack()
    {
        if (!isPlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        int damage = calculateDamage(PlayerManager.instance.GetCurrentPlayerAttack());
        EnemyManager.instance.SetCurrentEnemyHealth(EnemyManager.instance.GetCurrentEnemyHealth() - damage);
        battleOptionsPanel.SetActive(false);
        dialoguePanel.SetActive(true);
        yield return StartCoroutine(animateAttack());
        UpdateEnemyHealthUI();
        yield return StartCoroutine(TypeWriterEffect("Oswald attacks " + EnemyManager.instance.GetCurrentEnemyName() + " for " + damage + " damage.", 0.05f));
        Debug.Log("Player attacks " + EnemyManager.instance.GetCurrentEnemyName() + " for " + damage + " damage.");
        if (EnemyManager.instance.GetCurrentEnemyHealth() <= 0)
        {
            Debug.Log("Enemy defeated!");
            yield return StartCoroutine(TypeWriterEffect(EnemyManager.instance.GetCurrentEnemyName() + " has been defeated!", 0.05f));
            EndBattle();
            yield break;
        }

        Debug.Log("Enemy health: " + EnemyManager.instance.GetCurrentEnemyHealth());
        isPlayerTurn = false;
        
       NextTurn();
    }
    private IEnumerator TypeWriterEffect(string message, float delay)
    {
        dialogueText.text = ""; // Clear the text before adding new letters
        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1f); // Wait for a second after the message is fully displayed
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Player health: " + PlayerManager.instance.GetCurrentPlayerHealth());
        int damage = calculateDamage(EnemyManager.instance.GetCurrentEnemyAttack());
        PlayerManager.instance.SetCurrentPlayerHealth(PlayerManager.instance.GetCurrentPlayerHealth() - damage);
        battleOptionsPanel.SetActive(false);
        dialoguePanel.SetActive(true);
        UpdatePlayerHealthUI();
        yield return StartCoroutine(TypeWriterEffect(EnemyManager.instance.GetCurrentEnemyName() + " attacks Oswald for " + damage + " damage.", 0.05f));
        Debug.Log(EnemyManager.instance.GetCurrentEnemyName() + " attacks player for " + damage + " damage.");
        if (PlayerManager.instance.GetCurrentPlayerHealth() <= 0)
        {
            Debug.Log("Player defeated!");
            DataPersistenceManager.instance.SetLoadCheckpoint(true);
            yield return StartCoroutine(TypeWriterEffect("Oswald has been defeated!", 0.05f));
            EndBattle();
        }
        else
        {
            Debug.Log("Player health: " + PlayerManager.instance.GetCurrentPlayerHealth());
            isPlayerTurn = true;
            NextTurn();
        }
    }

    public IEnumerator RunFromBattle()
    {
        float roll = Random.value;
        if(roll <= 0.5f) // 50% chance to successfully run
        {
            Debug.Log("Player successfully ran from battle.");
            DataPersistenceManager.instance.SetLoadCheckpoint(false);
            dialoguePanel.SetActive(true);
            battleOptionsPanel.SetActive(false);
            yield return StartCoroutine(TypeWriterEffect("Oswald successfully ran from battle!", 0.05f));
            EndBattle();
        }
        else
        {
            Debug.Log("Player failed to run from battle.");
            isPlayerTurn = false;
            dialoguePanel.SetActive(true);
            battleOptionsPanel.SetActive(false);
            yield return StartCoroutine(TypeWriterEffect("Oswald failed to run from battle!", 0.05f));
            StartCoroutine(EnemyTurn());
        }
    }

    private int calculateDamage(int attack)
    {
        if (attack <= 0)
        {
            return 0;
        }

        int minDamage = Mathf.CeilToInt(attack * 0.75f);
        int maxDamage = Mathf.FloorToInt(attack * 1.25f);
        return Random.Range(minDamage, maxDamage + 1);
    }

    private IEnumerator animateAttack()
    {
        if (attackAnimator != null)
        {
            attackAnimator.Play("Base Layer.Attack", 0, 0f);
        }else
        {
            Debug.LogWarning("Attack Animator is not assigned.");
        }
        yield return null;
    }

    public void EndBattle()
    {
         SceneManager.instance.LoadScene("SampleScene");
    }
}
