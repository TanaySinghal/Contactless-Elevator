using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_SetAsChildOfElevator : MonoBehaviour {

    public int ElevatorID; // set up in Editor if use more than 1 elevator

    private void OnTriggerEnter(Collider other) // Set Any GO as Elevator as child, for detected attach use names or tags
    {
        //if(othet.gameObject.tag == "Player")
        other.transform.parent = Elevator_GlobalSystem.Player.SetPlayerAsChildOfElevator(ElevatorID);
    }

    private void OnTriggerExit(Collider other)
    {
        //if(othet.gameObject.tag == "Player")
        other.transform.parent = null;
    }
}
