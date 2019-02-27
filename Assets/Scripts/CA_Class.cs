using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Class : MonoBehaviour {

    // Variables
    private GameObject[,] grid;
    private int alive = 0;
    public GameObject caCubePrefab;
    public int columns = 20;
    public int rows = 20;
    public float spacing = 1.0f;

	// Use this for initialization
	void Start () {
        // Set a new instance of the grid 
        CreateGrid();
	}
	
	// Update is called once per frame
	void Update () {
        alive = 0;
        for (int i = 0; i < columns; i++){
            for (int j = 0; j < rows; j++){
                GameObject currentCube = grid[i, j];
                int currentCubeState = currentCube.GetComponent<CA_Cube_Class>().GetState();
                int neigboursCount = 0;
                int futureState = 0;

                // Calculate how many neigbours are alive around the current cell
                if(i>0 && i <columns-1 && j>0 && j <rows-1){
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i-1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i+1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i+1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i-1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;

                    /*
                    for (int k = -1; k <= 1; k++){
                        if (grid[i + k, j + k].GetComponent<CA_Cube_Class_Class>().GetState() == 1) neigbourCount++;
                    }
                    */
                }
                if (i == 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                }
                if (i > 0 && i < columns - 1 && j == 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                }
                if (i > 0 && i == columns && j > 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                }
                if (i > 0 && i < columns - 1 && j > 0 && j == rows)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                }

                // Rule 1: for cells that are alive
                if(currentCubeState == 1){
                    // I die if i have less than 2 neigbours
                    if(neigboursCount < 2){
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(0);
                        futureState = 0;
                    }
                    // I live if I have exactly 2 neigbours
                    if (neigboursCount == 2)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(1);
                        futureState = 1;
                    }
                    // I die if I have more than 3 neigbours
                    if (neigboursCount > 3)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(0);
                        futureState = 0;
                    }
                }

                // Rule 2: for cells that are dead
                if(currentCubeState == 0){
                    if(neigboursCount == 3)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(1);
                        futureState = 1;
                    }
                }

                // Update how many alive cells are in the game
                alive += futureState;
            }
        }
	}

    // Methods

    // Create the grid of cells
    private void CreateGrid(){
        grid = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++){
            for (int j = 0; j < rows; j++){
                Vector3 position = new Vector3(i * spacing, 0, j * spacing);
                Quaternion rotation = Quaternion.identity;
                GameObject newCube = Instantiate(caCubePrefab, position, rotation);
                newCube.transform.parent = gameObject.transform;
                newCube.GetComponent<CA_Cube_Class>().SetState(Random.Range(0, 2));
                grid[i, j] = newCube;
            }
        }
    }

    // Get how many cells are alive in the grid at the computed frame
    public int GetAlive(){
        return alive;
    }
}
