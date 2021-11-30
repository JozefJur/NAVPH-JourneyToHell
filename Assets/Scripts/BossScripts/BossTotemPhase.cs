using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTotemPhase : MonoBehaviour
{

    public Transform player;
    public Transform totemPhasePlace;
    public Vector3 originalPlace;
    public GameObject totem;

    private BossBrain bossBrain;
    private Vector2 currentVelocity;
    private Rigidbody2D bossRigidBody;
    private float scaleX;
    private Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bossBrain = gameObject.GetComponent<BossBrain>();
        bossRigidBody = gameObject.GetComponent<Rigidbody2D>();
        bossAnimator = gameObject.GetComponent<Animator>();
        scaleX = gameObject.transform.localScale.x;
        player = player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE))
        {

            if (bossBrain.CurrentStateTotem.Equals(BossBrain.TOTEM_PHASE_STATES.CASTING))
            {
                if(totem == null)
                {
                    bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.EXITING;
                }
            }

            switch (bossBrain.CurrentStateTotem)
            {
                case BossBrain.TOTEM_PHASE_STATES.INITIALIZING:
                    gameObject.GetComponent<MonsterHealth>().canHit = false;
                    bossAnimator.SetLayerWeight(0, 0);
                    bossAnimator.SetLayerWeight(1, 1);
                    bossAnimator.SetTrigger("teleport_totem");
                    bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.TELEPORTING;
                    break;
                case BossBrain.TOTEM_PHASE_STATES.EXITING:
                    bossAnimator.SetLayerWeight(1, 0);
                    bossAnimator.SetLayerWeight(0, 1);
                    gameObject.transform.position = originalPlace;
                    gameObject.GetComponent<MonsterHealth>().canHit = true;
                    bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.INITIALIZING;
                    bossBrain.currentPhase = BossBrain.MY_PHASE.MELEE_PHASE;
                    bossBrain.CurrentStateMelee = BossBrain.MELEE_PHASE_STATE.IDLE;
                    bossBrain.CurrentswitchTime = bossBrain.BaseSwitchTime;
                    break;
            }
        }
    }

    private void SpawnTotem()
    {
        GameObject[] totemSpawns = GameObject.FindGameObjectsWithTag("TotemSpawn");
        totem = totemSpawns[Random.Range(0, totemSpawns.Length - 1)].GetComponent<TotemSpawnScript>().SpawnTotem();
    }

    void PerformTeleport()
    {
        originalPlace = gameObject.transform.position;
        gameObject.transform.position = totemPhasePlace.position;
        SpawnTotem();
    }

    void FinishTeleportTotem()
    {
        Debug.Log("HERE ATER CAST?");
        bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.CASTING;
    }

}
