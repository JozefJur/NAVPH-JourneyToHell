using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Script handles all monster spawning on stage
public class Narrator : MonoBehaviour
{

    public GameObject SpawnParticles;
    public int NumberOfPoints;
    public List<GameObject> ActiveSpawnPoints;
    public int MaxNumberOfMonstersAtTheSameTime = 20;
    public List<GameObject> AllActiveMonsters;
    public int CreditGainMultiplier = 1;
    public float CreditGainTimeout = 30;
    public NARRATOR_STATE CurrentState = NARRATOR_STATE.IDLE;
    public int MaxNumOfMonsterOnSpawnSequence = 2;
    public float SpawnTimeout = 20f;
    public float BetweenSpawnTimeout = 2f;
    public bool CanSpawn;
    public int NumOfPortalsAtTheSameTime = 3;
    public Hashtable UnitVariation;



    private float currentCreditGainTimeout;
    private float currentSpawnTimeout;
    private float currentBetweenSpawnTimeout = 5f;
    private GameObject[] monsters;
    private List<GameObject> currentSpawnPoints;
    private int monstersRemainingSpawn;

    // Function loads all monsters from resources folder
    private void GetAllMonsters()
    {
        monsters = Resources.LoadAll<GameObject>("Monsters");

        //Debug.Log(monsters.Length);

        monsters = monsters.OrderBy(o => o.GetComponent<MonsterTemplate>().spawnChance()).ToList().ToArray();

        Debug.Log(monsters.Length);

        currentSpawnPoints = new List<GameObject>();
    }

    // Base initialization
    void Start()
    {
        GetAllMonsters();
        currentCreditGainTimeout = CreditGainTimeout;
        currentSpawnTimeout = 0;
        NumberOfPoints = 120;
        CanSpawn = true;
    }

    void Update()
    {
        CalcucateCredit();
        SpawnMonsters();
    }

    // Utility function that checks if player is hitting ground
    private bool HittingGround(Transform point)
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

    /*private bool validSpawnLocation()
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
    }*/

    // Function is used to determine spawn locaions and number of monsters to spawn
    private void SpawnMonsters()
    {
        // check state
        if (!CurrentState.Equals(NARRATOR_STATE.SPAWNING))
        {
            currentSpawnTimeout -= Time.deltaTime;
            if (CanSpawn && currentSpawnTimeout <=0 && ActiveSpawnPoints.Count > 0 && AllActiveMonsters.Count < MaxNumberOfMonstersAtTheSameTime)
            {
                // Clear last spawnpoints location
                currentSpawnPoints.Clear();
                // Get number of monsters to spawn
                monstersRemainingSpawn = MaxNumberOfMonstersAtTheSameTime - AllActiveMonsters.Count >= MaxNumOfMonsterOnSpawnSequence ? MaxNumOfMonsterOnSpawnSequence : MaxNumberOfMonstersAtTheSameTime - AllActiveMonsters.Count;
                CurrentState = NARRATOR_STATE.SPAWNING;
                List<int> indexes = new List<int>();
                for(int i =0;i< ActiveSpawnPoints.Count; i++)
                {
                    indexes.Add(i);
                }
                // Random order
                indexes = indexes.OrderBy(tvz => System.Guid.NewGuid()).ToList();
                for (int i= 0; i < ((ActiveSpawnPoints.Count < monstersRemainingSpawn) ? ActiveSpawnPoints.Count : monstersRemainingSpawn); i++)
                {
                    currentSpawnPoints.Add(ActiveSpawnPoints[indexes[i]]);
                }

            }
        }
    }
    
    // Function is used to remove monster from active list
    public void RemoveMonster(GameObject monster)
    {
        AllActiveMonsters.Remove(monster);
    }

    // Function adds credit to the pool
    private void CalcucateCredit()
    {
        currentCreditGainTimeout -= Time.deltaTime;
        if (currentCreditGainTimeout <= 0)
        {
            NumberOfPoints += CreditGainMultiplier * 40;
            currentCreditGainTimeout = CreditGainTimeout;
        }
    }

    // Function adds active spawn point to the pool
    public void RegisterSpawnPoint(GameObject spawnPoint)
    {
        ActiveSpawnPoints.Add(spawnPoint);
    }

    // Function removes spawn point from the pool
    public void RemoveSpawnPoint(GameObject spawnPoint)
    {
        ActiveSpawnPoints.Remove(spawnPoint);
    }

    // Narrator spawns monsters whe possible
    void FixedUpdate()
    {
        if (CurrentState.Equals(NARRATOR_STATE.SPAWNING))
        {
            currentBetweenSpawnTimeout -= Time.deltaTime;
            if(currentBetweenSpawnTimeout <= 0)
            {
                if(ActiveSpawnPoints.Count > 0)
                {
                    // When spawnpoint is present start spawning
                    currentBetweenSpawnTimeout = BetweenSpawnTimeout;
                    currentSpawnTimeout = SpawnTimeout;
                    Transform currentSpawnPoint = currentSpawnPoints[0].transform;
                    currentSpawnPoints.RemoveAt(0);
                    Vector2 newP = new Vector2(currentSpawnPoint.position.x, currentSpawnPoint.position.y + 3);
                    // Create portal
                    GameObject portal = Instantiate(SpawnParticles, newP, Quaternion.identity);
                    // Start spawning routine
                    StartCoroutine(SpawnEnemy(currentSpawnPoint, portal));
                    //allActiveMonsters.Add(Instantiate(monsters[Random.Range(0, monsters.Length)], currentSpawnPoint.position, Quaternion.identity));
                    monstersRemainingSpawn--;

                    //Random.Range(0, monsters.Length -1);
                    if(currentSpawnPoints.Count == 0)
                    {
                        CurrentState = NARRATOR_STATE.IDLE;
                        currentBetweenSpawnTimeout = 0;
                    }

                }
            }
        }
    }

    // Coroutine that removes portal and spawns enemies
    IEnumerator SpawnEnemy(Transform point, GameObject  portal)
    {
        yield return new WaitForSeconds(5f);

        int chance = Random.Range(0, 100);

        GameObject monster = null;
        foreach(GameObject monsterArr in monsters)
        {
            MonsterTemplate tempList = monsterArr.GetComponent<MonsterTemplate>();
            //Debug.Log(tempList.spawnChance() + " " + chance);
            if(tempList.spawnChance() > chance)
            {
                monster = monsterArr;
                break;
            }
        }

        MonsterTemplate temp = monster.GetComponent<MonsterTemplate>();

        for(int i=0; i< temp.numPerSpawn(); i++)
        {
            yield return new WaitForSeconds(1f);
            AllActiveMonsters.Add(Instantiate(monster, point.position, Quaternion.identity));
        }

        yield return new WaitForSeconds(temp.numPerSpawn());

        Destroy(portal);

        //allActiveMonsters.Add(Instantiate(monsters[Random.Range(0, monsters.Length)], point.position, Quaternion.identity));
    }

    // Function destroys all registered enemies present in the stage
    public void DestroyAllEnemies()
    {
        foreach(GameObject enemy in AllActiveMonsters)
        {
            Destroy(enemy);
        }
        AllActiveMonsters.Clear();
    }

    public enum NARRATOR_STATE
    {
        SPAWNING,
        IDLE
    }
}
