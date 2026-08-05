using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Effect
{
    public abstract GameAction GetGameAction(List<CombatantView> targets);
}
