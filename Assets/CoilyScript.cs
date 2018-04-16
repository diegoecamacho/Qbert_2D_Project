using System.Collections.Generic;
using UnityEngine;

public class CoilyScript : EnemyBase
{
    public static CoilyScript instance;

    /// <summary>
    /// Components
    /// </summary>
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool animate = false;
    private float animationDir = 0;

    //Coily Speed and Delay
    [SerializeField] private float movementSpeed = 0.2f;

    [SerializeField] private float movementDelay = 0.5f;

    //Qbert Location
    private QbertScript qbert;

    // Search Algorithm
    private Queue<NodeScript> movementQueue;

    private Queue<int> animationQueue;
    private NodeScript searchNode;
    private NodeScript destCube = null;

    //Movement
    public bool moveCoily = true;

    private Vector3 posOffset;

    //Begin Update
    private bool startUpdate = false;

    private bool onElevator = false;

    public override void StartScript(NodeScript node)
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        base.StartScript(node);

        qbert = GameObject.FindGameObjectWithTag("Qbert").GetComponent<QbertScript>();

        movementQueue = new Queue<NodeScript>();
        animationQueue = new Queue<int>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        LoadPath();
        Dequeue();

        posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.25f, 0);

        startUpdate = true;
    }

    private void Update()
    {
        if (GameManager.enemyUpdate)
        {
            if (startUpdate)
            {
                if (qbert.GetComponent<QbertScript>().CurrentCube == currentNode && transform.position == posOffset)
                {
                    Debug.Log("Same Cube");
                    Destroy(gameObject);
                }

                if (destCube != qbert.CurrentCube && !onElevator)
                {
                    destCube = qbert.CurrentCube;
                    LoadPath();
                }
                if (moveCoily)
                {
                    if (animate)
                    {
                        Debug.Log(animationDir);
                        animator.SetFloat("Direction", animationDir);
                        animator.SetBool("Jump", true);
                        animate = false;
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, posOffset, movementSpeed);

                        if (transform.position == posOffset)
                        {
                            if (currentNode.tag == "ElevatorNode")
                            {
                                Invoke("DeathSequence", 0.5f);

                                Destroy(gameObject, 2.0f);
                                return;
                            }
                            else
                            {
                                moveCoily = false;
                                Invoke("Dequeue", movementDelay);
                            }
                        }
                    }
                }
            }
        }
    }

    private void DeathSequence()
    {
        startUpdate = false;
        rb.isKinematic = false;
        rb.simulated = true;
        spriteRenderer.sortingOrder = -1;
    }

    private void Dequeue()
    {
        
        if (movementQueue.Count != 0)
        {
            currentNode = movementQueue.Dequeue();
            animationDir = animationQueue.Dequeue();
        }
        else
        {
            LoadPath();
            if (movementQueue.Count == 0) return;
            currentNode = movementQueue.Dequeue();
            animationDir = animationQueue.Dequeue();
        }

        if (currentNode.tag == "ElevatorNode")
        {
            onElevator = true;
            currentNode = currentNode.GetComponent<ElevatorScript>().deadZone;
            posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.25f, 0);
        }
        else
        {
            posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.25f, 0);
        }

        animate = true;
        moveCoily = true;
    }

    private void LoadPath()
    {
        movementQueue.Clear();
        animationQueue.Clear();
        searchNode = currentNode;
        for (int i = 0; i < 3; i++)
        {
            if (qbert.CurrentCube.transform.position.y < searchNode.transform.position.y)
            {
                QueueMovement(2);
            }
            else if (qbert.transform.position.y > searchNode.transform.position.y)
            {
                QueueMovement(0);
            }
            else
            {
                QueueMovement(Random.Range(0, 2) == 0 ? 2 : 0);
            }
        }
    }

    private void QueueMovement(int _min)
    {
        int ran;
        if (qbert.CurrentCube.transform.position.x < searchNode.transform.position.x)
        {
            ran = _min;
        }
        else if (qbert.transform.position.x > searchNode.transform.position.x)
        {
            ran = _min + 1;
        }
        else
        {
            ran = (Random.Range(0, 2) == 0 ? _min : _min + 1);
        }

        if (searchNode.tag != "ElevatorNode")
        {
            if (searchNode.Adjacent[ran].tag != "nullNode")
            {
                searchNode = searchNode.Adjacent[ran];
                animationQueue.Enqueue(ran);
                movementQueue.Enqueue(searchNode);
            }
        }
    }

    private void DisableJump()
    {
        animator.SetBool("Jump", false);
    }
}