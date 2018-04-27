using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private NodeScript[] SpawnLocations;
    [SerializeField] private GameObject CoilyPrefab;
    [SerializeField] private GameObject RedBallPrefab;
    [SerializeField] private GameObject GreenBallPrefab;

    [SerializeField] private float delay = 2.0f;

    private int currentSpawnLoc = 0;
    private int SpawnObject = 0;

    public bool CoilyAlive = false; //TODO: Move to Coily Script

    public bool ContinueGame = true;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(SpawnerRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private IEnumerator SpawnerRoutine()
    {
        while (ContinueGame) //TODO: Global Disable
        {
            if (GameManager.enemyUpdate)
            {
                //ContinueGame = false;
                if (CoilyScript.instance == null && CoilyBallScript.instance == null)
                {
                    SpawnObject = 0;
                }
                else
                {
                    int rand = Random.Range(1, 11);
                    if (rand <= 6)
                    {
                        SpawnObject = 1;
                    }
                    else
                    {
                        SpawnObject = 2;
                    }
                }

                switch (SpawnObject)
                {
                    case 0:
                        SpawnEnemy(CoilyPrefab);
                        break;

                    case 1:
                        SpawnEnemy(RedBallPrefab);
                        break;

                    case 2:
                        SpawnEnemy(GreenBallPrefab);
                        break;
                }
            }
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        currentSpawnLoc = Random.Range(0, SpawnLocations.Length);
        //Debug.Log(currentSpawnLoc);
        Vector3 currentNode = SpawnLocations[currentSpawnLoc].transform.position;
        Vector3 SpawnLoc = new Vector3(currentNode.x, currentNode.y + 1, currentNode.z);
        GameObject currEnemy = Instantiate(enemyPrefab, SpawnLoc, new Quaternion());
        currEnemy.GetComponent<EnemyBase>().StartScript(SpawnLocations[currentSpawnLoc]);
        //Debug.Log("Spawn");
    }
}