using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class TargetMode
{
    public abstract List<CombatantView> GetTargets();
}
