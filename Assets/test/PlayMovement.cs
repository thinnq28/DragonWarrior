using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private bool grounded;

    [SerializeField] private float speed;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        //gia tri truc ngang
        float horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (horizontalInput > 0.01f) //player di chuyển sang phải
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f) //player di chuyển sang trái
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", grounded);

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Wall jump logic
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            Jump();
        
    }

     private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }

}
