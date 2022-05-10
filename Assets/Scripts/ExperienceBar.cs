using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public Text text;

    //private void Awake()
    //{
    //    //slider.onValueChanged.AddListener(delegate { SliderValueChanged(); });
    //}


    public void SetMaxExperience(float maxExp)
    {
        slider.maxValue = maxExp;
    }

    public void SetExperience(float exp)
    {
        slider.value = exp;
    }

    public void SetLevel(int lvl) {
        text.text = "LV " + ("" + lvl).PadLeft(3, ' ');
    }

    //void SliderValueChanged()
    //{
    //}

    
}
