using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

    // max speed
    public float maxSpeed = 10f;

    // direction of sprite
    bool facingRight = true;


    // rigidbody and animator of player
    Rigidbody2D playerBody;
    Animator anim;

    // values for jumping
    bool onGround = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    // reference to camera and camera movement
    private Camera cam;
    private CameraMovement cameraMove;

    // reference to sky
    GameObject sky;

    // respawn point of player
    public float respawnX;
    public float respawnY;

	// Use this for initialization
	void Start ()
    {
        playerBody = gameObject.GetComponent<Rigidbody2D>();

        // get the animator
        anim = gameObject.GetComponent<Animator>();

        // get the camera and camera movement
        cam = FindObjectOfType<Camera>();
        cameraMove = FindObjectOfType<CameraMovement>();

        // get the parallax
        sky = GameObject.Find("Sky");

        // get start position
        respawnX = gameObject.GetComponent<Transform>().position.x;
        respawnY = gameObject.GetComponent<Transform>().position.y;
	}

	// update
    void FixedUpdate()
    {
        // move player
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        playerBody.velocity = new Vector2(move * maxSpeed, playerBody.velocity.y);

        // set the grounded bool
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", onGround);

        // flip sprite if necessarry and flip camera offset
        if(move > 0 && !facingRight)
        {
            Flip();
            cameraMove.flip = true;
            cameraMove.facingRight = facingRight;
        }
        else if(move < 0 && facingRight)
        {
            Flip();
            cameraMove.flip = true;
            cameraMove.facingRight = facingRight;
        }
    }

    void Update()
    {
        float jump = Input.GetAxis("Jump");

        if (onGround && jump > 0)
        {
            onGround = false;

            // Tell Animator to play Jump animation
            anim.SetBool("Ground", false);
            anim.SetBool("Jump", true);

            // Do the jump
            StartCoroutine(AnimateJump(jump));
        }
        else
        {
            // reset jump on animator
            anim.SetBool("Jump", false);
        }
    }

    // flip the sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        // flip camera's local position
        Vector3 camPos = cam.transform.localPosition;
        camPos.x *= -1;
        cam.transform.localPosition = camPos;

        // flip parallax
        Vector3 parallaxPos = sky.transform.localScale;
        parallaxPos.x *= -1;
        sky.transform.localScale = parallaxPos;

    }

    // jump animation then force
    IEnumerator AnimateJump(float jump)
    {
        anim.Play("Player_Jump");
        yield return new WaitForSeconds(0.05f); // wait for two seconds.

        // apply force
        playerBody.velocity = new Vector2(playerBody.velocity.x, jump * maxSpeed);
    }
}
