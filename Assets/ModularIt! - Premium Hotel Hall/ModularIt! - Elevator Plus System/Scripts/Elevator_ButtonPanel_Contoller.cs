using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_ButtonPanel_Contoller : MonoBehaviour {

    [Header("Working with concrete GO")]
    public Transform[] NumElementOff;
    public Transform[] NumElementOn;
    public Transform[] NumElementOff1;
    public Transform[] NumElementOn1;
    public Transform[] ArrayElementOff;
    public Transform[] ArrayElementOn;

    [Header("Working with component in ring parent GO")]
    public Transform[] Buttons;

    public void Undraw()
    {
        for (int i = 0; i < NumElementOn.Length; i++)
        {
            NumElementOn[i].GetComponent<MeshRenderer>().enabled = false;
            NumElementOff[i].GetComponent<MeshRenderer>().enabled = true;
        }
        for (int i = 0; i < ArrayElementOn.Length; i++)
        {
            ArrayElementOn[i].GetComponent<MeshRenderer>().enabled = false;
            ArrayElementOff[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void DisableButtons()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<Elevator_ButtonPanel_ButtonContoller>().DeactivateButton();
        }
    }

    public void EnableButton(int _num)
    {
        if (_num > Buttons.Length-1)
            return;
        Buttons[_num].GetComponent<Elevator_ButtonPanel_ButtonContoller>().ActivateButton();
    }

    public void DisableButton(int _num)
    {
        if (_num > Buttons.Length-1)
            return;
        Buttons[_num].GetComponent<Elevator_ButtonPanel_ButtonContoller>().DeactivateButton();
    }

    void Draw(bool[] _rule, bool _FirstNum)
    {
        for (int i = 0; i < _rule.Length; i++)
        {
            if (_FirstNum)
            {
                if (_rule[i])
                {
                    NumElementOn[i].GetComponent<MeshRenderer>().enabled = true;
                    NumElementOff[i].GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    NumElementOn[i].GetComponent<MeshRenderer>().enabled = false;
                    NumElementOff[i].GetComponent<MeshRenderer>().enabled = true;
                }
            } else
            {
                if (_rule[i])
                {
                    NumElementOn1[i].GetComponent<MeshRenderer>().enabled = true;
                    NumElementOff1[i].GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    NumElementOn1[i].GetComponent<MeshRenderer>().enabled = false;
                    NumElementOff1[i].GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }

    public void DrawArrow(bool _up, bool _down)
    {
        if (_up)
        {
            ArrayElementOn[0].GetComponent<MeshRenderer>().enabled = true;
            ArrayElementOff[0].GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            ArrayElementOn[0].GetComponent<MeshRenderer>().enabled = false;
            ArrayElementOff[0].GetComponent<MeshRenderer>().enabled = true;
        }

        if (_down)
        {
            ArrayElementOn[1].GetComponent<MeshRenderer>().enabled = true;
            ArrayElementOff[1].GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            ArrayElementOn[1].GetComponent<MeshRenderer>().enabled = false;
            ArrayElementOff[1].GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void DrawNum(int _num)
    {
        string _variable = _num.ToString("00");

        bool[] _rule0 = new bool[8];
        bool[] _rule1 = new bool[8];

        if (NumElementOff1.Length == 0)
        {
            _rule1 = SwitchRule((int)int.Parse("" + _variable[1]));
            Draw(_rule1, true);
        } else
        {
            _rule1 = SwitchRule((int)int.Parse("" + _variable[1]));
            Draw(_rule1, true);
            _rule0 = SwitchRule((int)int.Parse("" + _variable[0]));
            Draw(_rule0, false);
        }
    }

    bool[] SwitchRule(int _num)
    {
        bool[] _rule = new bool[8];

        switch (_num)
        {
            case 0:
                _rule[0] = true;
                _rule[1] = true;
                _rule[2] = true;
                _rule[3] = false;
                _rule[4] = true;
                _rule[5] = true;
                _rule[6] = true;
                _rule[7] = false;
                break;
            case 1:
                _rule[0] = false;
                _rule[1] = false;
                _rule[2] = true;
                _rule[3] = false;
                _rule[4] = true;
                _rule[5] = false;
                _rule[6] = false;
                _rule[7] = true;
                break;
            case 2:
                _rule[0] = false;
                _rule[1] = true;
                _rule[2] = true;
                _rule[3] = true;
                _rule[4] = false;
                _rule[5] = true;
                _rule[6] = true;
                _rule[7] = false;
                break;
            case 3:
                _rule[0] = false;
                _rule[1] = true;
                _rule[2] = true;
                _rule[3] = true;
                _rule[4] = true;
                _rule[5] = true;
                _rule[6] = false;
                _rule[7] = false;
                break;
            case 4:
                _rule[0] = true;
                _rule[1] = false;
                _rule[2] = true;
                _rule[3] = true;
                _rule[4] = true;
                _rule[5] = false;
                _rule[6] = false;
                _rule[7] = false;
                break;
            case 5:
                _rule[0] = true;
                _rule[1] = true;
                _rule[2] = false;
                _rule[3] = true;
                _rule[4] = true;
                _rule[5] = true;
                _rule[6] = false;
                _rule[7] = false;
                break;
            case 6:
                _rule[0] = true;
                _rule[1] = true;
                _rule[2] = false;
                _rule[3] = true;
                _rule[4] = true;
                _rule[5] = true;
                _rule[6] = true;
                _rule[7] = false;
                break;
            case 7:
                _rule[0] = false;
                _rule[1] = true;
                _rule[2] = true;
                _rule[3] = false;
                _rule[4] = true;
                _rule[5] = false;
                _rule[6] = false;
                _rule[7] = false;
                break;
            case 8:
                _rule[0] = true;
                _rule[1] = true;
                _rule[2] = true;
                _rule[3] = true;
                _rule[4] = true;
                _rule[5] = true;
                _rule[6] = true;
                _rule[7] = false;
                break;
            case 9:
                _rule[0] = true;
                _rule[1] = true;
                _rule[2] = true;
                _rule[3] = true;
                _rule[4] = true;
                _rule[5] = true;
                _rule[6] = false;
                _rule[7] = false;
                break;
            default:
                print("Can not configure Num");
                break;
        }

        return _rule;
    }

    public void Demonstration(bool _on)
    {
        if (_on && !DemoActive())
        {
            gameObject.AddComponent<Elevator_ButtonPanel_ContollerDemo>();
            GetComponent<Elevator_ButtonPanel_ContollerDemo>().Demo = true;
            return;
        }

        if (!_on && DemoActive())
        {
            GetComponent<Elevator_ButtonPanel_ContollerDemo>().Demo = false;
            return;
        }
    }

    public bool DemoActive()
    {
        if (GetComponent<Elevator_ButtonPanel_ContollerDemo>())
            return true;
        else
            return false;
    }
}
