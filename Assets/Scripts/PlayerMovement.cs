using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpHeight = 100;
    public Rigidbody2D target;
    private CharacterAnimation characterAnimation;
    private int jumpCount = 1;

    private void Awake()
    {
        target = GetComponent<Rigidbody2D>();
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal < 0)
        {
            // Flip X the player
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            // The Flip X make right movement become left movement. Thats why i use Vector2.right
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            characterAnimation.AnimateWalk(true);
        }
        if (moveHorizontal > 0)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            characterAnimation.AnimateWalk(true);
        }
        if(moveHorizontal == 0 && !Input.anyKeyDown)
        {
            characterAnimation.AnimateWalk(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(target.IsTouchingLayers() && jumpCount == 1)
            {
                transform.Translate(Vector2.up * jumpHeight * Time.deltaTime);
                jumpCount -= 1;
            }else if(target.IsTouchingLayers() && jumpCount == 0)
            {
                transform.Translate(Vector2.up * jumpHeight * Time.deltaTime);
                jumpCount += 1;
            }
        }
    }
}
