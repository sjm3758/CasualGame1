using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //Player properties:
    //walk speed
    public float walkSpeed = 5;
    //jump force
    public float jumpForce = 7;
    //gravity
    //starting position
    //color

    private SpriteRenderer spriteRenderer;
    private Animator animator;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime;
        var y = Input.GetAxis("Vertical") * Time.deltaTime;
	}
}
