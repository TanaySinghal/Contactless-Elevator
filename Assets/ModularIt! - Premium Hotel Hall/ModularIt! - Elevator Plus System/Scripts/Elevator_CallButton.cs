using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_CallButton : MonoBehaviour {

    public Transform CallButton_ElementOn;
    public Transform CallButton_ElementOff;

    public void ActivateButton()
    {
        CallButton_ElementOn.GetComponent<MeshRenderer>().enabled = true;
        CallButton_ElementOff.GetComponent<MeshRenderer>().enabled = false;
    }

    public void DeactivateButton()
    {
        CallButton_ElementOn.GetComponent<MeshRenderer>().enabled = false;
        CallButton_ElementOff.GetComponent<MeshRenderer>().enabled = true;
    }
}
