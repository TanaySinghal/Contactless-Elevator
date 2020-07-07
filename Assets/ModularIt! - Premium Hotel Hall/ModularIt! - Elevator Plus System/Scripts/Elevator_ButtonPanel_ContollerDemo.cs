using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_ButtonPanel_ContollerDemo : MonoBehaviour {

    public bool Demo;
    float Timer;
    int _num = 0;

    void Start()
    {
        Timer = Time.time;
    }

    void FixedUpdate()
    {
        if (Demo)
        {
            if (Timer + 1 < Time.time)
            {
                Timer = Time.time;
                _num++;
                if (GetComponent<Elevator_ButtonPanel_Contoller>().NumElementOff1.Length == 0)
                {
                    if (_num > 9)
                        _num = 0;
                }
                else
                {
                    if (_num > 99)
                        _num = 0;
                }
                GetComponent<Elevator_ButtonPanel_Contoller>().DrawNum(_num);
                GetComponent<Elevator_ButtonPanel_Contoller>().DrawArrow(randomBoolean(), randomBoolean());
                if (randomBoolean())
                    GetComponent<Elevator_ButtonPanel_Contoller>().DisableButtons();
                GetComponent<Elevator_ButtonPanel_Contoller>().EnableButton((int)Mathf.Round(Random.Range(0, GetComponent<Elevator_ButtonPanel_Contoller>().Buttons.Length)));

            }
        } else
        {
            Destroy(this);
        }
    }

    bool randomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }
}
