using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMusicPlayer : MonoBehaviour {
    public static MainMusicPlayer instance;

	// Use this for initialization
	void Awake () {
        if (!instance)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);

            }
        }
	}

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1Scene"))
        {
            Destroy(gameObject);

        }
    }


}
