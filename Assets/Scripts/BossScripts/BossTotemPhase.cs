using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script is used for boss
// Script contains logic for boss entity during totem phase
public class BossTotemPhase : MonoBehaviour
{

    public Transform Player;
    public Transform TotemPhasePlace;
    public Vector3 OriginalPlace;
    public GameObject Totem;

    private BossBrain bossBrain;
    private BossCasting castingController;
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
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        castingController = gameObject.GetComponent<BossCasting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE))
        {

            if (bossBrain.CurrentStateTotem.Equals(BossBrain.TOTEM_PHASE_STATES.CASTING))
            {
                if(Totem == null)
                {
                    bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.EXITING;
                }
            }

            switch (bossBrain.CurrentStateTotem)
            {
                case BossBrain.TOTEM_PHASE_STATES.INITIALIZING:
                    // On initialize, boss can not be damaged
                    gameObject.GetComponent<MonsterHealth>().CanHit = false;
                    gameObject.GetComponent<BossMovement>().SetCurrentVelocity(new Vector2(0, 0));
                    bossAnimator.SetLayerWeight(0, 0);
                    bossAnimator.SetLayerWeight(1, 1);
                    bossAnimator.SetTrigger("teleport_totem");
                    bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.TELEPORTING;
                    break;
                case BossBrain.TOTEM_PHASE_STATES.EXITING:
                    // On totem exit, enable boss damage, set melee phase state
                    bossAnimator.SetLayerWeight(1, 0);
                    bossAnimator.SetLayerWeight(0, 1);
                    gameObject.transform.position = OriginalPlace;
                    gameObject.GetComponent<MonsterHealth>().CanHit = true;
                    bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.INITIALIZING;
                    bossBrain.CurrentPhase = BossBrain.MY_PHASE.MELEE_PHASE;
                    bossBrain.CurrentStateMelee = BossBrain.MELEE_PHASE_STATE.IDLE;
                    bossBrain.CurrentswitchTime = bossBrain.BaseSwitchTime;
                    break;
            }
        }
    }

    // Method creates totem on random spawn point
    private void SpawnTotem()
    {
        GameObject[] totemSpawns = GameObject.FindGameObjectsWithTag("TotemSpawn");
        Totem = totemSpawns[Random.Range(0, totemSpawns.Length - 1)].GetComponent<TotemSpawnScript>().SpawnTotem();
    }

    // Function is called from animation
    // Boss is teleported on specified position and original place is stored
    void PerformTeleport()
    {
        OriginalPlace = gameObject.transform.position;
        gameObject.transform.position = TotemPhasePlace.position;
        SpawnTotem();
    }

    // Function is called from animation
    // Funtion sets totem phase state
    void FinishTeleportTotem()
    {
        //Debug.Log("HERE ATER CAST?");
        bossBrain.CurrentStateTotem = BossBrain.TOTEM_PHASE_STATES.CASTING;
        castingController.CurrentCastPhase = BossCasting.CASTING_PHASE.READY;
    }

}
