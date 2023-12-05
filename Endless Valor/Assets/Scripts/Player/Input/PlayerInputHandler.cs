using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //Movement properties
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedInputX { get; private set; }
    public int NormalizedInputY { get; private set; }
    
    //Jump properties
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }

    [SerializeField] private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    
    //Attack properties
    public bool PrimaryAttackInput { get; private set; }
    public bool SecondaryAttackInput { get; private set; }


    private void Update()
    {
        CheckJumpInputHoldTime();
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
            RawMovementInput = context.ReadValue<Vector2>();

            NormalizedInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormalizedInputY = Mathf.RoundToInt(RawMovementInput.y);
            
            Debug.Log(NormalizedInputY);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    
    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started && !PrimaryAttackInput)
        {
            PrimaryAttackInput = true;
        }
    }

    public void UsePrimaryAttackInput() => PrimaryAttackInput = false;
    
    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SecondaryAttackInput = true;
        }
    }

    public void UseSecondaryAttackInput() => SecondaryAttackInput = false;
}
