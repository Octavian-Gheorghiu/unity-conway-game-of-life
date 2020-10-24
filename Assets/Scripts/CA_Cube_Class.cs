using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Cube_Class : MonoBehaviour {

    // Variables
    private int state = 0;
    private int futureState = 0;
    public int age = 0;
    public bool updateState = true;


    // Getter and setter c#
	
	// Update is called once per frame
	void Update () {
        if (updateState)
        {
            state = futureState;
            DisplayCube();
        }
	}

    // Behaviours (Methods)

    // Method to handel how the cube is displayed
    void DisplayCube(){
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;
        if(state == 0){
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            // Do somethng here with the renderer
        }
        if(state == 1){
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            props.SetColor("_Color", Color.black);

            renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }

    public void DisplayAge(Color _cubeColor)   
    {
        // Set the material property block to a colur assigned by the evaluator
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        props.SetColor("_Color", _cubeColor);

        // Update the colour of the mesh
        MeshRenderer renderer;
        renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.SetPropertyBlock(props);
    }

    // Methods to set the future state
    public void SetFutureState(int _futureState){
        futureState = _futureState;
    }

    // Methods to set the current state
    public void SetState(int _state){
        futureState = _state;
    }

    // Helper method to get the current state
    public int GetState(){
        return state;
    }

    // Helpre methods to get the future state
    public int GetFutureState(){
        return futureState;
    }

    public int GetAge()
    {
        return age;
    }

    public void SetAge(int _age)
    {
        age = _age;
    }
}
