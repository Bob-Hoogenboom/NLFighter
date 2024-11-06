using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fillColor;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fillColor.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        fillColor.color = gradient.Evaluate(slider.normalizedValue);
        Debug.Log(slider.normalizedValue);
    }

}
