using UnityEngine;

public class QbertScript : MonoBehaviour
{
    //GameObject Component References
    BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;


    /// <summary>
    /// Player Stats
    /// </summary>
    private int playerLives = 3;

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

    public int PlayerLives
    {
        get
        {
            return playerLives;
        }

        set
        {
            playerLives = value;
        }
    }

    [SerializeField] float movementSpeed = 0.05f;

    private bool moveNow = false;
    private bool AllowInput = true;
    public bool onElevator = false;
    public bool onDeathAnim = false;

    // Use this for initialization
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        qbertAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        InputManager();
        if (moveNow)
        {
            Vector3 posOffset = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.15f, 0);
            transform.position = Vector2.MoveTowards(transform.position, posOffset, movementSpeed);
            if (transform.position == posOffset)
            {
                moveNow = false;
                if (currentCube.tag == "nullNode")
                {
                    playerLives--;
                    GameManager.enemyUpdate = false;
                    DeathSprite.SetActive(true);
                    AllowInput = false;
                    Invoke("FallDeathAnim", 0.5f);
                    return;
                }
                if (currentCube.tag == "ElevatorNode")
                {
                    
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
            else
            {
                boxCollider.enabled = true;

            }
            if (onDeathAnim)
            {
                GameManager.enemyUpdate = true;
                transform.position = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.15f, 0);
                onDeathAnim = false;
                return;

            }
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                QbertMove(CurrentCube.Adjacent[0], 0);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                QbertMove(CurrentCube.Adjacent[1], 1);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                QbertMove(CurrentCube.Adjacent[2], 2);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                QbertMove(CurrentCube.Adjacent[3], 3);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Break();
        }
    }
    /// <summary>
    /// enables Qbert movement to new node
    /// </summary>
    /// <param name="destNode">Destination node</param>
    /// <param name="_dir">Direction of destination node</param>

    private void QbertMove(NodeScript destNode, int _dir)
    {
        
        AllowInput = false;
        qbertAnim.SetBool("Jump", true);
        qbertAnim.SetFloat("Direction", _dir);

        PreviousNode = CurrentCube;
        CurrentCube = destNode;
        Move();
    }

    private void Move()
    {
        CurrentCube.Selected = true;
        moveNow = true;
        Invoke("DisableJump", 0.2f);
    }

    private void DisableJump()
    {
        AllowInput = true;
        qbertAnim.SetBool("Jump", false);
    }

    /// <summary>
    /// Enables Physics to play fall animation
    /// </summary>
    private void FallDeathAnim()
    {
        
        DeathSprite.SetActive(false);
        CurrentCube = PreviousNode;
        Invoke("MoveBack", 0.2f);
    }
     /// <summary>
     /// Returns player back to position before fall
     /// <see cref="FallDeathAnim"/>
     /// </summary>
    private void MoveBack()
    {
        onDeathAnim = true;
        Invoke("DisableJump", 0.3f);

        return;
    }

  

   

    
}