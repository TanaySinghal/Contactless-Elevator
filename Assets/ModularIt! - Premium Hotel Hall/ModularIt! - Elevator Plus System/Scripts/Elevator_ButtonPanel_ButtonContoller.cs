using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_ButtonPanel_ButtonContoller : MonoBehaviour {

    public Transform ElementOff;
    public Transform ElementOn;

    public void ActivateButton()
    {
        ElementOn.GetComponent<MeshRenderer>().enabled = true;
        ElementOff.GetComponent<MeshRenderer>().enabled = false;
    }

    public void DeactivateButton()
    {
        ElementOn.GetComponent<MeshRenderer>().enabled = false;
        ElementOff.GetComponent<MeshRenderer>().enabled = true;
    }
}
