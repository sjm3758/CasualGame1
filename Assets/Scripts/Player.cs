using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float speed = 5.0f;
    private float jumpForce = 6.0f;
    private bool facingRight = true;
    private bool grounded = false;
    private float deltaX;
    private float maxXSpeed = 10.0f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("switch");
        if (collision.gameObject.tag == "Switch")
        {
            this.gameObject.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
        }

        grounded = true;
    }

    void Move()
    {
        Vector3 pos = this.gameObject.GetComponent<Transform>().position;
        Vector2 velocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
        //move right
        //if (Input.GetKey(KeyCode.D))
        //{
        //    velocity.x += speed * Time.deltaTime;
        //}
        ////move left
        //if (Input.GetKey(KeyCode.A))
        //{
        //    velocity.x -= speed * Time.deltaTime;
        //}
        //MOVE LEFT/RIGHT
        deltaX = Input.GetAxis("Horizontal");
        //decelerate
        //if (Mathf.Abs(velocity.x) > 0.0f)
        //{
        //    velocity.x *= 0.9f;
        //    //if (Mathf.Abs(velocity.x) < 0) velocity.x = 0;
        //}
        //jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                velocity.y = jumpForce;
                grounded = false;
            }
        }

        this.gameObject.GetComponent<Transform>().position = pos;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(deltaX * speed, velocity.y);
    }

    void TurnAround()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
