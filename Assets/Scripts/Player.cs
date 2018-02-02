using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
        speed = 5.0f;
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
            Debug.Log("switch hit!");
        }
    }

    void Move()
    {
        Vector3 pos = this.gameObject.GetComponent<Transform>().position;
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

        this.gameObject.GetComponent<Transform>().position = pos;
    }

}
