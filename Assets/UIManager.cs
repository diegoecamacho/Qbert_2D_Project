using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {


    [SerializeField] GameObject GameUI;
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject WinScreenUI;
    [SerializeField] GameObject LoseScreenUI;

    [SerializeField] TextMeshProUGUI WinScore;
    [SerializeField] TextMeshProUGUI LoseScore;
    [SerializeField] TMP_InputField InputField;

    public string WinScoreText
    {
        get
        {
            return WinScore.text;
        }

        set
        {
            WinScore.text = value;
        }
    }

    public string LoseScoreText
    {
        get
        {
            return LoseScore.text;
        }

        set
        {
            LoseScore.text = value;
        }
    }

    public string InputFieldText
    {
        get
        {
            return InputField.text;
        }
    }


    // Use this for initialization
    void Start () {
        PauseUI.SetActive(false);
        WinScreenUI.SetActive(false);
        LoseScreenUI.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
	}

    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        GameUI.SetActive(!GameUI.activeSelf);
        PauseUI.SetActive(!PauseUI.activeSelf);  
    }

    public void WinScreenEnable()
    {
        Time.timeScale = 0;
        GameUI.SetActive(false);
        WinScreenUI.SetActive(true);
    }

    public void LoseScreenEnable()
    {
        Time.timeScale = 0;
        GameUI.SetActive(false);
        LoseScreenUI.SetActive(true);
    }

    public void SetHighScore()
    {
        for (int i = 0; i < 10; i++)
        {
            if (GameManager.Score > PlayerPrefs.GetFloat(i.ToString()))
            {
                if (PlayerPrefs.GetFloat(i.ToString()) > 0)
                {
                    PlayerPrefs.SetString("n" + (i + 1).ToString(), PlayerPrefs.GetString(i.ToString()));
                    PlayerPrefs.SetFloat((i+1).ToString(), PlayerPrefs.GetFloat(i.ToString()));

                    PlayerPrefs.SetString("n" + i.ToString(), InputFieldText);
                    PlayerPrefs.SetFloat(i.ToString(), GameManager.Score);
                }
                else
                {
                    PlayerPrefs.SetString("n" + i.ToString(), InputFieldText);
                    PlayerPrefs.SetFloat(i.ToString(), GameManager.Score);
                }
                
                return;
            }
        }
    }

    public void LoadSceneHighScore(string sceneName)
    {
        SetHighScore();
        SceneManager.LoadScene(sceneName);
    }




}
