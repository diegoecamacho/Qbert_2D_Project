using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool enemyUpdate = false;
    public static bool playerUpdate = false;
    public static int Score = 0;
    public static int playerLives = 2;

    private List<NodeScript> gameNodes;
    private bool startSearch = false;


    [SerializeField] UIManager UI;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] float DeathAnimTime;

    
    int currentLives;

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
        enemyUpdate = false;
        playerUpdate = false;
        StartCoroutine(StartAnim());
        playerLives = 2;
       
        Score = 0;
        Time.timeScale = 1;
        
        currentLives = playerLives;
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
        
        if (currentLives != playerLives)
        {
            GameObject.FindGameObjectWithTag("Qbert").GetComponent<QbertScript>().EnableDeathAnim(DeathAnimTime);
            WipeBoard();
            StopGameClock();
            StartCoroutine(DeathAnim());
            Invoke("StartGameClock", (DeathAnimTime * 7));
            currentLives = playerLives;
        }
        scoreText.text = Score.ToString();
        if (playerLives == -1)
        {
            UI.LoseScreenEnable();
            UI.LoseScoreText = Score.ToString();
        }
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
            Score += GameObject.FindGameObjectsWithTag("ElevatorNode").Length * 100;
            Score += 1000;
            Debug.Log("Win Screen");
            UI.WinScreenEnable();
            UI.WinScoreText = Score.ToString();
            gameOver = false;
        }
    }

   

    void WipeBoard()
    {
        foreach (var go in GameObject.FindGameObjectsWithTag("Enemies"))
        {
            Destroy(go);
        }
    }

    void StartGameClock()
    {
        Debug.Log("Movepesant");
        enemyUpdate = true;
        playerUpdate = true;
    }

    void StopGameClock()
    {
        enemyUpdate = false;
        playerUpdate = false;
    }

    IEnumerator DeathAnim()
    {
        
        int count = 0;
        int nodeNum = 7;
        var Nodes = GameObject.FindGameObjectsWithTag("Nodes");
        int currentNode = Nodes.Length -1;
        while (nodeNum > 0)
        {
            if (count == 0)
            {
                for (int i = 0; i < nodeNum; i++)
                {
                    Nodes[currentNode - i].GetComponent<SpriteRenderer>().enabled = false;
                }
                count++;
                yield return new WaitForSeconds(DeathAnimTime);

            }
            if (count == 1)
            {
                for (int i = 0; i < nodeNum; i++)
                {
                    Nodes[currentNode - i].GetComponent<SpriteRenderer>().enabled = true;
                }
                currentNode -= nodeNum;
                nodeNum--;
                count = 0;
                
            }
        
        }
        
    }
    IEnumerator StartAnim()
    {
        
        int count = 0;
        int nodeNum = 7;
        var Nodes = GameObject.FindGameObjectsWithTag("Nodes");
        int currentNode = Nodes.Length -1;
        while (nodeNum > 0)
        {
            if (count == 0)
            {
                for (int i = 0; i < nodeNum; i++)
                {
                    Nodes[currentNode - i].GetComponent<SpriteRenderer>().enabled = false;
                }
                count++;
                yield return new WaitForSeconds(DeathAnimTime);

            }
            if (count == 1)
            {
                for (int i = 0; i < nodeNum; i++)
                {
                    Nodes[currentNode - i].GetComponent<SpriteRenderer>().enabled = true;
                }
                currentNode -= nodeNum;
                nodeNum--;
                count = 0;
                
            }
        
        }
        enemyUpdate = true;
        playerUpdate = true;

    }

   
}