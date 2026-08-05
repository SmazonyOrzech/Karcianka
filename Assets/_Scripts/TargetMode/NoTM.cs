using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NoTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        return null;
    }
}
