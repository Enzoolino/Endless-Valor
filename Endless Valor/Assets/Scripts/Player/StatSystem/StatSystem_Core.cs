using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatSystem_Core
{
    public float BaseValue { get; protected set; }
    public float CurrentValue { get; protected set; }
    public float MaximumValue { get; protected set; }
    public float TriggerEffectValue { get; protected set; }

    public StatSystem_Core(float baseValue, float maximumValue, float triggerEffectValue)
    {
        BaseValue = baseValue;
        CurrentValue = baseValue;
        MaximumValue = maximumValue;
        TriggerEffectValue = triggerEffectValue;
    }
    
    public void Add(float modifier)
    {
        Mathf.Clamp(CurrentValue, BaseValue, MaximumValue);
        CurrentValue += modifier;
    }

    public void Subtract(float modifier)
    {
        Mathf.Clamp(CurrentValue, BaseValue, MaximumValue);
        CurrentValue -= modifier;
    }

    public void Multiply(float multiplicator)
    {
        Mathf.Clamp(CurrentValue, BaseValue, MaximumValue);
        CurrentValue *= multiplicator;
    }

    public void Divide(float divider)
    {
        Mathf.Clamp(CurrentValue, BaseValue, MaximumValue);
        CurrentValue /= divider;
    }

    public virtual void TriggerEffect()
    {
        
    }
    
    public void ResetStatToBaseValue()
    {
        CurrentValue = BaseValue;
    }
    
    
}
