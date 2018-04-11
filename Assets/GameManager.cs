using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

     public static int Score = 0;
    List<NodeScript> gameNodes;
    bool startSearch = false;

	// Use this for initialization
	void Start ()
    {
        Search();
    }

    private void Search()
    {
        gameNodes = new List<NodeScript>();
        foreach (GameObject node in GameObject.FindGameObjectsWithTag("Nodes"))
        {
            gameNodes.Add(node.GetComponent<NodeScript>());
        }
        startSearch = true;
    }

    void Update()
    {
        if (startSearch)
        {
            foreach (var node in gameNodes)
            {
                if (node.Selected == true)
                {
                    node.SelectCube();
                    CheckGameOver();
                    node.Selected = false;
                }
            }
        }
    }

    void CheckGameOver()
    {
        bool gameOver = true;
        foreach (var node in gameNodes)
        {
            if(!node.complete)
            {
                gameOver = false;
            }

        }
        if (gameOver)
        {
            Debug.Log("Win Screen");
            //Todo WIN Screen.
        }
    }
}
