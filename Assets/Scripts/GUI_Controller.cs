using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Controller : MonoBehaviour {

    public GUISkin mySkin;
    public int score = 0;

    private void Update()
    {
        // Handeling keyboard events
        if(Input.GetKeyDown(KeyCode.Return)){
            Debug.Log("Enter was pressed...maybe do someting!");
            // Code to do someting
        }
        // Load 2d CA
        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("CA_2d");
            DynamicGI.UpdateEnvironment();
        }

        // Load 2d CA with history record
        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("CA_2d_history");
            DynamicGI.UpdateEnvironment();
        }

        // Load 3d CA
        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("CA_3d");
            DynamicGI.UpdateEnvironment();
        }

        // Reset current scene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            DynamicGI.UpdateEnvironment();
        }

        // Quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    // Function that handles screen dsiplays
    private void OnGUI()
    {
        // Set a label for our game
        GUI.skin = mySkin;
        GUI.Label(new Rect(new Vector2(50,100), new Vector2(300,100)), "The Game of Life!");

        // Set the population count 
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "CA_2d")
        {
            score = GameObject.Find("caGrid").GetComponent<CA_Class>().GetAlive();
        }
        if (scene.name == "CA_2d_Class")
        {
            score = GameObject.Find("caGrid").GetComponent<CA>().GetAlive();
        }
        if (scene.name == "CA_2d_history")
        {
            score = GameObject.FindGameObjectsWithTag("caNode").Length;
        }        
        GUI.Label(new Rect(new Vector2(Screen.width-175, 100), new Vector2(300, 100)), "Population: " + score.ToString());
        if (scene.name == "CA_3d")
        {
            score = GameObject.Find("caGrid").GetComponent<CA_3D>().GetAlive();
        }
        GUI.Label(new Rect(new Vector2(Screen.width - 175, 100), new Vector2(300, 100)), "Population: " + score.ToString());
    }
}
