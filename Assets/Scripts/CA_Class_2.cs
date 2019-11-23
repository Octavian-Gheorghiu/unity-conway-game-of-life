using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Class_2 : MonoBehaviour {

    // Variables
    private GameObject[,] grid;
    private int alive = 0;
    public GameObject caCubePrefab;
    public int columns = 20;
    public int rows = 20;
    public float spacing = 1.0f;
    public int overPopulation = 3;
    public int correctPopulation = 2;
    public int birthPopulation = 2;

	// Use this for initialization
	void Start () {
        CreateGrid();
	}

    // Update is called once per frame
    void Update()
    {
        // Assume all cells are dead at begining of frame
        alive = 0;

        // Go trought the grid and compute cells state
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Step 1: calculate how many neighbours are alive
                GameObject currentCube = grid[i, j];
                int currentCubeState = currentCube.GetComponent<CA_Cube_Class_2>().GetState();
                int aliveNeighbours = 0;
                int currentCubeFutureState = 0;

                // For cells that are not edges
                if(i>0 && i<columns-1 && j>0 && j<rows-1){
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are on the top edge
                if (i == 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are the left edge
                if (i > 0 && i < columns - 1 && j == 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are the right edge
                if (i > 0 && i < columns - 1 && j > 0 && j == rows)
                {
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are the bottom edge
                if (i > 0 && i == columns && j > 0 && j < rows - 1)
                {
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // Step 2: based on alive neigbours and cell state compute future state

                // Rules 1: if cell is alive
                if(currentCubeState == 1){
                    // Dies of lonliness
                    if(aliveNeighbours <correctPopulation){
                        grid[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(0);
                    }

                    // Lives if correct population
                    if(aliveNeighbours == correctPopulation){
                        grid[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(1);
                    }

                    // Dies of overpopulation
                    if(aliveNeighbours > overPopulation){
                        grid[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(0);
                    }
                }

                // Rules 2: if cell is dead
                if(currentCubeState == 0){
                    if(aliveNeighbours == birthPopulation){
                        grid[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(1);
                    }
                }

            }
        }
    }

    // Method to create a bunch of cubes on the grid
    private void CreateGrid(){
        // Step 1: allocate memorey to store the cubes
        grid = new GameObject[columns, rows];

        // Step 2: create a bunch of cubes placed in the grid with random states
        for (int i = 0; i < columns; i++){
            for (int j = 0; j < rows; j++){
                Vector3 cubePos = new Vector3(i * spacing, 0, j * spacing);
                Quaternion cubeRot = Quaternion.identity;
                GameObject newCube = Instantiate(caCubePrefab, cubePos, cubeRot);
                newCube.transform.parent = gameObject.transform;
                int newCubeState = Random.Range(0, 2);
                newCube.GetComponent<CA_Cube_Class_2>().SetState(newCubeState);
                grid[i, j] = newCube;
            }
        }
    }

    // Helper function telling the user how many cells are alive
    public int GetAlive(){
        return alive;
    }
}
