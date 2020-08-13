using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private SpriteAnimator spriteAnimator; 

    [SerializeField]
    private float movementSpeed = 10f;

    private Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/PlayerCharacter");
    private KeyCode[] movementKeys = new KeyCode[4] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D }; 

    public static Vector2 movementVector; 

    private void Start()
    {
        
    }

    private void IsKeyIsolated(KeyCode isolatedKey)
    {
        foreach (KeyCode key in movementKeys)
        {
            if (Input.GetKeyDown(key))
            {

            }
        }
    }

    // Get input in Update 
    private void Update()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical"); 
        
    }

    // Move player in FixedUpdate 
    private void FixedUpdate()
    {
        // Adjust for diagonal input 
        if (movementVector.magnitude > 1f)
        {
            movementVector /= movementVector.magnitude;
        }

        gameObject.transform.position += new Vector3(movementVector.x, movementVector.y, 0f) * movementSpeed * Time.fixedDeltaTime;

        //if (Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A))

    }
}
