using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_3D_Class_2 : MonoBehaviour {

    // Variables
    public Texture2D seedImage;
    private GameObject[,] layer;
    private GameObject[,,] stack;
    private int alive = 0;
    public GameObject caCubePrefab;
    public int columns = 40;
    public int rows = 40;
    public int height = 80;
    private int currentHeight = 0;
    public float spacing = 1.0f;
    public int overPopulation = 3;
    public int correctPopulation = 2;
    public int birthPopulation = 2;

	// Use this for initialization
	void Start () {
        stack = new GameObject[columns, rows, height];
        CreateLayer();
	}

    // Update is called once per frame
    void Update()
    {
        if(currentHeight < height){
            // Update current layer computation
            UpdateLayer();
            // Save the computed layer to the stack
            SaveLayer();
            // Increase layer count
            currentHeight++;
        }

        // Rule: Decrease density as stack grows
        /*
        if(currentHeight%10==0){
            overPopulation--;
        }
        */
    }

    // Function to save a computed layer
    private void SaveLayer(){
        string LayerName = "layer" + currentHeight.ToString();
        GameObject currentLayer = new GameObject(LayerName);

        for (int i = 0; i < columns; i++){
            for (int j = 0; j < rows; j++){
                GameObject computedCube = layer[i, j];

                Vector3 computedCubePos = new Vector3(i, currentHeight, j);
                Quaternion computedCubeRot = Quaternion.identity;
                GameObject copiedCube = Instantiate(caCubePrefab, computedCubePos, computedCubeRot);
                int computedCubeState = computedCube.GetComponent<CA_Cube_Class_2>().GetState();
                copiedCube.GetComponent<CA_Cube_Class_2>().SetState(computedCubeState);
                copiedCube.GetComponent<CA_Cube_Class_2>().SetComputed(true);

                copiedCube.transform.parent = currentLayer.transform;

                stack[i, j, currentHeight] = copiedCube;
            }
        }
    }

    // Function to compute a new layer
    private void UpdateLayer(){
        // Assume all cells are dead at begining of frame
        alive = 0;

        // Go trought the layer and compute cells state
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Step 1: calculate how many neighbours are alive
                GameObject currentCube = layer[i, j];
                int currentCubeState = currentCube.GetComponent<CA_Cube_Class_2>().GetState();
                int aliveNeighbours = 0;
                int currentCubeFutureState = 0;

                // For cells that are not edges
                if (i > 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (layer[i - 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i - 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are on the top edge
                if (i == 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (layer[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are the left edge
                if (i > 0 && i < columns - 1 && j == 0 && j < rows - 1)
                {
                    if (layer[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i - 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are the right edge
                if (i > 0 && i < columns - 1 && j > 0 && j == rows)
                {
                    if (layer[i - 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i + 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // For cells that are the bottom edge
                if (i > 0 && i == columns && j > 0 && j < rows - 1)
                {
                    if (layer[i - 1, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i - 1, j].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i - 1, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j - 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                    if (layer[i, j + 1].GetComponent<CA_Cube_Class_2>().GetState() == 1) aliveNeighbours++;
                }

                // Step 2: based on alive neigbours and cell state compute future state

                // Rules 1: if cell is alive
                if (currentCubeState == 1)
                {
                    // Dies of lonliness
                    if (aliveNeighbours < correctPopulation)
                    {
                        layer[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(0);
                    }

                    // Lives if correct population
                    if (aliveNeighbours == correctPopulation)
                    {
                        layer[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(1);
                    }

                    // Dies of overpopulation
                    if (aliveNeighbours > overPopulation)
                    {
                        layer[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(0);
                    }
                }

                // Rules 2: if cell is dead
                if (currentCubeState == 0)
                {
                    if (aliveNeighbours == birthPopulation)
                    {
                        layer[i, j].GetComponent<CA_Cube_Class_2>().SetFutureState(1);
                    }
                }

            }
        }
    }

    // Method to create a bunch of cubes on the layer
    private void CreateLayer(){
        // Step 1: allocate memorey to store the cubes
        layer = new GameObject[columns, rows];

        // Step 2: create a bunch of cubes placed in the layer with random states
        for (int i = 0; i < columns; i++){
            for (int j = 0; j < rows; j++){
                Vector3 cubePos = new Vector3(i * spacing, 0, j * spacing);
                Quaternion cubeRot = Quaternion.identity;
                GameObject newCube = Instantiate(caCubePrefab, cubePos, cubeRot);
                newCube.transform.parent = gameObject.transform;
                //int newCubeState = Random.Range(0, 2);
                int newCubeState = (int)seedImage.GetPixel(i, j).grayscale;
                newCube.GetComponent<CA_Cube_Class_2>().SetState(newCubeState);
                layer[i, j] = newCube;
            }
        }
    }

    // Helper function telling the user how many cells are alive
    public int GetAlive(){
        return alive;
    }
}
