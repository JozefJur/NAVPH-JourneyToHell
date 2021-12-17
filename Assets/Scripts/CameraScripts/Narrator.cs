using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Narrator : MonoBehaviour
{

    public GameObject spawnParticles;
    
    public int numberOfPoints;

    public List<GameObject> activeSpawnPoints;

    private List<GameObject> currentSpawnPoints;
    
    public int maxNumberOfMonstersAtTheSameTime;
    public List<GameObject> allActiveMonsters;

    public int creditGainMultiplier;
    public float creditGainTimeout;
    public NARRATOR_STATE currentState = NARRATOR_STATE.IDLE;

    public int maxNumOfMonsterOnSpawnSequence;

    private float currentCreditGainTimeout;

    public float spawnTimeout;
    private float currentSpawnTimeout;
    public float betweenSpawnTimeout = 5f;
    private float currentBetweenSpawnTimeout = 5f;

    public int numOfPortalsAtTheSameTime = 3;

    public Hashtable unitVariation;

    public bool canSpawn;

    private GameObject[] monsters;

    private int monstersRemainingSpawn;

    private void getAllMonsters()
    {
        monsters = Resources.LoadAll<GameObject>("Monsters");

        Debug.Log(monsters.Length);

        monsters = monsters.OrderBy(o => o.GetComponent<MonsterTemplate>().spawnChance()).ToList().ToArray();

        Debug.Log(monsters.Length);

        currentSpawnPoints = new List<GameObject>();
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

    private void spawnMonsters()
    {
        if (!currentState.Equals(NARRATOR_STATE.SPAWNING))
        {
            currentSpawnTimeout -= Time.deltaTime;
            if (canSpawn && currentSpawnTimeout <=0 && activeSpawnPoints.Count > 0 && allActiveMonsters.Count < maxNumberOfMonstersAtTheSameTime)
            {
                currentSpawnPoints.Clear();
                monstersRemainingSpawn = maxNumberOfMonstersAtTheSameTime - allActiveMonsters.Count >= maxNumOfMonsterOnSpawnSequence ? maxNumOfMonsterOnSpawnSequence : maxNumberOfMonstersAtTheSameTime - allActiveMonsters.Count;
                currentState = NARRATOR_STATE.SPAWNING;
                List<int> indexes = new List<int>();
                for(int i =0;i< activeSpawnPoints.Count; i++)
                {
                    indexes.Add(i);
                }
                indexes = indexes.OrderBy(tvz => System.Guid.NewGuid()).ToList();
                for (int i= 0; i < ((activeSpawnPoints.Count < monstersRemainingSpawn) ? activeSpawnPoints.Count : monstersRemainingSpawn); i++)
                {
                    currentSpawnPoints.Add(activeSpawnPoints[indexes[i]]);
                }

            }
        }
    }
    
    public void removeMonster(GameObject monster)
    {
        allActiveMonsters.Remove(monster);
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

    public void registerSpawnPoint(GameObject spawnPoint)
    {
        activeSpawnPoints.Add(spawnPoint);
    }

    public void removeSpawnPoint(GameObject spawnPoint)
    {
        activeSpawnPoints.Remove(spawnPoint);
    }

    void FixedUpdate()
    {
        if (currentState.Equals(NARRATOR_STATE.SPAWNING))
        {
            currentBetweenSpawnTimeout -= Time.deltaTime;
            if(currentBetweenSpawnTimeout <= 0)
            {
                if(activeSpawnPoints.Count > 0)
                {
                    currentBetweenSpawnTimeout = betweenSpawnTimeout;
                    currentSpawnTimeout = spawnTimeout;
                    Transform currentSpawnPoint = currentSpawnPoints[0].transform;
                    currentSpawnPoints.RemoveAt(0);
                    Vector2 newP = new Vector2(currentSpawnPoint.position.x, currentSpawnPoint.position.y + 3);
                    GameObject portal = Instantiate(spawnParticles, newP, Quaternion.identity);
                    StartCoroutine(spawnEnemy(currentSpawnPoint, portal));
                    //allActiveMonsters.Add(Instantiate(monsters[Random.Range(0, monsters.Length)], currentSpawnPoint.position, Quaternion.identity));
                    monstersRemainingSpawn--;

                    //Random.Range(0, monsters.Length -1);
                    if(currentSpawnPoints.Count == 0)
                    {
                        currentState = NARRATOR_STATE.IDLE;
                        currentBetweenSpawnTimeout = 0;
                    }

                }
            }
        }
    }

    IEnumerator spawnEnemy(Transform point, GameObject  portal)
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
            allActiveMonsters.Add(Instantiate(monster, point.position, Quaternion.identity));
        }

        yield return new WaitForSeconds(temp.numPerSpawn());

        Destroy(portal);

        //allActiveMonsters.Add(Instantiate(monsters[Random.Range(0, monsters.Length)], point.position, Quaternion.identity));
    }

    public enum NARRATOR_STATE
    {
        SPAWNING,
        IDLE
    }
}
