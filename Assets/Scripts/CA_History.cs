using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_History : MonoBehaviour {

    // Variables
    public int noStatesToRecord = 5;
    private int recordedGridStates = 0;
    private GameObject caGridStatesHistory;
    private GameObject[,] grid;
    private int alive = 0;
    public GameObject obj;
    public int columns = 20;
    public int rows = 20;
    public float spacing = 1.0f;

    // Use this for initialization
    void Start () {
        // Initialise the history list
        caGridStatesHistory = new GameObject("caHistory");

        // Setup a new instance of the grid
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (recordedGridStates <= noStatesToRecord)
        {
            // Update the grid
            UpdateGrid();

            // Add the updated state to the history
            AddUpatedGridToHistory();

            // Increase history count
            recordedGridStates++;
        }
        else
        {
            Destroy(GameObject.Find("caGrid"));
            GameObject.Find("caHistory").AddComponent<ModelDisplay>();
            GameObject.Find("groundPlane").GetComponent<Transform>().transform.localScale = new Vector3(50,50,50);
            GameObject.Find("groundPlane").GetComponent<Transform>().transform.Translate(Vector3.up);
        }
    }

    // Create the caGrid
    private void CreateGrid()
    {
        grid = new GameObject[columns, rows];
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Create a new caCube
                Vector3 position = new Vector3(i * spacing, 0, j * spacing); // Unity Y axis is the Z axis
                Quaternion rotation = Quaternion.identity;
                GameObject newObj = Instantiate(obj, position, rotation);
                newObj.transform.parent = gameObject.transform; // Attach the caCube to the caGrid
                newObj.GetComponent<CA_Cube>().SetState(Random.Range(0, 2)); // Assign a random type to the caCube
                // Store the caCube in the data structure
                grid[i, j] = newObj;
            }
        }
    }

    private void UpdateGrid()
    {
        alive = 0;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject currentCube = grid[i, j];
                int currentCubeState = currentCube.GetComponent<CA_Cube>().GetState();
                int neighborsCount = 0;
                int futureState = 0;

                // Calculate alive neighbors
                if (i > 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                }
                if (i == 0 && i < columns - 1 && j > 0 && j < rows - 1)
                {
                    if (grid[i + 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                }
                if (i > 0 && i < columns - 1 && j == 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                }
                if (i > 0 && i == columns && j > 0 && j < rows - 1)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i - 1, j + 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                }
                if (i > 0 && i < columns - 1 && j > 0 && j == rows)
                {
                    if (grid[i - 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i - 1, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                    if (grid[i + 1, j - 1].GetComponent<CA_Cube>().GetState() == 1) neighborsCount++;
                }

                // Rule 1: for cells that are alive
                if (currentCubeState == 1)
                {
                    if (neighborsCount < 2)
                    {
                        grid[i, j].GetComponent<CA_Cube>().SetFutureState(0);
                        futureState = 0;
                    }
                    if (neighborsCount == 2 || neighborsCount == 3)
                    {
                        grid[i, j].GetComponent<CA_Cube>().SetFutureState(1);
                        futureState = 1;
                    }
                    if (neighborsCount > 3)
                    {
                        grid[i, j].GetComponent<CA_Cube>().SetFutureState(0);
                        futureState = 0;
                    }
                }

                // Rule 2: for cells that are dead
                if (currentCubeState == 0)
                {
                    if (neighborsCount == 3)
                    {
                        grid[i, j].GetComponent<CA_Cube>().SetFutureState(1);
                        futureState = 1;
                    }
                }

                // Update how many are alive
                alive += futureState;
            }
        }
    }

    private void AddUpatedGridToHistory()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                // Create a new caCube at the recorded position if cube is alive
                GameObject gridCube = grid[i, j];
                if (gridCube.GetComponent<CA_Cube>().GetState() == 1)
                {
                    Vector3 position = new Vector3(gridCube.transform.position.x, gridCube.transform.position.y, gridCube.transform.position.z);
                    Quaternion rotation = Quaternion.identity;
                    GameObject newObj = Instantiate(obj, position, rotation);
                    Destroy(newObj.GetComponent<Rigidbody>());
                    Destroy(newObj.GetComponent<Collider>());
                    Destroy(newObj.GetComponent<CA_Cube>());
                    MaterialPropertyBlock props = new MaterialPropertyBlock();
                    props.SetColor("_Color", Color.black);
                    newObj.GetComponent<MeshRenderer>().SetPropertyBlock(props);
                    // Move the new caCube up by the current record count
                    newObj.transform.Translate(Vector3.up * recordedGridStates);
                    newObj.transform.parent = caGridStatesHistory.transform;                    
                }                        
            }
        }
    }

    // Get how many cells are alive
    public int GetAlive()
    {
        return alive;
    }
}
