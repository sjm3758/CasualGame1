using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWheelRotate : MonoBehaviour {

    private Animator animator;
    private Component[] spriteColor;

	// Use this for initialization
	void Start () {
        spriteColor = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Change Color");

            //test
            
        }
        else
        {

        }

	}

    public void WheelColorChange()
    {
        foreach (SpriteRenderer color in spriteColor)
        {
            if (color.color == Color.red)
            {
                color.color = Color.blue;
            }
            else if (color.color == Color.blue)
            {
                color.color = Color.green;
            }
            else if (color.color == Color.green)
            {
                color.color = Color.red;
            }
        }
    }
}
