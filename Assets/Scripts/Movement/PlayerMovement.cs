using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    up, upleft, left, downleft, down, downright, right, upright
}

public class PlayerMovement : MonoBehaviour
{
    public static Vector2 movementVector;

    [SerializeField] private SpriteAnimator spriteAnimator;

    private float currentSpeed; 
    [SerializeField] private float maximumSpeed; 
    [SerializeField] private float acceleration;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }    

    // Get input in Update 
    private void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        SetDirection();
    }

    // Move player in FixedUpdate 
    private void FixedUpdate()
    {
        // Adjust for diagonal input 
        if (movementVector.magnitude > 1f)
        {
            movementVector /= movementVector.magnitude;
        }

        MovePlayer(); 
    }

    private void SetDirection()
    {
        if (movementVector == Vector2.up)
        {
            SpriteAnimator.direction = Direction.up;
        }
        else if (movementVector == new Vector2(-1f, 1f))
        {
            SpriteAnimator.direction = Direction.upleft;
        }
        else if (movementVector == Vector2.left)
        {
            SpriteAnimator.direction = Direction.left;
        }
        else if (movementVector == new Vector2(-1f, -1f))
        {
            SpriteAnimator.direction = Direction.downleft;
        }
        else if (movementVector == Vector2.down)
        {
            SpriteAnimator.direction = Direction.down;
        }
        else if (movementVector == new Vector2(1f, -1f))
        {
            SpriteAnimator.direction = Direction.downright;
        }
        else if (movementVector == Vector2.right)
        {
            SpriteAnimator.direction = Direction.right;
        }
        else if (movementVector == Vector2.one)
        {
            SpriteAnimator.direction = Direction.upright;
        }
    }

    private void MovePlayer()
    {
        if (currentSpeed < maximumSpeed)
        {
            currentSpeed += acceleration; 
        }

        if (movementVector == Vector2.zero)
        {
            currentSpeed = 0f;
        }

        Vector3 movement = new Vector3(movementVector.x, 0f, movementVector.y); 
        rb.MovePosition(transform.position + movement * currentSpeed * Time.fixedDeltaTime); 
    }

    private void LogMovementData()
    {
        Debug.Log("Movement vector: " + movementVector);
        Debug.Log("Current speed: " + currentSpeed);
        Debug.Log("Maximum speed: " + maximumSpeed);
    }
}
