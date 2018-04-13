using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QbertScript : MonoBehaviour
{

    //GameObject Component References
    Animation qBertAnimationComponent;
    Animator qbertAnim;
    [SerializeField]GameObject DeathSprite;

    //Current Cube
    NodeScript currentCube;
    NodeScript PreviousNode;

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

    bool moveNow = false;
    bool AllowInput = true;


    // Use this for initialization
    void Start()
    {
        qBertAnimationComponent = GetComponent<Animation>();
        currentCube = transform.parent.GetComponent<NodeScript>();
        qbertAnim = GetComponent<Animator>();
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
                moveNow = false;
                if (currentCube.tag == "nullNode")
                {
                    DeathSprite.SetActive(true);
                    Invoke("DeathAnim", 2.0f);
                    return;
                }
            }
        }
    }

    void InputManager()
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

        void QbertMove(NodeScript destNode, int _dir)
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

    void DisableJump()
        {

            AllowInput = true;
            qbertAnim.SetBool("Jump", false);

        }

        void DeathAnim(){
        
            DeathSprite.SetActive(false);
            CurrentCube = PreviousNode;
            moveNow = true;

        }

}
