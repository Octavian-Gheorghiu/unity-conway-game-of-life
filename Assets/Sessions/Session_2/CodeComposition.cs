// LIBRARIES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeComposition : MonoBehaviour
{
    //VARIABLES (DATA) 

    // Scope - Type - Name - Value (optional)

    // NuUMBERS

    // Whole numbers
    public int myNumber = 1;
    public int mySecondNumber = 2;
    private int someNumberToBeStored;
    public int[] myNumbersArray = {1,2,3,4,5};
    public int[] myEmptyArray = new int[10];
    int sumAddition = 0;

    // Fractional 
    public float myFractionalNumber = 1.5f;
    double myLongFractionalNumber = 1.55555555555d;
    public List<float> myFloatList = new List<float>();

    // WORDS
    public string myWords = "Hello AADRL!";
    string oneWord = "Hello!";

    // LOGICAL
    bool someLogicalVariable = false;

    // VECTORS
    private Vector3[] danceMoves = {Vector3.up, Vector3.left, Vector3.down, Vector3.right, Vector3.right, Vector3.right, Vector3.down};

    // GAME OBJECTS
    public GameObject[] danceCube = new GameObject[3];
    
    // Start is called before the first frame update
    void Start()
    {
        myFloatList.Add(1.0f);
        myFloatList.Add(2.75f);

        int arrayElementsAddition = 0;

        // For loop (Start, End Condition, A Step)
        for(int i = 0; i < myNumbersArray.Length; i = i + 1)
        {
            arrayElementsAddition = arrayElementsAddition + myNumbersArray[i];
        }
        Debug.Log(arrayElementsAddition);
    }

    // Update is called once per frame
    void Update()
    {

        sumAddition = CalculatorAddition((int) myFloatList[1], sumAddition);
        Debug.Log("Calculated sum is :" + sumAddition.ToString());

        //CubeDance();

        CubeRotate(sumAddition);

        ChangeCubeColor();
    }

    // FUNCTIONS

    // Scope - Type - Name - Variables (Optional) - Return Data (Optinal)

    private void CubeDance () 
    {
        // READING DATA

        // For loop (counter, stop, increase) 
        Debug.Log("Cube Should Dance!");
        for (int i=0; i<danceMoves.Length; i++) 
        {
            Vector3 currentVec = danceMoves[i];
            for(int j=0; j<danceCube.Length; j++){
                danceCube[j].transform.Translate(currentVec);
            }            
        }
    }

    private void CubeRotate(int stopCondition)
    {
        int degrees = 0;

        // While loop (condtion for exit loop)
        while(degrees < stopCondition)
        {
            for(int j=0; j<danceCube.Length; j++){
                danceCube[j].transform.Rotate(0,degrees,0);
            }              
            degrees++;
        }
    }

    private int CalculatorAddition(int value1, int value2)
    {
        int sumResult = value1 + value2;
        return sumResult;
    }

    private void ChangeCubeColor()
    {
        for(int i = 0; i< danceCube.Length; i++)
        {
            Color newRandomCol = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
            danceCube[i].GetComponent<MeshRenderer>().material.SetColor("_Color", newRandomCol);
        }        
    }
}

/*
Exercise 1
1.Create an array of cylinders game objects
2.For each cylinder in the array scale it random in the x and y axis in a function 

Exercsie 2
1.Create a function that does the following
2.Create two random numbers between 7 and 187
3.Multiply the numbers
4.Calculate the square root of the multiplication result
5.Retrurn and print to console the function result
*/
