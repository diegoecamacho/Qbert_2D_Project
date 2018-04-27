using UnityEngine;

public class QbertScript : MonoBehaviour
{
    //GameObject Component References
    BoxCollider2D boxCollider;
    Rigidbody2D rigidbody2D;
    AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    [SerializeField]GameObject QbertPrefab;


    [SerializeField] AudioClip QbertJump;
    [SerializeField] AudioClip QbertSwear;
    [SerializeField] AudioClip QbertFall;
    [SerializeField] AudioClip QbertElevator;
    int JumpClip = 0;
    int SwearClip = 0;
    int FallClip = 0;
    int ElevatorClip = 0;



    private Animator qbertAnim;
    [SerializeField] private GameObject DeathSprite;

    //Current Cube
    [SerializeField] private NodeScript currentCube;

    private NodeScript PreviousNode;

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

    [SerializeField] float movementSpeed = 0.05f;
    int Spawned = 0;

    private bool moveNow = false;
    private bool AllowInput = true;
    public bool onElevator = false;
    public bool onDeathAnim = false;
    private bool once = true;

    // Use this for initialization
    private void Start()
    {
        GetComponent();
    }

    void GetComponent()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        qbertAnim = GetComponent<Animator>();
        QbertPrefab = (GameObject)Resources.Load("Qbert");
    }

     public void StartScript(NodeScript startNode)
    {
        GameManager.enemyUpdate = true;
        GetComponent();
        Spawned = 0;
        CurrentCube = startNode;
        transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.15f, 0);
        moveNow = true;
        spriteRenderer.sortingOrder = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.playerUpdate)
        {
            InputManager();
            QbertMovement();
        }
        
    }

    private void QbertMovement()
    {
        if (moveNow)
        {
            Vector3 posOffset = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.15f, 0);
            transform.position = Vector2.MoveTowards(transform.position, posOffset, movementSpeed);
            if (transform.position == posOffset)
            {
                moveNow = false;
                if (currentCube.tag == "nullNode")
                {
                    if (FallClip == 0)
                    {
                        audioSource.clip = QbertFall;
                        audioSource.Play();
                        FallClip++;
                    }
                    AllowInput = false;

                    GameManager.enemyUpdate = false;
                    DeathSprite.SetActive(true);
                    Invoke("FallDeathAnim", 0.5f);
                    return;
                }
                if (currentCube.tag == "ElevatorNode")
                {
                 
                    audioSource.clip = QbertElevator;
                    audioSource.Play();
                    onElevator = true;
                    AllowInput = false;
                    return;
                }
                AllowInput = true;

            }
        }
        else
        {
            if (onElevator)
            {
                transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.10f, 0);
                boxCollider.enabled = false;
                return;
            }
            if (onDeathAnim)
            {
                onDeathAnim = false;
                return;

            }
            boxCollider.enabled = true;
            moveNow = true;
        }
    }

    /// <summary>
    /// Handles player input.
    /// </summary>
    private void InputManager()
    {
       
            if (AllowInput)
            {
                if (Input.GetKeyDown(KeyCode.Keypad7))
                {
                    QbertMove(CurrentCube.Adjacent[0], 0);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad9))
                {
                    QbertMove(CurrentCube.Adjacent[1], 1);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    QbertMove(CurrentCube.Adjacent[2], 2);
                }
                else if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    QbertMove(CurrentCube.Adjacent[3], 3);
                }
            }
    }
    /// <summary>
    /// enables Qbert movement to new node
    /// </summary>
    /// <param name="destNode">Destination node</param>
    /// <param name="_dir">Direction of destination node</param>

    private void QbertMove(NodeScript destNode, int _dir)
    {
        if (destNode != null)
        {
            AllowInput = false;
            qbertAnim.SetBool("Jump", true);
            qbertAnim.SetFloat("Direction", _dir);

            PreviousNode = CurrentCube;
            CurrentCube = destNode;
            Move();
        }
    }

    private void Move()
    {
        CurrentCube.Selected = true;
        moveNow = true;
        audioSource.clip = QbertJump;
        audioSource.Play();
        Invoke("DisableJump", 0.3f);
    }

    private void DisableJump()
    {
        boxCollider.enabled = false;
        qbertAnim.SetBool("Jump", false);
        AllowInput = true;
        Invoke("DisableInvul", 5.0f);
    }

    /// <summary>
    /// Enables Physics to play fall animation
    /// </summary>
    private void FallDeathAnim()
    {
        
        DeathSprite.SetActive(false);
        rigidbody2D.isKinematic = false;
        rigidbody2D.simulated = true;
        spriteRenderer.sortingOrder = -1;

        Invoke("MoveBack", 0.2f);
    }
     /// <summary>
     /// Returns player back to position before fall
     /// <see cref="FallDeathAnim"/>
     /// </summary>
    private void MoveBack()
    {
        onDeathAnim = true;
        if (Spawned++ == 1)
        {
            
            GameManager.playerLives--;
            GameObject Qbert = Instantiate(QbertPrefab, null);
            Qbert.GetComponent<QbertScript>().StartScript(PreviousNode);
            
        }

        Destroy(gameObject, 2.0f);

       // Invoke("DisableJump", 0.3f);
        return;
    }

    public void EnableDeathAnim(float seconds)
    {
        audioSource.clip = QbertSwear;
        audioSource.Play();
        DeathSprite.SetActive(true);
        Invoke("DisableDeathAnim", seconds);
    }

    void DisableDeathAnim()
    {
        DeathSprite.SetActive(false);
    }






}