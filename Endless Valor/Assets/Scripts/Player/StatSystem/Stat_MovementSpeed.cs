using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_MovementSpeed : StatSystem_Core
{
    
    
    public Stat_MovementSpeed(float baseValue, float maximumValue, float triggerEffectValue) : base(baseValue, maximumValue, triggerEffectValue)
    {
    }
    
    public override void TriggerEffect()
    {
        base.TriggerEffect();
    }
}
