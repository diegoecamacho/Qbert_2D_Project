using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] NodeScript[] SpawnLocations;
    [SerializeField] GameObject CoilyPrefab;
    [SerializeField] GameObject RedBallPrefab;
    [SerializeField] GameObject GreenBallPrefab;

    [SerializeField] float delay = 2.0f;

    int currentSpawnLoc = 0;
    int SpawnObject = 0;


    public bool CoilyAlive = false; //TODO: Move to Coily Script

    public bool ContinueGame = true;

    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnerRoutine());
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private IEnumerator SpawnerRoutine()
    {
        while (ContinueGame) //TODO: Global Disable
        {
            if (!CoilyAlive)
            {
                SpawnObject = 0;
                CoilyAlive = true;
            }
            else
            {
                SpawnObject = Random.Range(1, 3);
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
        currEnemy.GetComponent<RedBallScript>().StartScript(SpawnLocations[currentSpawnLoc]);
        //Debug.Log("Spawn");
    }
}
