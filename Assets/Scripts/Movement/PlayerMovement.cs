using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up, upleft, left, downleft, down, downright, right, upright
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private SpriteAnimator spriteAnimator; 

    [SerializeField]
    private float movementSpeed = 10f;

    public static Vector2 movementVector;

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
        //spriteAnimator.setanim(direction);
        gameObject.transform.position += new Vector3(movementVector.x, movementVector.y, 0f) * movementSpeed * Time.fixedDeltaTime;
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
}
