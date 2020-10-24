// LIBRARIES
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
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

    // Fractional 
    public float myFractionalNumber = 1.5f;
    double myLongFractionalNumber = 1.55555555555d;
    public List<float> myFloatList = new List<float>();

    // WORDS
    public string myWords = "Hello AADRL!";
    string oneWord = "Hello!";

    // LOGICAL
    bool someLogicalVariable = false;
    
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
        
        int sumAddition = (int) myFloatList[1] + mySecondNumber;
        Debug.Log(sumAddition);
    }
}

// Homework
/*
1. Declare and initalize an integer variable (25)
2. Declare and intialize a float array collecyion (1.2, 2.5, 7.8, 9.4, 22.36)
3. Add up the integer variable to all the float numbers in the array collection
4. Add all the float variables in the array together
5. Dsiplay the result in the Unity console 
*/
