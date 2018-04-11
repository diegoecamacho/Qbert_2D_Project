using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QbertScript : MonoBehaviour
{

    //GameObject Component References
    Animation qBertAnimationComponent;
    SpriteRenderer qbertSpriteRend;
    Animator qbertAnim;

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

    /// <summary>
    /// QbertS Idle Sprites Array
    /// </summary>
    [SerializeField] Sprite[] QbertIdleSprites;
    int movementDir = 0;

    bool moveNow = false;


    // Use this for initialization
    void Start()
    {
        qBertAnimationComponent = GetComponent<Animation>();
        qbertSpriteRend = GetComponent<SpriteRenderer>();
        currentCube = transform.parent.GetComponent<NodeScript>();
        qbertAnim = GetComponent<Animator>();
        if (CurrentCube != null)
        {
            Debug.Log(CurrentCube.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputManager();
        if (moveNow)
        {
            Vector3 posOffset = new Vector3(CurrentCube.transform.position.x, CurrentCube.transform.position.y + 0.15f, 0);
            transform.position = Vector2.MoveTowards(transform.position, posOffset, 0.05f);
            if (transform.position == posOffset)
            {
                Debug.Log(movementDir);
                moveNow = false;
            }
        }
    }

    void InputManager()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QbertMove(CurrentCube.Adjacent[0], 0);
            Debug.Log("Q");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            QbertMove(CurrentCube.Adjacent[1], 1);
            Debug.Log("W");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            QbertMove(CurrentCube.Adjacent[2], 2);
            Debug.Log("A");
        }
        else if (Input.GetKeyDown(KeyCode.S)){
            QbertMove(CurrentCube.Adjacent[3], 3);
            Debug.Log("C");
        }

         if (Input.GetKeyDown(KeyCode.Escape))
         {
                Debug.Break();
         }

         if (Input.GetKeyDown(KeyCode.Space))
         {
            qbertSpriteRend.sprite = QbertIdleSprites[0];
            Debug.Log(0);
         }
    }

        void QbertMove(NodeScript destNode, int _dir)
        {
            if (currentCube)
            {
                CurrentCube = destNode;
                CurrentCube.Selected = true;
                movementDir = _dir;
                moveNow = true;
                //qBertAnimationComponent.Play();
                qbertAnim.SetBool("Jump", true);
                qbertAnim.SetFloat("Direction", _dir);
               
        }
        }

        void DisableJump()
        {
            qbertAnim.SetBool("Jump", false);

        }
}
