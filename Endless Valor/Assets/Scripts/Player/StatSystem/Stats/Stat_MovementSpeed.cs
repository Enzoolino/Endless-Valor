
public class Stat_MovementSpeed : StatSystem_Core
{
    //NOTE: Current Value is working as Acceleration value in this state. It changes to what amount the player can 
    //speed up but Base Value is always a starting point.

    private AttackDetails attackDetails;
    protected bool wallCheck;
    
    public Stat_MovementSpeed(float baseValue, float maximumValue, float triggerEffectValue) : base(baseValue, maximumValue, triggerEffectValue)
    {
    }
    
    public override void EnableTriggerEffect()
    {
        base.EnableTriggerEffect();
        
        if (player != null)
        {
            if (player.movementSpeedWorkspace >= TriggerEffectValue)
            {
                attackDetails.damageAmount = (player.movementSpeedWorkspace - TriggerEffectValue + 1) * 5;
                wallCheck = player.CheckIfTouchingWall();

                if (wallCheck)
                {
                    player.movementSpeedWorkspace = 0f;
                    player.TakeDamage(attackDetails); //TODO: Change the way of handling that as it's not an attack 
                }
            }
        }
    }
    
    
}
