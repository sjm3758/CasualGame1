using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWheelRotate : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
		
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
        }
        else
        {

        }

	}
}
