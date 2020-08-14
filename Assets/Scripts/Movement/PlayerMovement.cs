using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private SpriteAnimator spriteAnimator; 

    [SerializeField]
    private float movementSpeed = 10f;

    public static Vector2 movementVector; 
    
    //enum Direction
    //{
    //    left, right, up, down, upleft, etc
    //}

    // Get input in Update 
    private void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        //if(movementVector.y > 0 && movementVector.x == 0)
        //{

        //}
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
}
