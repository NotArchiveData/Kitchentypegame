using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counter_mask;

    private bool is_walking;
    private Vector3 last_interacted_direction;
    private ClearCounter selected_counter;

    private void Start() {
        gameInput.on_interact_action += gameInput_on_interact_action;
    }

    private void gameInput_on_interact_action(object sender, System.EventArgs e) {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);

        if (moveDirection != Vector3.zero) {
            last_interacted_direction = moveDirection;
        }

        float interact_distance = 2f;
        if (Physics.Raycast(transform.position, last_interacted_direction, out  RaycastHit raycast_hit, interact_distance, counter_mask)) {
            if (raycast_hit.transform.TryGetComponent(out ClearCounter clear_counter)) {
                // Has clear counter
                if (clear_counter != selected_counter) {
                    selected_counter = clear_counter;
                }
            }
        }
        else {
            selected_counter = null;
        }

    }
    

    private void Update() {
        handle_movement();
        handle_interactions();
    }

    public bool IsWalking() {
        return is_walking;
    }

    private void handle_interactions() {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);

        if (moveDirection != Vector3.zero) {
            last_interacted_direction = moveDirection;
        }

        float interact_distance = 2f;
        if (Physics.Raycast(transform.position, last_interacted_direction, out  RaycastHit raycast_hit, interact_distance, counter_mask)) {
            if (raycast_hit.transform.TryGetComponent(out ClearCounter clear_counter)) {
                // Has clear counter
                
            }
        }

    }
    private void handle_movement() {
        Vector2 input = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove) {
            // Cannot move ur stuck L

            // Move to the X axis instead
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove) {
                // moving X axis
                moveDirection = moveDirectionX;
            }

            else {
                // cannot move X

                // Now go ZZZZZ
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove) {
                    // moving Z
                    moveDirection = moveDirectionZ;
                }

                else {
                    // ur actually stuck loser
                }

            }
        }

        if (canMove) {
            transform.position += moveDirection * moveDistance;
        }

        is_walking = moveDirection != Vector3.zero;
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }
}
