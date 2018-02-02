using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    //initializes the colors and temp variable for the given gameObjects renderer
    public MeshRenderer gameobjectRenderer;
    public Color red;
    public Color green;
    public Color blue;
    //public Material redmat;
    //public Material greenmat;
    //public Material bluemat;
    private Color[] colors;
    private Material[] mats;
    private int counter;

	// Use this for initialization
	void Start () {
        counter = 0;
        gameobjectRenderer = this.gameObject.GetComponent<MeshRenderer>();
        gameobjectRenderer.material = this.gameObject.GetComponent<Material>();
        //RGB colors set
        red = new Color(255f, 0, 0, 1f);
        blue = new Color(0, 0, 255f, 1f);
        green = new Color(0, 255f, 0, 1f);
        colors = new Color[] { red, green, blue };
        //mats = new Material[] { redmat, greenmat, bluemat };
        
    }
	
	// Update is called once per frame
	void Update () {
        CycleColor();
        this.gameObject.GetComponent<MeshRenderer>().material = gameobjectRenderer.material;
    }

    public void CycleColor()
    {
        #region Change color with RGB Keys
        /*if (Input.GetKeyDown(KeyCode.R))
        {
            gameobjectRenderer.material.color = red;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            gameobjectRenderer.material.color = blue;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameobjectRenderer.material.color = green;
        }*/
        #endregion

        //Change color by spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameobjectRenderer.material.color = colors[counter];
            counter++;
        }

        //Change color by switch in level
        
        //Change color by player jumping



        if (counter >= colors.Length)
        {
            counter = 0;
        }
    }
}
