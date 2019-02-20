using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Enemy : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;

    [SerializeField] float firstJumpSpeed = 20f;
    [SerializeField] float secondJumpSpeed = 5f;
    private bool canJump = true;
    private float jumpSpeed;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myColider;



    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myColider = GetComponent<Collider2D>();
        jumpSpeed = firstJumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Landing();
        FlipSprite();
    }

    private void Run()
    {

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);


    }

    private void Jump()
    {
        bool jumped = CrossPlatformInputManager.GetButtonDown("Jump");
        bool grounded = myColider.IsTouchingLayers(LayerMask.GetMask("Ground"));


        if (grounded)
        {
            canJump = true;
        }

        if (jumped)
        {
            if (canJump)
            {
                myAnimator.SetBool("Landed", false);
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity += jumpVelocity;
                bool playerHasVerticaltalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
                myAnimator.SetBool("Jumping", playerHasVerticaltalSpeed);
                canJump = false;

                jumpSpeed = secondJumpSpeed;
            }
        }

    }



    private void Landing()
    {
        if (myAnimator.GetBool("Jumping") && myRigidbody.velocity.y == 0)
        {
            myAnimator.SetBool("Landed", true);
            myAnimator.SetBool("Jumping", false);

            jumpSpeed = firstJumpSpeed;

        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
