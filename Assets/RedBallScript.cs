using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallScript : MonoBehaviour {

    // Components
    Animator animator;

    bool animate = true;

    bool startSequence = true;
    NodeScript currentNode;
    private bool moving = false;
    Vector3 posOffset;

    public bool Alive = false;

    // Use this for initialization
    public void StartScript (NodeScript node) {
        
        animator = GetComponent<Animator>();
        startSequence = true;
        Alive = true;
        currentNode = node;
        StartCoroutine(MovementRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, posOffset, 0.02f);
        }
        if (transform.position == posOffset)
        {
            if (animate)
            {
                animator.SetBool("Collision", true);
                startSequence = false;
                animate = false;
            }
            moving = false;
        }

    }

    private IEnumerator MovementRoutine()
    {
        while (Alive)
        {
            if (startSequence)
            {
                animate = true;
                posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
                moving = true;
            }
            else
            {
                animate = true;
                currentNode = currentNode.Adjacent[Random.Range(2, 4)];
                posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
                moving = true;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void DisableCollision(){
        animator.SetBool("Collision", false);
    }
}
