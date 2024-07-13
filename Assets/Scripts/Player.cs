using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    [SerializeField] private float moveSpeed = 11f;

    private bool walk;

    private void Update() {

        Vector2 input = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            input.y = +1;
        } 
        
        if (Input.GetKey(KeyCode.A)) {
            input.x = -1;
        } 

        if (Input.GetKey(KeyCode.S)) {
            input.y = -1;
        } 

        if (Input.GetKey(KeyCode.D)) {
            input.x = +1;
        } 

        input = input.normalized;

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
