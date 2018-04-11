using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallScript : MonoBehaviour {

    // Components
    Animator animator; 

    bool startSequence = true;
    NodeScript currentNode;
    private bool moving = false;
    Vector3 posOffset;

    public bool Move { get; private set; }

    // Use this for initialization
    public void StartScript (NodeScript node) {
        animator = GetComponent<Animator>();
        startSequence = true;
        currentNode = node;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, posOffset, 0.005f);
        }
        if (transform.position == posOffset)
        {
            moving = false;
        }

    }

    private IEnumerator MovementRoutine()
    {
        Debug.Log("Hello");
        if (startSequence)
        {
            Debug.Log("Alive");
            posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.15f, 0);
            ;
            if (transform.position == posOffset)
            {
                animator.SetBool("Collision", true);
                startSequence = false;
                yield return null;//new WaitForSeconds(1.0f);
            }
        }
        else
        {
 
                currentNode = currentNode.Adjacent[Random.Range(2, 4)];
                posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.15f, 0);
                moving = true;
                yield return null;//new WaitForSeconds(1.0f);
        }
    }
}
