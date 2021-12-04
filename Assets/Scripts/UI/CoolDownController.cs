using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownController : MonoBehaviour
{

    public Image CoolDown;
    private bool isOnCooldown = false;
    private float cooldownTime;
    private float currentCoolDownTime;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void setCooldown(float cooldownTime)
    {
        isOnCooldown = true;
        this.cooldownTime = cooldownTime;
        this.currentCoolDownTime = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnCooldown)
        {
            currentCoolDownTime -= Time.deltaTime;
            if(currentCoolDownTime <= 0)
            {
                isOnCooldown = false;
                CoolDown.fillAmount = 0f;
            }
            else
            {
                CoolDown.fillAmount = currentCoolDownTime / cooldownTime;

            }
        }
    }
}
