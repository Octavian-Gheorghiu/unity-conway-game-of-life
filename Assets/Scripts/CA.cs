using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA : MonoBehaviour {

    // Variables
    private GameObject[,] grid;
    private int alive = 0;
    public GameObject obj;
    public int columns = 20;
    public int rows = 20;
    public float spacing = 1.0f;

    // Use this for initialization
    void Start () {
        // Setup a new instance of the grid
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
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
                if (i>0 && i < columns - 1 && j > 0 && j < rows - 1)
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

    // Get how nay cells are alive
    public int GetAlive()
    {
        return alive;
    }
}
