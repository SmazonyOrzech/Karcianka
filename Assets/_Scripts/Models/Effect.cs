using System;
using UnityEngine;

[Serializable]
public abstract class Effect
{
    public abstract GameAction GetGameAction();
}
