using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilyScript :  EnemyBase{

    QbertScript qbert;

    bool moveCoily = false;
    bool moveUp = false;
    bool moveDown = false;

    Vector3 posOffset;

	private void Start()
	{
        qbert = GameObject.FindGameObjectWithTag("Qbert").GetComponent<QbertScript>();
	}
	private void Update()
	{
        CoilyLocationCheck();
        MoveCoily();
        if(moveCoily){
            transform.position = Vector2.MoveTowards(transform.position, posOffset, 0.02f);

        }
	}

	void CoilyLocationCheck(){
        Debug.Log(qbert.transform.position.y);
        Debug.Log(transform.position.y - 0.05f);

        if (qbert.transform.position.y  < transform.position.y - 0.05f)
        {
            moveDown = true;
            Debug.Log("Qbert below");
        }
        else if (qbert.transform.position.y  > transform.position.y - 0.05f)
        {
            moveUp = true;
            Debug.Log("Qbert Above");
        }
        moveCoily = true;
    }

    void MoveCoily(){
        if (moveUp)
        {
            currentNode = currentNode.Adjacent[Random.Range(0, 2)];
            posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
            moveCoily = true;
            moveUp = false;
        }
        else
        {
          if (moveDown)
          {
              currentNode = currentNode.Adjacent[Random.Range(2, 4)];
              posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
              moveCoily = true;
              moveDown = false;
          }
        }
        
    }
}
