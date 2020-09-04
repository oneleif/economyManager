using System; 
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    private float frameRate = 0.15f;

    // The sprites to loop through when moving  
    public Sprite[] upFrames;
    public Sprite[] leftFrames;
    public Sprite[] downFrames; 
    public Sprite[] rightFrames;

    // Stores the currently looping sprites 
    private Sprite[] frameArray;

    // The sprites displayed when stationary  
    // Indices are: 0-3 WASD
    public Sprite[] stationaryFrames;

    public Material material;

    // Stores the current stationary sprite
    private Sprite stationaryPosition;  
    
    private int currentFrame;
    private float timer;
    private bool flipped;

    public static Direction direction; 

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Default stationary position is facing down 
        stationaryPosition = stationaryFrames[2];
        material.mainTexture = stationaryPosition.texture;

        // Set frame rate proportionally to movement speed 
        // Enable this when not Serialized
        // And set movementSpeed to static member*
        //frameRate = PlayerMovement.movementSpeed / 1000; 

    }

    void FixedUpdate()
    {
        if (PlayerMovement.movementVector == Vector2.zero)
        {
            material.mainTexture = stationaryPosition.texture;
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
                //spriteRenderer.flipX = flipped ? true : false;
                material.mainTextureScale = flipped ? new Vector2(-1f, 1f) : Vector2.one;

                // Update the sprite being renderer
                //spriteRenderer.sprite = frameArray[currentFrame]; 
                material.mainTexture = frameArray[currentFrame].texture;
            }
        }
    }

    // Assign frameArray to the frames corresponding to 
    // the player's current direction 
    private void UpdateFrameArray()
    {
        // Ugliest switch block in existence 
        switch (direction)
        {
            case Direction.up:
                frameArray = upFrames;
                stationaryPosition = stationaryFrames[0];
                flipped = false;
                break;
            case Direction.upleft:
                frameArray = leftFrames;
                stationaryPosition = stationaryFrames[1];
                flipped = false;
                break;
            case Direction.left:
                frameArray = leftFrames;
                stationaryPosition = stationaryFrames[1];
                flipped = false;
                break;
            case Direction.downleft:
                frameArray = leftFrames;
                stationaryPosition = stationaryFrames[1];
                flipped = false;
                break;
            case Direction.down:
                frameArray = downFrames;
                stationaryPosition = stationaryFrames[2];
                flipped = false;
                break;
            case Direction.downright:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[3];
                flipped = false;
                break;
            case Direction.right:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[3];
                flipped = false;
                break;
            case Direction.upright:
                frameArray = rightFrames;
                stationaryPosition = stationaryFrames[3];
                flipped = false;
                break;
        }
    }
}
