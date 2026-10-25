using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public int speed;
    public int attack;
    public int baseExperienceReward;
    public Sprite enemySprite;
}
