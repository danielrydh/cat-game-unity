using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player2 : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;

    [SerializeField] float firstJumpSpeed = 20f;
    [SerializeField] float secondJumpSpeed = 5f;
    [SerializeField] float downSpeed = -20f;
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
        Landing();
        FlipSprite();
        Win();
    }

    public void Run()
    {
        Vector2 playerVelocity = new Vector2(runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    public void RunLeft()
    {
        Vector2 playerVelocity = new Vector2(-runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    public void Jump()
    {
        
        bool grounded = myColider.IsTouchingLayers(LayerMask.GetMask("Ground"));


        if (grounded)
        {
            canJump = true;
        }

   
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


    public void Down()
    {
        
        bool playerHasVerticaltalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        if (playerHasVerticaltalSpeed)
        {
            myAnimator.SetBool("Down", true);
            myAnimator.SetBool("Jumping", false);
            Vector2 downVelocity = new Vector2(0f, downSpeed);
            myRigidbody.velocity = downVelocity;
        }

    }



    private void Landing()
    {
        if (myAnimator.GetBool("Jumping") && myRigidbody.velocity.y == 0 || myAnimator.GetBool("Down") && myRigidbody.velocity.y == 0)
        {
            myAnimator.SetBool("Landed", true);
            myAnimator.SetBool("Jumping", false);
            myAnimator.SetBool("Down", false);

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

    private void Win()
    {

        bool EnemyHead = myColider.IsTouchingLayers(LayerMask.GetMask("EnemyHead"));
        var down = myAnimator.GetBool("Down");

        if (EnemyHead && down)
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

    }
}
