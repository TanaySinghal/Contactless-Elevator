using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_ButtonReaction_UnityFPSControllerIntegration : MonoBehaviour
{
    Vector3 zeroOfScreen;

    private void Start()
    {
        Screen.lockCursor = true;
        zeroOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0); // Calcute crosshair position, need recalculate if change screen resolution runtime
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.JoystickButton0))
        { // for use LMB or   if(Input.GetKey(KeyCode.JoystickButton0)) { // for use Xbox Controller A Button on Windows(more about Xbox Controller: http://wiki.unity3d.com/index.php?title=Xbox360Controller#Buttons)
            Ray ray = Camera.main.ScreenPointToRay(zeroOfScreen);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance > 1) // Exit function if distance more 1m
                    return;
                if (hit.collider.gameObject.name == "E0_S1")
                {
                    if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
                        Debug.LogWarning("Elevator Busy");
                    else
                        Elevator_GlobalSystem.Elevator.CallToStageSimulate(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)
                } else if(hit.collider.gameObject.name == "E0_S49")
                {
                    if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0))
                        Debug.LogWarning("Elevator Busy");
                    else
                        Elevator_GlobalSystem.Elevator.CallToStageSimulate(0, 49);
                }
            }
        }
    }
}
