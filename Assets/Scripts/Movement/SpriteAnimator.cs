using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // The sprites displayed when stationary  
    private Sprite upStationary;
    private Sprite leftStationary; 
    private Sprite downStationary;
    private Sprite rightStationary;

    // The sprites to loop through when moving  
    public Sprite[] upFrames; 
    public Sprite[] leftFrames;
    public Sprite[] downFrames; 
    public Sprite[] rightFrames;

    // Stores the currently looping sprites 
    private Sprite[] frameArray; 

    private int currentFrame;
    private float frameRate = 0.1f;
    private float timer;
    private bool isPlaying = true; 
    private bool isLooping = false; 

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //downStationary = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (1).png");
        //upStationary = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (4).png"); 

        //// We can copy the reference here as we are not transforming this sprite 
        //rightStationary = downStationary;

        //leftStationary = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (1).png"); 



        //downFrames[0] = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (2).png");
        //downFrames[1] = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (3).png");
        //upFrames[0] = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (5).png"); 
        //upFrames[1] = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (6).png");
        //rightFrames[0] = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (7).png");
        //rightFrames[0] = Resources.Load<Sprite>("Sprites/PlayerCharacter/pixel-16x16 (8).png");
    }

    void FixedUpdate()
    {
        if (PlayerMovement.movementVector == Vector2.zero)
        {
            return; 
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            frameArray = rightFrames;

            //if (Input.GetKeyUp(KeyCode.D))
            //{
            //    isPlaying = false; 
            //}
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            frameArray = upFrames; 
        }

        //if (!isPlaying)
        //{
        //    return;
        //}

        timer += Time.fixedDeltaTime;

        // Do something every "frame",
        // according to the frameRate defined above 
        if (timer >= frameRate)
        {
            timer -= frameRate;

            // Reset currentFrame to 0 when it reaches the length of the array 
            currentFrame = (currentFrame + 1) % frameArray.Length;

            //if (PlayerMovement.movementVector == Vector2.zero)
            //{
            //    isPlaying = false; 
            //}
            //else
            //{
            //    spriteRenderer.sprite = frameArray[currentFrame];
            //}

            spriteRenderer.sprite = frameArray[currentFrame]; 
        }
    }

    private IEnumerator PlayAnimation(KeyCode key)
    {


        while (Input.GetKeyDown(key))
        {
            // Play corresponding animation

            // Indefinite cycle through the sprites 
            yield return null; 
        }

        //switch(key)
        //{

        //}
    }

    //private void PlayAnimation()
    //{

    //}

    private void ToggleAnimation(bool play)
    {
        isPlaying = play;
    }

}
