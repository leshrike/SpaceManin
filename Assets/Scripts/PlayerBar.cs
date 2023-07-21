using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BarType{

    hpBar,
    manaBar
}

public class PlayerBar : MonoBehaviour
{

    private Slider slider;
    // Start is called before the first frame update
    public BarType type;
    void Start()
    {
        slider = GetComponent<Slider>();
        switch(type){

            case BarType.hpBar:
                slider.maxValue = PlayerController.maxHp;
                break;
            
            case BarType.manaBar:
                slider.maxValue = PlayerController.maxMp;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case BarType.hpBar:
            slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHealth();
            break;
            
            case BarType.manaBar:
            slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetMana();
            break;
        }
    }
}
