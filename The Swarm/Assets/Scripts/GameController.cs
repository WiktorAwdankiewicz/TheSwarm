using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject swarmLandUnit;
    public Vector3 swarmSpawnValues;
    public static int lives;
    public static int remainingUnits;
    public static float swarmLandUnitSpeed;

    [SerializeField]
    private int swarmSize;
    [SerializeField]
    private float swarmSpawnWait;
    [SerializeField]
    private float startWait;

    public TMP_Text livesCounterTMP;
    public TMP_Text remainingUnitsTMP;

    int swarmSizeCounter = 0;
    CircleCollider2D swarmLandUnitCollider;
    float spawnCollisionCheckRadius;

    private void Start()
    {
        lives = 3;
        remainingUnits = 0;
        swarmLandUnitSpeed = 2;
        remainingUnits = swarmSize;
        livesCounterTMP.text = "Lives: " + lives;
        remainingUnitsTMP.text = "Swarm size: " + remainingUnits;
        StartCoroutine(SpawnSwarm());
    }

    private void FixedUpdate()
    {
        livesCounterTMP.text = "Lives: " + lives;
        remainingUnitsTMP.text = "Swarm size: " + remainingUnits;
        if (remainingUnits == 0)
        {
            SceneManager.LoadScene(3);
        }
    }

    IEnumerator SpawnSwarm()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            Vector3 swarmSpawnPosition = new Vector3(Random.Range(-swarmSpawnValues.x, swarmSpawnValues.x), Random.Range(-swarmSpawnValues.y, swarmSpawnValues.y), swarmSpawnValues.z);
            swarmLandUnitCollider = swarmLandUnit.GetComponent<CircleCollider2D>();
            spawnCollisionCheckRadius = swarmLandUnitCollider.radius;
            if (Physics2D.OverlapCircle(swarmSpawnPosition, spawnCollisionCheckRadius) == null)
            {
                Instantiate(swarmLandUnit, swarmSpawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(swarmSpawnWait);
                swarmSizeCounter++;
            }
            if (swarmSizeCounter >= swarmSize)
            {
                break;
            }
        }
    }
}
