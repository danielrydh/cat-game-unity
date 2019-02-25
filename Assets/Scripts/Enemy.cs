using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float downSpeed = -20f;

    [SerializeField] float firstJumpSpeed = 20f;
    [SerializeField] float secondJumpSpeed = 5f;
    private bool canJump = true;
    private float jumpSpeed;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myColider;
    public GameObject player;
    public Transform target;



    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myColider = GetComponent<Collider2D>();
        jumpSpeed = firstJumpSpeed;
        player = GameObject.Find("Player");
        target = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Down();
        RunAi();
        Jump();
        Landing();
        FlipSprite();
        Win();
        
    }

    private void RunAi()
    {
        if (target.position.x == transform.position.x)
        {
            return;
        }
            else if (target.position.x < transform.position.x)
        {
            Run(-1);
        } else if (target.position.x > transform.position.x)
        {
            Run(1);
        }




    }

    private void Run(int v)
        
    {
        if (target.position.y < transform.position.y && Vector2.Distance(transform.position, transform.position) < 1)
        {
            Vector2 playerVelocity = new Vector2(v * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerHasHorizontalSpeed);
        } else
        {
            Vector2 playerVelocity = new Vector2(-v * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("Running", playerHasHorizontalSpeed);
        }

    }

    public void Jump()
    {
        
        bool grounded = myColider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (grounded)
        {
            canJump = true;
        }

        if (Vector2.Distance(transform.position, transform.position) < 3)
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

    private void JumpOrDown()
    {
        if (Vector2.Distance(transform.position, transform.position) < 0.5)
        {
            Down();
        } else if (Vector2.Distance(transform.position, transform.position) < 3)
        {
            Jump();
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

    public void Down()
    {
 
        bool playerHasVerticaltalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        if (playerHasVerticaltalSpeed && target.position.y == transform.position.y)
        {
            myAnimator.SetBool("Down", true);
            myAnimator.SetBool("Jumping", false);
            Vector2 downVelocity = new Vector2(0f, downSpeed);
            myRigidbody.velocity = downVelocity;
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

        if (EnemyHead)
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 2);
        }

    }
}
