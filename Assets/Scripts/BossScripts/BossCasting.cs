using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script handles boss spell casting
public class BossCasting : MonoBehaviour
{
    public Transform SpellTemplate;
    public CASTING_PHASE CurrentCastPhase = CASTING_PHASE.READY;
    public int ChargesLeft = 0;
    public int MaxChargesCasting = 3;
    public int MaxChargedCastingEnraged = 6;
    public int MaxChargesEnraged = 3;
    public float BetweenChargeCooldown = 1f;
    public float BetweenChargeCooldownEnraged = 1f;
    private float CurrentBetweenChargeCooldown = 2f;
    public float BetweenCastCooldown = 3f;
    public float BetweenCastCooldownEnraged = 3f;
    private float CurrentBetweenCastCooldown = 3f;

    private GameObject player;
    private BossBrain bossBrain;


    // Base initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossBrain = gameObject.GetComponent<BossBrain>();
    }

    // Update checks if any spell cast charge is left and if it able to cast
    void Update()
    {
        // Cast only when on totem phase and when enraged
        if ((bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.CurrentStateTotem.Equals(BossBrain.TOTEM_PHASE_STATES.CASTING))
            || (bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.MELEE_PHASE) && bossBrain.isEnraged))
        {
            switch (CurrentCastPhase)
            {
                case CASTING_PHASE.READY:
                    CurrentCastPhase = CASTING_PHASE.CASTING;
                    ChargesLeft = bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.MELEE_PHASE) ? MaxChargesEnraged : 
                        ((bossBrain.isEnraged) ? MaxChargedCastingEnraged : MaxChargesCasting);
                    CurrentBetweenChargeCooldown = 0f;
                    break;
                case CASTING_PHASE.CASTING:
                    // Cast only when charges left
                    if(ChargesLeft > 0)
                    {
                        CurrentBetweenChargeCooldown -= Time.deltaTime;
                        if(CurrentBetweenChargeCooldown <= 0)
                        {
                            // Cast when player on the ground
                            if (!player.GetComponent<PlayerJump>().HasJumped())
                            {
                                ChargesLeft--;
                                Vector3 playerPos = player.transform.position;
                                //playerPos.y -= 10;
                                Instantiate(SpellTemplate, playerPos, Quaternion.identity);
                                CurrentBetweenChargeCooldown = (bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.isEnraged) ? BetweenChargeCooldownEnraged : BetweenChargeCooldown;
                            }
                            else
                            {
                                // When not on ground add one charge
                                if(bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && ChargesLeft < ((bossBrain.isEnraged) ? MaxChargedCastingEnraged : MaxChargesCasting))
                                {
                                    ChargesLeft++;
                                    CurrentBetweenChargeCooldown = (bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.isEnraged) ? BetweenChargeCooldownEnraged : BetweenChargeCooldown;
                                }
                            }
                        }
                    }
                    else
                    {
                        CurrentCastPhase = CASTING_PHASE.ON_COOLDOWN;
                        CurrentBetweenCastCooldown = (bossBrain.CurrentPhase.Equals(BossBrain.MY_PHASE.TOTEM_PHASE) && bossBrain.isEnraged) ? BetweenCastCooldownEnraged : BetweenCastCooldown;
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
