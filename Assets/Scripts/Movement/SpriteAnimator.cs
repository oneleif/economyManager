using System; 
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // The sprites to loop through when moving  
    public Sprite[] upFrames;
    public Sprite[] downFrames; 
    public Sprite[] rightFrames;

    // Stores the currently looping sprites 
    private Sprite[] frameArray;

    // The sprites displayed when stationary  
    // Indices are: 0 for facing down, 1 for facing up 
    public Sprite[] stationaryFrames;

    // Stores the current stationary sprite
    private Sprite stationaryPosition;  

    [SerializeField]
    private float frameRate = 0.1f;

    private int currentFrame;
    private float timer;

    private bool flipped; 

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Default stationary position is facing down 
        stationaryPosition = stationaryFrames[0]; 
        spriteRenderer.sprite = stationaryPosition; 

    }

    void FixedUpdate()
    {
        if (PlayerMovement.movementVector == Vector2.zero)
        {
            spriteRenderer.sprite = stationaryPosition; 
        }
        else
        {
            if (PlayerMovement.movementVector == Vector2.up)
            {
                frameArray = upFrames;
                stationaryPosition = stationaryFrames[1]; 
                flipped = false; 

            }
            else if (PlayerMovement.movementVector == Vector2.left)
            {
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[0]; 
                flipped = true; 
            }
            else if (PlayerMovement.movementVector == Vector2.down)
            {
                frameArray = downFrames;
                stationaryPosition = stationaryFrames[0]; 
                flipped = false; 
            }
            else if (PlayerMovement.movementVector == Vector2.right)
            {
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = false; 
            }

            timer += Time.fixedDeltaTime;

            // Do something every "frame",
            // according to the frameRate defined above 
            if (timer >= frameRate)
            {
                timer -= frameRate;

                // Reset currentFrame to 0 when it reaches the length of the array 
                currentFrame = (currentFrame + 1) % frameArray.Length;

                // Flip sprite if moving left 
                spriteRenderer.flipX = flipped ? true : false; 

                // Update the sprite being renderer
                spriteRenderer.sprite = frameArray[currentFrame]; 
            }
        }
    }

    // Would be cleaner to get the corresponding frame array here.
    // But how do we set this up to account for stationary sprites?
    private Sprite[] GetFrameArray(Vector2 movementVector)
    {
        if (movementVector == Vector2.up)
        {
            return upFrames; 
        }   
        else if (movementVector == Vector2.left)
        {
            return rightFrames; 
        }
        else if (movementVector == Vector2.down)
        {
            return downFrames;
        }
        else if (movementVector == Vector2.right)
        {
            return rightFrames;
        }
        else
        {
            return new Sprite[0]; 
        }
    }
}
