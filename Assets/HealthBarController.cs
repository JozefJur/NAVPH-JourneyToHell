using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider HealthSlider;
    public Text health;

    private void Update()
    {

        if(health != null)
        {
            health.text = HealthSlider.value + " / " + HealthSlider.maxValue;
        }
    }


    public void setMaxHealth(float health)
    {
        HealthSlider.maxValue = health;
    }

    public void setCurrentHealth(float health)
    {
        HealthSlider.value = health;
    }
}
