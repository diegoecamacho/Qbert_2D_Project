using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QbertScript : MonoBehaviour {
    //Current Cube
    NodeScript currentCube;

    public NodeScript CurrentCube
    {
        get
        {
            return currentCube;
        }

        set
        {
            currentCube = value;
        }
    }

    //Animator
    Animator qbertAnim;

	

    // Use this for initialization
	void Start () {
        currentCube = transform.parent.GetComponent<NodeScript>();
        qbertAnim = GetComponent<Animator>();
        if (CurrentCube != null)
        {
            Debug.Log(CurrentCube.name);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        InputManager();

    }

    void InputManager()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentCube.Adjacent[0] != null)
            {
                CurrentCube = CurrentCube.Adjacent[0];
                transform.position = Vector3.MoveTowards(transform.position, CurrentCube.transform.position, 0.0002f);
                qbertAnim.SetBool("Jump", true);
                qbertAnim.SetInteger("Direction", 0);
            }
            Debug.Log("Q");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (CurrentCube.Adjacent[1] != null)
            {
                CurrentCube = CurrentCube.Adjacent[1];
                transform.position = Vector3.MoveTowards(transform.position, CurrentCube.transform.position, 0.0002f);
                qbertAnim.SetBool("Jump", true);
                qbertAnim.SetInteger("Direction", 1);
            }
            Debug.Log("W");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (CurrentCube.Adjacent[2] != null)
            {
                CurrentCube = CurrentCube.Adjacent[2];
                transform.position = Vector3.MoveTowards(transform.position, CurrentCube.transform.position, 0.0002f);
                qbertAnim.SetBool("Jump", true);
                qbertAnim.SetInteger("Direction", 2);
            }
            Debug.Log("A");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (CurrentCube.Adjacent[3] != null)
            {
                CurrentCube = CurrentCube.Adjacent[3];
                transform.position = Vector3.MoveTowards(transform.position, CurrentCube.transform.position, 0.0002f);
                qbertAnim.SetBool("Jump", true);
                qbertAnim.SetInteger("Direction", 3);
            }
            Debug.Log("S");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Break();
        }

        ;

	}
}
