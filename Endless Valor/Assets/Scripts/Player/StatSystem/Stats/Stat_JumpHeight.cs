
public class Stat_JumpHeight : StatSystem_Core
{
    public Stat_JumpHeight(float baseValue, float maximumValue, float triggerEffectValue) : base(baseValue, maximumValue, triggerEffectValue)
    {
    }


    public override void EnableTriggerEffect()
    {
        base.EnableTriggerEffect();

        //For now stat is dependant only on fall damage. Special effect can be added here.
        if (player != null)
        {
            if (player.jumpHeightWorkspace >= TriggerEffectValue)
            {
                
            }
        }
        
    }
}
