using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //keeps tarck of the player's starting location when they spawn into a level; helps with resetting location
    private Vector3 startLocation;
    //temp variable for adjusting the player's location in Move()
    private Vector3 pos;
    //the gamemanager in the scene (used to get materials)
    public GameObject gamemanager;
    private float speed = 5.0f;
    private float jumpForce = 5.5f;
    private bool facingRight = true;
    private bool grounded = false;
    private float killZone = 40.0f;
    private float deltaX;
    private float maxXSpeed = 10.0f;

    public GameObject walkO, walkF, standO, standF, jumpO, jumpF;
    public string color;
    private SpriteRenderer srOutline, srFill;
    private Sprite outline, fill;
    private Animator animator;

    //sprites to animate

    private int currentLevel;

    // Use this for initialization
    void Start () {
        startLocation = this.gameObject.GetComponent<Transform>().position;
        gamemanager = GameObject.Find("GameManager");
        currentLevel = gamemanager.GetComponent<Info>().level;
        color = "#FFFFFF";
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        RenderSprites();
	}


    //what should happen when the player collides with any object in the scene
    void OnCollisionEnter2D(Collision2D collision)
    {
        //player changes to the switch's color
        if (collision.gameObject.tag == "Switch")
        {
            //get color of object
            //string newColorMat = collision.gameObject.GetComponent<MeshRenderer>().material.ToString();
            this.gameObject.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
        }
        //if player is on a platform, kill them if they are not the right color (excluding neutral color)
        if (collision.gameObject.tag == "Platform")
        {
            if ((collision.gameObject.GetComponent<MeshRenderer>().material.color != this.gameObject.GetComponent<MeshRenderer>().material.color) && (collision.gameObject.GetComponent<MeshRenderer>().material.color != gamemanager.GetComponent<Info>().neutral.color))
            {
                Death();
            }
        }
        //hard-coding level loading based on what the current level is. Should probably fix this later :/
        if (collision.gameObject.tag=="Goal")
        {
            if (currentLevel == 1)
            {
                SceneManager.LoadScene("LevelTwo");
            }
            if (currentLevel == 2)
            {
                SceneManager.LoadScene("LevelThree");
            }
            if (currentLevel == 3)
            {
                SceneManager.LoadScene("Winner");
            }
        }
        grounded = true;
    }

    void Move()
    {
        pos = this.gameObject.GetComponent<Transform>().position;
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

        //flip the player sprite
        if(deltaX < 0.0f && !facingRight)
        {
            TurnAround();
        }
        else if(deltaX > 0.0f && facingRight)
        {
            TurnAround();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded)
            {
                velocity.y = jumpForce;
                grounded = false;
            }
        }

        //reset position if out of bounds
        if (OutOfBounds())
        {
            pos = startLocation;
            velocity = new Vector2(0, 0);
        }

        this.gameObject.GetComponent<Transform>().position = pos;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(deltaX * speed, velocity.y);
    }

    void RenderSprites()
    {
        //turn off all sprites, then activate the appropriate one
        walkO.SetActive(true);
        walkF.SetActive(true);
        standO.SetActive(true);
        standF.SetActive(true);
        jumpO.SetActive(true);
        jumpF.SetActive(true);
        //if in the air, render jump sprite
        if (!grounded)
        {
            standO.SetActive(false);
            standF.SetActive(false);
            walkO.SetActive(false);
            walkF.SetActive(false);
        }
        //if moving on the ground, render walk sprite
        else if (Mathf.Abs(deltaX) > 0)
        {
            jumpO.SetActive(false);
            jumpF.SetActive(false);
            standO.SetActive(false);
            standF.SetActive(false);
        }
        //if motionless, render idle sprite
        else
        {
            jumpO.SetActive(false);
            jumpF.SetActive(false);
            walkO.SetActive(false);
            walkF.SetActive(false);
        }
    }

    void TurnAround()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        this.transform.localScale = localScale;
    }

    //resets the player's location to their starting position in the level (does not reset level, just the player)
    void Death()
    {
        //sets the color to white (no color)
        this.gameObject.GetComponent<MeshRenderer>().material = gamemanager.GetComponent<Info>().white;
        this.gameObject.GetComponent<Transform>().position = startLocation;
        //resets the objects velocity so that the object's previous state of gravity and speed (before death) has no effect on them afterwards
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    //resets the player with Death() if they are out of the map/off-screen (if return = true)
    bool OutOfBounds()
    {
        bool oob = false;
        //if out of bounds, set oob to true
        //get position, compare it to killzone
        Vector2 pos = this.gameObject.GetComponent<Rigidbody2D>().position;
        if(Mathf.Abs(pos.x) > killZone || Mathf.Abs(pos.y) > killZone)
        {
            oob = true;
        }
        return oob;
    }

}
