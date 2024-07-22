using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameInput : MonoBehaviour {
    
    public event EventHandler on_interact_action;
    private PlayerInputActions player_input_actions;

    private void Awake() {
        player_input_actions = new PlayerInputActions();
        player_input_actions.Player.Enable();

        player_input_actions.Player.Interact.performed += interact_performed;
    }

    private void interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        on_interact_action?.Invoke( this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 input = player_input_actions.Player.Move.ReadValue<Vector2>();

        input = input.normalized;
        
        return input;
    }
}
