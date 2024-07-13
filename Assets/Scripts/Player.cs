using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool walk;

    private void Update() {
        Vector2 input = gameInput.GetMovementVectorNormalized();

        Vector3 movementDirection = new Vector3(input.x, 0f, input.y);
        transform.position += moveSpeed * movementDirection * Time.deltaTime;

        walk = movementDirection != Vector3.zero;
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);

    }

    public bool IsWalking() {
        return walk;
    }
}
