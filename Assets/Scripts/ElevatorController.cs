using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UHFrameworkLite;

public class ElevatorController : MonoBehaviourSingleton<ElevatorController>
{
    [SerializeField] Elevator_ButtonPanel_Contoller elevatorPanel;

    int currFloor;
    public bool doorIsOpen;
    bool inTransit;

    public System.Action OnDoorOpen;
    
    public System.Action OnDoorClose;

    void Start() {
        currFloor = 1;
        OpenDoor();
        inTransit = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            OpenDoor();
        }
        if (Input.GetKeyDown(KeyCode.Y )) {
            CloseDoor();
        }
    }

    public void GoToFloor(int targetFloor) {
        if (!inTransit) {
            Debug.Log("Going to floor " + targetFloor);
            StartCoroutine(TravelToFloor(targetFloor));
        }
        else {
            Debug.LogWarning("Cannot move! Elevator is in transit.");
        }
    }

    IEnumerator TravelToFloor(int targetFloor) {
        CloseDoor();
        inTransit = true;
        yield return new WaitForSeconds(1f);
        
        Elevator_GlobalSystem.SFX.PlayMotor(0);
        while (currFloor != targetFloor) {
            yield return new WaitForSeconds(1f);
            if (currFloor < targetFloor) {
                currFloor ++;
                elevatorPanel.DrawNum(currFloor);
                elevatorPanel.DrawArrow(true, false);
            }
            else {
                currFloor --;
                elevatorPanel.DrawNum(currFloor);
                elevatorPanel.DrawArrow(false, true);
            }
        }

        yield return new WaitForSeconds(2f);
        Elevator_GlobalSystem.SFX.StopSFX(0);
        Elevator_GlobalSystem.SFX.PlayBing(0);

        inTransit = false;
        OpenDoor();
    }

    public void OpenDoor() {
        if (!doorIsOpen && !inTransit) {
            Elevator_GlobalSystem.Doors.OpenAllDoorOfUnit(0);
            elevatorPanel.DrawArrow(false, false);
            doorIsOpen = true;
            if (OnDoorOpen != null) {
                OnDoorOpen();
            }
        }
    }
    
    public void CloseDoor() {
        if (doorIsOpen && !inTransit) {
            Elevator_GlobalSystem.Doors.CloseAllDoorOfUnit(0);
            elevatorPanel.DrawArrow(false, false);
            doorIsOpen = false;
            if (OnDoorClose != null) {
                OnDoorClose();
            }
        }
    }
}
