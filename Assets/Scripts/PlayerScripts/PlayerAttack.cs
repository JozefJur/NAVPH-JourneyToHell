using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float baseDmg = 10f;
    public float currentDmg = 10f;

    public float currentAttackSpeed = 5f;
    public float baseAttackSpeed = 5f;

    public float lightAttackDuration = 2f;
    public float heavyAttackDuration = 3f;

    public float lightAttackCoolDown = 1f;
    public float heavyAttackCoolDown = 5f;

    private AttackState lightAttackState = AttackState.READY;
    private AttackState heavyAttackState = AttackState.READY;

    private float lightAttackC = 0f;
    private float heavyAttackC = 0f;

    private float lightAttackD = 0f;
    private float heavyAttackD = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(lightAttackState + " " + heavyAttackState);
        lightAttack();
        heavyAttack();
    }

    private void lightAttack()
    {
        switch (lightAttackState)
        {
            case AttackState.READY:

                if (Input.GetMouseButtonDown(0))
                {
                    lightAttackState = AttackState.ATTACKING;
                    lightAttackD = lightAttackDuration;
                }

                break;
            case AttackState.ON_COOLDOWN:
                lightAttackC -= Time.deltaTime;
                if (lightAttackC <= 0)
                {
                    lightAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                lightAttackD -= Time.deltaTime;
                if (lightAttackD <= 0)
                {
                    lightAttackState = AttackState.ON_COOLDOWN;
                    lightAttackC = lightAttackCoolDown;
                }
                break;
        }
    }

    private void heavyAttack()
    {
        switch (heavyAttackState)
        {
            case AttackState.READY:

                if (Input.GetMouseButtonDown(1))
                {
                    heavyAttackState = AttackState.ATTACKING;
                    heavyAttackD = heavyAttackDuration;
                }

                break;
            case AttackState.ON_COOLDOWN:
                heavyAttackC -= Time.deltaTime;
                if (heavyAttackC <= 0)
                {
                    heavyAttackState = AttackState.READY;
                }

                break;
            case AttackState.ATTACKING:
                heavyAttackD -= Time.deltaTime;
                if (heavyAttackD <= 0)
                {
                    heavyAttackState = AttackState.ON_COOLDOWN;
                    heavyAttackC = lightAttackCoolDown;
                }
                break;
        }
    }

    public bool isLightAttacking()
    {
        return lightAttackState == AttackState.ATTACKING;
    }

    public bool isHeavyAttacking()
    {
        return heavyAttackState == AttackState.ATTACKING;
    }

    public bool isAttacking()
    {
        return isLightAttacking() || isHeavyAttacking();
    }

    public void resetStats()
    {
        currentDmg = baseDmg;
        currentAttackSpeed = baseAttackSpeed;
    }

    public enum AttackState
    {
        READY,
        ATTACKING,
        ON_COOLDOWN
    }
}
