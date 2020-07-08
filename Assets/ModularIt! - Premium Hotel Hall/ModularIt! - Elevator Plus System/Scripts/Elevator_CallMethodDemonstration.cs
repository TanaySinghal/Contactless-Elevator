using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_CallMethodDemonstration : MonoBehaviour {

    private void Start()
    {
        Invoke("InvokeLaunchDemo", 8); // Activate Button Panel Demo after 8 sec
    }

    void InvokeLaunchDemo()
    {
        Elevator_GlobalSystem.ButtonPanel.Demonstration(0, true);
    }

    private void Update()
    {
        ////
        //// DOOR ACTION
        ////  |
        ////  V

        if(Input.GetKeyDown(KeyCode.Alpha0))
            Elevator_GlobalSystem.Doors.CloseAllDoorOfUnit(0); //Close All Door of Elevator[0] - CloseAllDoorOfUnit(int ElevatorID)
        
        
        if(Input.GetKeyDown(KeyCode.Alpha9))
            Elevator_GlobalSystem.Doors.OpenAllDoorOfUnit(0); //Close All Door of Elevator[0] - CloseAllDoorOfUnit(int ElevatorID)

        // Elevator_GlobalSystem.Doors.CloseAllDoorOfUnit(0); //Close All Door of Elevator[0] - CloseAllDoorOfUnit(int ElevatorID)
        // Elevator_GlobalSystem.Doors.CloseElevatorDoor(0); //Close inside Elevator Door of Elevator[0] - CloseElevatorDoor(int ElevatorID)
        // Elevator_GlobalSystem.Doors.CloseHallDoor(0, 0); //Close Hall[0] Door of Elevator[0] - CloseHallDoor(int ElevatorID, int HallID)


        // Elevator_GlobalSystem.Doors.OpenAllDoorOfUnit(0, true); //Open All Door of Elevator[0] - OpenAllDoorOfUnit(int ElevatorID, bool _force(ighoneElevatorCurrentState))
        // Elevator_GlobalSystem.Doors.OpenElevatorDoor(0, true); //Open inside Elevator Door of Elevator[0] - OpenAllDoorOfUnit(int ElevatorID, bool _force(ighoneElevatorCurrentState))
        // Elevator_GlobalSystem.Doors.OpenHallDoor(0, 0, true); //Open Hall[0] Door of Elevator[0] - CloseHallDoor(int ElevatorID, int HallID, bool _force(ighoneElevatorCurrentState))

        ////
        //// BUTTON PANEL INSIDE ELEVATOR ACTION
        ////  |
        ////  V

        // if (Input.GetKeyDown(KeyCode.Alpha9))
        //     Elevator_GlobalSystem.ButtonPanel.Demonstration(0, !Elevator_GlobalSystem.ButtonPanel.DemoActive(0)); // Demo active of Button Panel in Elevator[0] = !Demo active

        // Elevator_GlobalSystem.ButtonPanel.Demonstration(0, !Elevator_GlobalSystem.ButtonPanel.DemoActive(0)); // Demo active of Button Panel in Elevator[0] = !Demo active
        // Elevator_GlobalSystem.ButtonPanel.Undraw(0); // Undraw all in Button Panel in Elevator[0] - Undraw(int ElevatorID)
        // Elevator_GlobalSystem.ButtonPanel.DisableButtons(0); // Disable all buttons in Button Panel in Elevator[0] - DisableButtons(int ElevatorID)
        // Elevator_GlobalSystem.ButtonPanel.EnableButton(0, 1); // Enable button[1] in Button Panel in Elevator[0] - EnableButton(int ElevatorID, int ButtonID)
        // Elevator_GlobalSystem.ButtonPanel.DisableButton(0, 1); // Disable button[1] in Button Panel in Elevator[0] - DisableButton(int ElevatorID, int ButtonID)
        // Elevator_GlobalSystem.ButtonPanel.DrawArrow(0, true, false); // Draw Arrow up in Button Panel in Elevator[0] - DrawArrow(int ElevatorID, bool _UpArrowGlowng, bool _DownArrowGlowng)
        // Elevator_GlobalSystem.ButtonPanel.DrawNum(0, 4); // Draw "4" in Button Panel in Elevator[0] - DrawNum(int ElevatorID, int NumberToDraw)

        ////
        //// MOVING ELEVATOR
        ////  |
        ////  V

        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        //         Debug.LogWarning("Elevator Busy");
        //     else
        //         Elevator_GlobalSystem.Elevator.CallToStageSimulate(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)

        // }
        // if (Input.GetKeyDown(KeyCode.Alpha2))
        // {
        //     if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        //         Debug.LogWarning("Elevator Busy");
        //     else
        //         Elevator_GlobalSystem.Elevator.CallToStageSimulate(0, 2); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        //         Debug.LogWarning("Elevator Busy");
        //     else
        //         Elevator_GlobalSystem.Elevator.CallToStagePhysically(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)

        // }
        // if (Input.GetKeyDown(KeyCode.Alpha4))
        // {
        //     if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        //         Debug.LogWarning("Elevator Busy");
        //     else
        //         Elevator_GlobalSystem.Elevator.CallToStagePhysically(0, 2); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha5))
        // {
        //     if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        //         Debug.LogWarning("Elevator Busy");
        //     else
        //         Elevator_GlobalSystem.Elevator.CallToStageNow(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)

        // }
        // if (Input.GetKeyDown(KeyCode.Alpha6))
        // {
        //     if (Elevator_GlobalSystem.Elevator.ElevatorBusy(0)) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        //         Debug.LogWarning("Elevator Busy");
        //     else
        //         Elevator_GlobalSystem.Elevator.CallToStageNow(0, 2); // Call Elevator[0] to Stage[0+1] - CallToStage(int ElevatorID, int StageID+1)
        // }

        // Elevator_GlobalSystem.Elevator.ElevatorBusy(0) // Check if the elevator can perform a new task - ElevatorBusy(int ElevatorID) : return true/false
        // Elevator_GlobalSystem.Elevator.CallToStageSimulate(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStageSimulate(int ElevatorID, int StageID+1) Moving is simulated and elevator teleporn when simulation end
        // Elevator_GlobalSystem.Elevator.CallToStagePhysically(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStagePhysically(int ElevatorID, int StageID+1) Moving is physically for glass mine or anything you want
        // Elevator_GlobalSystem.Elevator.CallToStageNow(0, 1); // Call Elevator[0] to Stage[0+1] - CallToStageNow(int ElevatorID, int StageID+1) Moving to target stage as fast as posible(close door on current door and lets teleport)

        ////
        //// Player
        ////  |
        ////  V

        // transform.parent = Elevator_GlobalSystem.Player.SetPlayerAsChildOfElevator(0); // Set this GO as child of Elevator[0] (need for moving object(player for example) with elevator) - SetPlayerAsChildOfElevator(int ElevatorID)
        //  ^ for multi objects you can use trigger, to detect objects in the elevator 

        ////
        //// CALL BUTTON
        ////  |
        ////  V

        // Elevator_GlobalSystem.CallButton.EnableButton(0, 0); // Enable Call Button Elevator[0] on Stage 1 - EnableButton(int ElevatorID, int StageID)
        // Elevator_GlobalSystem.CallButton.DisableButton(0, 0); // Disable Call Button Elevator[0] on Stage 1 - DisableButton(int ElevatorID, int StageID)
        // Elevator_GlobalSystem.CallButton.DisableAllButton(0); // Disable ALL Call Button Elevator[0] - DisableAllButton(int ElevatorID)
        // Elevator_GlobalSystem.CallButton.EnableAllButton(0); // Enable ALL Call Button Elevator[0] - EnableAllButton(int ElevatorID)

        ////
        //// SFX
        ////  |
        ////  V

        // Elevator_GlobalSystem.SFX.PlayBing(0); // Play Bing Sound in Elevator[0] - PlayBing(int ElevatorID)
        // Elevator_GlobalSystem.SFX.PlayDoor(0); // Play Door Sound in Elevator[0] - PlayDoor(int ElevatorID)
        // Elevator_GlobalSystem.SFX.PlayMotor(0); // Play Motor Sound in Elevator[0] - PlayMotor(int ElevatorID)
        // Elevator_GlobalSystem.SFX.StopSFX(0); // Stop Sound in Elevator[0] - StopSFX(int ElevatorID)

    }
}
