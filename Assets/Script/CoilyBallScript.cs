using UnityEngine;

public class CoilyBallScript : EnemyBase
{
    public static CoilyBallScript instance;
    // Components
    private Animator animator;
    AudioSource audioSource;

    [SerializeField] private GameObject CoilyPrefab;

    [SerializeField] private float enemySpeed = 0.01f;
    [SerializeField] private float movementDelay = 0.05f;

    private bool animate = true;

    private bool startSequence = true;
    private bool moving = false;
    private Vector3 posOffset;

    public bool Alive = false;

    // Use this for initialization
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

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        startSequence = true;
        Alive = true;
        currentNode = node;
        MovementRoutine();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.enemyUpdate)
        {
            if (moving)
            {
                transform.position = Vector2.MoveTowards(transform.position, posOffset, enemySpeed * Time.deltaTime);

                if (transform.position == posOffset)
                {
                    if (animate)
                    {
                        audioSource.Play();
                        animator.SetBool("Collision", true);
                        animate = false;
                    }
                    moving = false;
                    Invoke("MovementRoutine", movementDelay);
                }
            }
        }
    }

    private void MovementRoutine()
    {
        if (Alive)
        {
            if (startSequence)
            {
                animate = true;
                posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
                moving = true;
                startSequence = false;
            }
            else
            {
                if (currentNode.Adjacent[Random.Range(2, 4)].tag == "nullNode")
                {
                    //Instatiate Coily
                    //Debug.Log("SpawnCoily");
                    posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.20f, 0);
                    GameObject coily = Instantiate(CoilyPrefab, posOffset, new Quaternion());
                    coily.GetComponent<CoilyScript>().StartScript(currentNode);

                    Destroy(gameObject);
                }
                else
                {
                    animate = true;
                    currentNode = currentNode.Adjacent[Random.Range(2, 4)];
                    posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
                    moving = true;
                }
            }
        }
    }

    public void DisableCollision()
    {
        animator.SetBool("Collision", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int count = 0;
        if (collision.tag == "Qbert")
        {
            if (count++ == 0)
            {
                GameManager.playerLives--;
            }
        }
    }
}