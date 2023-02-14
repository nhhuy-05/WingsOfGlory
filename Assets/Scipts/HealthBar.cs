using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    /// <summary>
    /// Changes the health value of the bar
    /// </summary>
    /// <param name="health">current health value</param>
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    /// <summary>
    /// Gives the health bar a maximum value
    /// </summary>
    /// <param name="health">maximum health value</param>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
