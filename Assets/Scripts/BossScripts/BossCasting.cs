using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCasting : MonoBehaviour
{
    public Transform SpellTemplate;
    public CASTING_PHASE CurrentCastPhase = CASTING_PHASE.READY;
    public int ChargesLeft;
    public int MaxChargesCasting = 3;
    public int MaxChargedCastingEnraged = 6;
    public int MaxChargesEnraged = 3;
    public float BetweenChargeCooldown = 2f;
    public float BetweenChargeCooldownEnraged = 1f;
    private float CurrentBetweenChargeCooldown = 2f;
    public float BetweenCastCooldown = 5f;
    public float BetweenCastCooldownEnraged = 3f;
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
        if ((bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.CurrentStateTotem.Equals(BossBrain.TOTEM_PHASE_STATES.CASTING))
            || (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.MELEE_PHASE) && bossBrain.isEnraged))
        {
            switch (CurrentCastPhase)
            {
                case CASTING_PHASE.READY:
                    CurrentCastPhase = CASTING_PHASE.CASTING;
                    ChargesLeft = bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.MELEE_PHASE) ? MaxChargesEnraged : 
                        ((bossBrain.isEnraged) ? MaxChargedCastingEnraged : MaxChargesCasting);
                    CurrentBetweenChargeCooldown = 0f;
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
                                CurrentBetweenChargeCooldown = (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.isEnraged) ? BetweenChargeCooldownEnraged : BetweenChargeCooldown;
                            }
                            else
                            {
                                if(bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && ChargesLeft < ((bossBrain.isEnraged) ? MaxChargedCastingEnraged : MaxChargesCasting))
                                {
                                    ChargesLeft++;
                                    CurrentBetweenChargeCooldown = (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.isEnraged) ? BetweenChargeCooldownEnraged : BetweenChargeCooldown;
                                }
                            }
                        }
                    }
                    else
                    {
                        CurrentCastPhase = CASTING_PHASE.ON_COOLDOWN;
                        CurrentBetweenCastCooldown = (bossBrain.currentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.isEnraged) ? BetweenCastCooldownEnraged : BetweenCastCooldown;
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
