using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{

    public int numberOfPoints;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public int creditGainMultiplier;
    public float creditGainTimeout;
    public NARRATOR_STATE currentState = NARRATOR_STATE.IDLE;

    private float currentCreditGainTimeout;

    public float spawnTimeout;
    private float currentSpawnTimeout;
    public float betweenSpawnTimeout = 2f;
    private float currentBetweenSpawnTimeout = 2f;

    public bool canSpawn;

    private Vector3 currentFirstSpawnPoint;
    private Vector3 currentSecondSpawnPoint;

    private GameObject[] monsters;

    private int monstersRemainingSpawn;

    private void getAllMonsters()
    {
        monsters = Resources.LoadAll<GameObject>("Monsters");
    }

    // Start is called before the first frame update
    void Start()
    {
        getAllMonsters();
        currentCreditGainTimeout = creditGainTimeout;
        currentSpawnTimeout = 0;
        numberOfPoints = 120;
        canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        calcucateCredit();
        spawnMonsters();
    }

    private bool hittingGround(Transform point)
    {
        RaycastHit2D[] groundInfo = Physics2D.RaycastAll(point.position, Vector2.down, 3f);

        foreach (RaycastHit2D hit in groundInfo)
        {
            //Debug.Log(hit.transform.tag);
            if (hit.transform.tag.Equals("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private bool validSpawnLocation()
    {
        bool atLeastOne = false;
        currentFirstSpawnPoint = Vector3.zero;
        currentSecondSpawnPoint = Vector3.zero;
        if (hittingGround(spawnPoint1))
        {
            atLeastOne = true;
            currentFirstSpawnPoint = new Vector3(spawnPoint1.position.x, spawnPoint1.position.y, 0f);
        }

        if (hittingGround(spawnPoint2))
        {
            atLeastOne = true;
            currentSecondSpawnPoint = new Vector3(spawnPoint2.position.x, spawnPoint2.position.y, 0f);
        }

        return atLeastOne;
    }

    private void spawnMonsters()
    {
        if (!currentState.Equals(NARRATOR_STATE.SPAWNING))
        {
            currentSpawnTimeout -= Time.deltaTime;
            if (canSpawn && currentSpawnTimeout <=0 && validSpawnLocation())
            {
                monstersRemainingSpawn = 2;
                currentState = NARRATOR_STATE.SPAWNING;
            }
        }
    }

    private void calcucateCredit()
    {
        currentCreditGainTimeout -= Time.deltaTime;
        if (currentCreditGainTimeout <= 0)
        {
            numberOfPoints += creditGainMultiplier * 40;
            currentCreditGainTimeout = creditGainTimeout;
        }
    }

    void FixedUpdate()
    {
        /*if (currentState.Equals(NARRATOR_STATE.SPAWNING))
        {
            currentBetweenSpawnTimeout -= Time.deltaTime;
            if(currentBetweenSpawnTimeout <= 0)
            {
                currentBetweenSpawnTimeout = betweenSpawnTimeout;
                currentSpawnTimeout = spawnTimeout;
                if (!currentFirstSpawnPoint.Equals(Vector3.zero))
                {
                    Instantiate(monsters[Random.Range(0, monsters.Length - 1)], currentFirstSpawnPoint, Quaternion.identity);
                    monstersRemainingSpawn--;
                }
                if (!currentSecondSpawnPoint.Equals(Vector3.zero))
                {
                    Instantiate(monsters[Random.Range(0, monsters.Length - 1)], currentSecondSpawnPoint, Quaternion.identity);
                    monstersRemainingSpawn--;
                }

                //Random.Range(0, monsters.Length -1);
                if(monstersRemainingSpawn == 0)
                {
                    currentState = NARRATOR_STATE.IDLE;
                    currentBetweenSpawnTimeout = 0;
                }
            }
        }*/
    }

    public enum NARRATOR_STATE
    {
        SPAWNING,
        IDLE
    }
}
