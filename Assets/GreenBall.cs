using System.Collections;
using UnityEngine;

public class GreenBall : EnemyBase
{
    // Components
    private Animator animator;
    SpriteRenderer sprite;
    AudioSource audioSource;
    [SerializeField]AudioClip QbertPrize;

    private bool animate = true;

    private bool startSequence = true;
    private bool moving = false;
    private Vector3 posOffset;

    public bool Alive = false;

    bool play = true;

    // Use this for initialization
    public override void StartScript(NodeScript node)
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        startSequence = true;
        Alive = true;
        currentNode = node;
        StartCoroutine(MovementRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, posOffset, 0.02f);
        }
        if (transform.position == posOffset)
        {
            if (animate)
            {
                audioSource.Play();
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

    void FreezeGame()
    {
        GameManager.enemyUpdate = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Qbert")
        {
            if (play)
            {
                audioSource.clip = QbertPrize;
                audioSource.Play();
                play = false;

            }
            
            GameManager.enemyUpdate = false;
            GameManager.Score += 100;
            sprite.enabled = false;
            Invoke("FreezeGame", 2.0f);
            Destroy(gameObject,3.0f);
        }
    }
}