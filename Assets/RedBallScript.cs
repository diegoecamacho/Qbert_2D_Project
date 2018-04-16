﻿using System.Collections;
using UnityEngine;

public class RedBallScript : EnemyBase
{
    // Components
    private Animator animator;

    private bool animate = true;

    private bool startSequence = true;
    private bool moving = false;
    private Vector3 posOffset;

    public bool Alive = false;

    // Use this for initialization
    public override void StartScript(NodeScript node)
    {
        animator = GetComponent<Animator>();
        startSequence = true;
        Alive = true;
        currentNode = node;
        StartCoroutine(MovementRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.enemyUpdate)
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
                if (currentNode.tag == "nullNode")
                {
                    Destroy(gameObject);
                }
            }
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

    public void DisableCollision()
    {
        animator.SetBool("Collision", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Qbert")
        {
            collision.GetComponent<QbertScript>().PlayerLives--;
            Debug.Log(collision.GetComponent<QbertScript>().PlayerLives);
            Destroy(gameObject);
        }
    }
}