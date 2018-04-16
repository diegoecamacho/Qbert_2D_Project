using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool enemyUpdate = true;
    public static int Score = 0;
    private int playerLives = 3;

    private List<NodeScript> gameNodes;
    private bool startSearch = false;

    public int PlayerLives
    {
        get
        {
            return playerLives;
        }

        set
        {
            playerLives = value;
        }
    }

    // Use this for initialization
    private void Start()
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

    private void Update()
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

    private void CheckGameOver()
    {
        bool gameOver = true;
        foreach (var node in gameNodes)
        {
            if (!node.complete)
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