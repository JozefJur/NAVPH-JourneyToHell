using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCasting : MonoBehaviour
{
    public Transform SpellTemplate;
    public CASTING_PHASE CurrentCastPhase = CASTING_PHASE.READY;
    public int ChargesLeft;
    public int MaxCharges = 3;
    public float BetweenChargeCooldown = 2f;
    private float CurrentBetweenChargeCooldown = 2f;
    public float BetweenCastCooldown = 5f;
    private float CurrentBetweenCastCooldown = 5f;

    private GameObject player;
    private BossBrain bossBrain;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossBrain = gameObject.GetComponent<BossBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.CurrentStateTotem.Equals(BossBrain.TOTEM_PHASE_STATES.CASTING))
        {
            switch (CurrentCastPhase)
            {
                case CASTING_PHASE.READY:
                    CurrentCastPhase = CASTING_PHASE.CASTING;
                    ChargesLeft = MaxCharges;
                    break;
                case CASTING_PHASE.CASTING:
                    if(ChargesLeft > 0)
                    {
                        CurrentBetweenChargeCooldown -= Time.deltaTime;
                        if(CurrentBetweenChargeCooldown <= 0)
                        {
                            if (!player.GetComponent<PlayerJump>().HasJumped())
                            {
                                ChargesLeft--;
                                Vector3 playerPos = player.transform.position;
                                //playerPos.y -= 10;
                                Instantiate(SpellTemplate, playerPos, Quaternion.identity);
                                CurrentBetweenChargeCooldown = BetweenChargeCooldown;
                            }
                        }
                    }
                    else
                    {
                        CurrentCastPhase = CASTING_PHASE.ON_COOLDOWN;
                        CurrentBetweenCastCooldown = BetweenCastCooldown;
                    }
                    break;
                case CASTING_PHASE.ON_COOLDOWN:

                    CurrentBetweenCastCooldown -= Time.deltaTime;
                    if (CurrentBetweenCastCooldown <= 0)
                    {
                        CurrentCastPhase = CASTING_PHASE.READY;
                    }
                    break;
            }
        }
    }

    public enum CASTING_PHASE
    {
        READY,
        CASTING,
        ON_COOLDOWN
    }

}
