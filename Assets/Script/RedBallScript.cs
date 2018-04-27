using System.Collections;
using UnityEngine;

public class RedBallScript : EnemyBase
{
    // Components
    private Animator animator;
    AudioSource audio;

    private bool animate = true;

    [SerializeField] float enemySpeed;

    private bool startSequence = true;
    private bool moving = false;
    private Vector3 posOffset;

    public bool Alive = false;

    // Use this for initialization
    public override void StartScript(NodeScript node)
    {
        audio = GetComponent<AudioSource>();
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
                transform.position = Vector2.MoveTowards(transform.position, posOffset, enemySpeed * Time.deltaTime);
            }
            if (transform.position == posOffset)
            {
                if (animate)
                {
                    audio.Play();
                    animator.SetBool("Collision", true);
                    startSequence = false;
                    animate = false;
                }
                moving = false;
                if (currentNode != null)
                {
                    if (currentNode.tag == "nullNode")
                    {
                        Destroy(gameObject);
                    }
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
                if (currentNode.Adjacent != null)
                {
                    animate = true;
                    currentNode = currentNode.Adjacent[Random.Range(2, 4)];
                    posOffset = new Vector3(currentNode.transform.position.x, currentNode.transform.position.y + 0.10f, 0);
                    moving = true;
                }
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
        int count = 0;
        if (collision.tag == "Qbert")
        {
            if (count++ == 0)
            {
                Alive = false;
                GameManager.playerLives--;
                Destroy(gameObject);
            }
        }
    }
}