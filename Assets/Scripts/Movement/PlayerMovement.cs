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
    [SerializeField]
    private SpriteAnimator spriteAnimator;

    [SerializeField]
    private float movementSpeed = 100f;  

    public static Vector2 movementVector;

    public Camera mainCamera; 

    // Get input in Update 
    private void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        SetDirection();
        LogMovementData(); 
    }

    // Move player in FixedUpdate 
    private void FixedUpdate()
    {
        // Adjust for diagonal input 
        if (movementVector.magnitude > 1f)
        {
            movementVector /= movementVector.magnitude;
        }

        //spriteAnimator.setanim(direction);

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

    private void LogMovementData()
    {
        Debug.Log("Movement vector: " + movementVector);
        Debug.Log("Movement speed: " + movementSpeed);

        Debug.Log("Camera xMin: " + mainCamera.rect.xMin);
        Debug.Log("Camera xMax: " + mainCamera.rect.xMax);
        Debug.Log("Camera yMin: " + mainCamera.rect.yMin);
        Debug.Log("Camera yMax: " + mainCamera.rect.yMax);
    }

    private void MovePlayer()
    { 
        Vector3 newPosition = gameObject.transform.position + new Vector3(movementVector.x, movementVector.y, 0f) 
            * movementSpeed * Time.fixedDeltaTime;

        gameObject.transform.position = newPosition;

        // Clamp character to current room  
        //gameObject.transform.position = new Vector3(
        //    Mathf.Clamp(newPosition.x, mainCamera.rect.xMin, mainCamera.rect.xMax),
        //    Mathf.Clamp(newPosition.y, mainCamera.rect.yMin, mainCamera.rect.yMax),
        //    0f); 
    }
}
