using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI[] HighScoreText;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < HighScoreText.Length; i++)
        {
            HighScoreText[i].text = PlayerPrefs.GetString("n" + i.ToString()).ToString() + "            " + PlayerPrefs.GetFloat(i.ToString()).ToString();

        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
