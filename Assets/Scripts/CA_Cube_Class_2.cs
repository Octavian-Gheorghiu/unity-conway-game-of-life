using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Cube_Class_2 : MonoBehaviour {

    // Variables
    private int state = 0;
    private int futureState = 0;
    public bool computed = false;
	
	// Update is called once per frame
	void Update () {
        if(computed == false){
            state = futureState;
            DisplayCube();
        } else {
            DisplayCube();
        }
	}

    // Behaviours (Methods)

    // Sets the cube as being computed 
    public void SetComputed(bool _computed){
        computed = _computed;
    }

    // Display behaviour
    void DisplayCube(){
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        MeshRenderer renderer;
        // If the cell is dead do not show it
        if(state == 0){
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        } else {
            // Else show the cell
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            props.SetColor("_Color", Color.black);

            renderer = gameObject.GetComponent<MeshRenderer>();
            renderer.SetPropertyBlock(props);
        }
    }

    // Method to set the futureState variable
    public void SetFutureState(int _futureState){
        futureState = _futureState;
    }

    // Methods to set the current state variable
    public void SetState(int _state){
        state = _state;
    }

    // Help method to retreive what the current state is
    public int GetState(){
        return state;
    }

    // Help method to retrive what will be the future state
    public int GetFutureState(){
        return futureState;
    }

    // Method to rotate the cube
    public void RotateCube(){
        gameObject.transform.Rotate(new Vector3(0, 45, 0));
    }

}
