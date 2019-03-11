using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evaluator : MonoBehaviour {

    // Variables and refrences
    private GameObject caGrid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            caGrid = GameObject.Find("caHistory");
            DisplayAge();
        }
	}

    // Methods

    // Display age of cells
    public void DisplayAge()
    {
        // Compute the minimu age and maximum age
        float minVal = float.MaxValue;
        float maxVal = float.MinValue;

        Debug.Log(caGrid.name);

        foreach(Transform child in caGrid.transform)
        {
            int ageOfCube = child.gameObject.GetComponent<CA_Cube_Class>().GetAge();
            //Debug.Log("Age="+ageOfCube.ToString());
            if (ageOfCube < minVal) minVal = ageOfCube;
            if (ageOfCube > maxVal) maxVal = ageOfCube;
        }

        //Debug.Log("Min="+minVal.ToString());
        //Debug.Log("Max=" + maxVal.ToString());

        foreach(Transform child in caGrid.transform)
        {
            float greyVal = Remap((float) child.gameObject.GetComponent<CA_Cube_Class>().GetAge(), minVal, maxVal, 0.0f, 1.0f);
            Color greyColour = new Color(greyVal, greyVal, greyVal);
            child.GetComponent<CA_Cube_Class>().DisplayAge(greyColour);
        }

    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
