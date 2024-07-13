using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool walk;

    private void Update() {
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

        walk = moveDirection != Vector3.zero;
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

    }

    public bool IsWalking() {
        return walk;
    }
}
