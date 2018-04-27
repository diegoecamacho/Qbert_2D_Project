using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveController : MonoBehaviour {

   [SerializeField] GameObject[] Sprites;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (GameManager.playerLives)
        {
            case 0:
                Sprites[GameManager.playerLives].SetActive(false);
                break;
            case 1:
                Sprites[GameManager.playerLives].SetActive(false);
                break;
            case 2:
                Sprites[GameManager.playerLives].SetActive(false);
                break;
        }

    }
}
