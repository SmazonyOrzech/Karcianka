using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singelton<EnemySystem>
{
    [SerializeField] private EnemyBoardView enemyBoardView;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
    }
    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
    }
    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach(var enemy in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemy);
        }
    }
    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn");
        yield return new WaitForSeconds(2f);
        Debug.Log("End Enemy Turn");
    }
}
