using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_History_Class : MonoBehaviour {

    // Variables
    public int noStatesToRecord = 5;
    private int recordedStates = 0;
    private GameObject caGridStatesHistory;
    private GameObject[,] grid;
    private int[,] ageOfCells;
    private int alive = 0;
    public GameObject cubePrefab;
    public int columns = 20;
    public int rows = 20;
    public float spacing = 1.0f;
    public Texture2D seedImage;

	// Use this for initialization
	void Start () {
        // Initialize the history holder
        caGridStatesHistory = new GameObject("caHistory");

        // Setup the grid
        CreateGrid();	
	}
	
	// Update is called once per frame
	void Update () {
		// We need to compute a new state
        if(recordedStates<=noStatesToRecord){
            // Update the grid (compute a new state
            UpdateGrid();

            // Add the updated grid to the history
            AddUpdatedGridToHistory();

            // Increase the recorded states count
            recordedStates++;
        } else {
            Destroy(GameObject.Find("caGrid"));
            // Dsiplay the fancy new 3d CA
        }


	}

    // Methods
    // Create the caGrid
    private void CreateGrid()
    {
        // Random grid
        /*
        grid = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Create a new caCube
                Vector3 position = new Vector3(i * spacing, 0, j * spacing); // Unity Y axis is the Z axis
                Quaternion rotation = Quaternion.identity;
                GameObject newObj = Instantiate(cubePrefab, position, rotation);
                newObj.transform.parent = gameObject.transform; // Attach the caCube to the caGrid
                newObj.GetComponent<CA_Cube_Class>().SetState(Random.Range(0, 2)); // Assign a random type to the caCube
                // Store the caCube in the data structure
                grid[i, j] = newObj;
            }
        }
        */

        // Grid from image
        grid = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Create a new caCube
                Vector3 position = new Vector3(i * spacing, 0, j * spacing); // Unity Y axis is the Z axis
                Quaternion rotation = Quaternion.identity;
                GameObject newObj = Instantiate(cubePrefab, position, rotation);
                newObj.transform.parent = gameObject.transform; // Attach the caCube to the caGrid
                int state = (int) seedImage.GetPixel(i, j).grayscale;
                newObj.GetComponent<CA_Cube_Class>().SetState(state);
                // Store the caCube in the data structure
                grid[i, j] = newObj;
            }
        }

        ageOfCells = new int[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                ageOfCells[i, j] = 0;
            }
        }
    }

    // Update the grid
    private void UpdateGrid()
    {
        alive = 0;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject currentCube = grid[i, j];
                int currentCubeState = currentCube.GetComponent<CA_Cube_Class>().GetState();
                int neigboursCount = 0;
                int futureState = 0;

                // Calculate how many neigbours are alive around the current cell
                if (i > 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube_Class>().GetState() == 1) neigboursCount++;

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
                if (currentCubeState == 1)
                {
                    // I die if i have less than 2 neigbours
                    if (neigboursCount < 2)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(0);
                        futureState = 0;
                    }
                    // I live if I have exactly 2 neigbours
                    if (neigboursCount == 2)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(1);
                        futureState = 1;
                        ageOfCells[i, j]++;
                        grid[i, j].GetComponent<CA_Cube_Class>().SetAge(ageOfCells[i, j]);
                    }
                    // I die if I have more than 3 neigbours
                    if (neigboursCount > 3)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(0);
                        futureState = 0;
                    }
                }

                // Rule 2: for cells that are dead
                if (currentCubeState == 0)
                {
                    if (neigboursCount == 3)
                    {
                        grid[i, j].GetComponent<CA_Cube_Class>().SetFutureState(1);
                        futureState = 1;
                        ageOfCells[i, j]++;
                        grid[i, j].GetComponent<CA_Cube_Class>().SetAge(ageOfCells[i, j]);
                    }
                }

                // Update how many alive cells are in the game
                alive += futureState;
            }
        }
    }

    // Add the computed state to the CA History game object
    private void AddUpdatedGridToHistory(){
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Create a new caCube at the recorded postion if the cube is alive
                GameObject gridCube = grid[i, j];
                if (gridCube.GetComponent<CA_Cube_Class>().GetState() == 1){
                    // Create a new cube at the recoded postion and rotation
                    Vector3 pos = new Vector3(gridCube.transform.position.x, gridCube.transform.position.y,gridCube.transform.position.z);
                    Quaternion rot = Quaternion.identity;
                    GameObject newCube = Instantiate(cubePrefab, pos, rot);
                    // Optimise the cube by removing redundent components
                    Destroy(newCube.GetComponent<Rigidbody>());
                    Destroy(newCube.GetComponent<Collider>());
                    // Destroy(newCube.GetComponent<CA_Cube_Class>());
                    newCube.GetComponent<CA_Cube_Class>().updateState = false;
                    // Optimse the cube for GPU rendering
                    MaterialPropertyBlock props = new MaterialPropertyBlock();
                    props.SetColor("_Color", Color.black);
                    newCube.GetComponent<MeshRenderer>().SetPropertyBlock(props);
                    // Move the cube up 
                    newCube.transform.Translate(Vector3.up * recordedStates);
                    // Parent the copied cube to the CA History game object
                    newCube.transform.parent = caGridStatesHistory.transform;
                    // Copy age information
                    newCube.GetComponent<CA_Cube_Class>().SetAge(ageOfCells[i,j]);
                }
            }
        }
    }

    // Get how many cells are alive
    public int GetAlive(){
        return alive;
    }

}
