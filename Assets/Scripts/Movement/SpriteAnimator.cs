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

    public static Direction direction; 

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
            UpdateFrameArray(); 
                
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

    private void UpdateFrameArray()
    {
        // Ugliest switch block in existence 
        switch (direction)
        {
            case Direction.up:
                frameArray = upFrames;
                stationaryPosition = stationaryFrames[1];
                flipped = false;
                break;
            case Direction.upleft:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[1];
                flipped = true;
                break;
            case Direction.left:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = true;
                break;
            case Direction.downleft:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = true;
                break;
            case Direction.down:
                frameArray = downFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = false;
                break;
            case Direction.downright:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = false;
                break;
            case Direction.right:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = false;
                break;
            case Direction.upright:
                frameArray = upFrames;
                stationaryPosition = stationaryFrames[1];
                flipped = false;
                break;
        }
    }
}
