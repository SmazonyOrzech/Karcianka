using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllEnemyTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        return new(EnemySystem.Instance.Enemies);
    }
}
