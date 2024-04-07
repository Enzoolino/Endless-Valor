using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;


public class Controlelrs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            if (gamepad is DualShockGamepad)
            {
                Debug.Log("PS");
            }
                
            if (gamepad is XInputController)
            {
                Debug.Log("Xbox");
            }
        }

    }

    
}
