using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaSlider;

    public void SetSlider(float amount)
    {
        manaSlider.value = amount;
    }

    public void SetSliderMax(float amount)
    {
        manaSlider.maxValue = amount;
        SetSlider(amount);
    }
}
