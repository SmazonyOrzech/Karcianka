using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;
    public int AttackPower { get; set; }
    public void Setup(EnemyData enemyData)
    {
        AttackPower = enemyData.Attack;
        UpdateAttackText();
        SetupBase(enemyData.Health, enemyData.Image);
    }
    private void UpdateAttackText()
    {
        attackText.text = "ATK: " + AttackPower;
    }
}
