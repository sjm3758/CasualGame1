using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //player's speed
    public float speed;
    //keeps tarck of the player's starting location when they spawn into a level; helps with resetting location
    private Vector3 startLocation;
    //temp variable for adjusting the player's location in Move()
    private Vector3 pos;
    //the gamemanager in the scene (used to get materials)
    public GameObject gamemanager;

    // Use this for initialization
    void Start () {
        speed = 5.0f;
        startLocation = this.gameObject.GetComponent<Transform>().position;
        gamemanager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        this.gameObject.GetComponent<Transform>().position = pos;
    }

    //what should happen when the player collides with any object in the scene
    void OnCollisionEnter2D(Collision2D collision)
    {
        //player changes to the switch's color
        if (collision.gameObject.tag == "Switch")
        {
            this.gameObject.GetComponent<MeshRenderer>().material = collision.gameObject.GetComponent<MeshRenderer>().material;
        }
        //if player is on a platform, kill them if they are not the right color (excluding neutral color)
        if (collision.gameObject.tag == "Platform")
        {
            if ((collision.gameObject.GetComponent<MeshRenderer>().material.color != this.gameObject.GetComponent<MeshRenderer>().material.color) && (collision.gameObject.GetComponent<MeshRenderer>().material.color != gamemanager.GetComponent<Materials>().neutral.color))
            {
                Death();
            }
        }
    }

    //moves the player left and right across the scene
    void Move()
    {
        pos = this.gameObject.GetComponent<Transform>().position;
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed * Time.deltaTime;
        }
        //move left
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed * Time.deltaTime;
        }
    }

    //resets the player's location to their starting position in the level (does not reset level, just the player)
    void Death()
    {
        //sets the color to white (no color)
        this.gameObject.GetComponent<MeshRenderer>().material = gamemanager.GetComponent<Materials>().white;
        this.gameObject.GetComponent<Transform>().position = startLocation;
        //resets the objects velocity so that the object's previous state of gravity and speed (before death) has no effect on them afterwards
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    //resets the player with Death() if they are out of the map/off-screen (if return = true)
    bool OutOfBounds()
    {
        bool oob = false;
        return oob;
    }
}
