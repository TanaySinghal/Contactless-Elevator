using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_GlobalSystem : MonoBehaviour {

    [Header("Link to Elevator_Unit")]
    public Elevator_UnitContoller[] ElevatorUnits;
    public static Elevator_UnitContoller[] Elevators;
    [Header("Start Parameters")]
    public int[] StartStage;
    public float ElevatorsSpeed;
    public static float ElevatorsSpeedStatic;

    [Header("SFX")]
    public AudioClip BingSFX;
    public float BingVolume = 1f;
    public AudioClip DoorSFX;
    public float DoorVolume = 0.3f;
    public AudioClip MotorSFX;
    public float MotorVolume = 0.25f;

    public static AudioClip BingSFXstatic;
    public static AudioClip DoorSFXstatic;
    public static AudioClip MotorSFXstatic;
    public static float BingVolumeStatic;
    public static float DoorVolumeStatic;
    public static float MotorVolumeStatic;

    private void Awake()
    {
        ElevatorsSpeedStatic = ElevatorsSpeed;
        Elevators = ElevatorUnits;
        BingSFXstatic = BingSFX;
        DoorSFXstatic = DoorSFX;
        MotorSFXstatic = MotorSFX;
        BingVolumeStatic = BingVolume;
        DoorVolumeStatic = DoorVolume;
        MotorVolumeStatic = MotorVolume;
    }

    private void Start()
    {
        // for(int i = 0; i < Elevators.Length; i++)
        // {
        //     Elevators[i].ChangeTargetStage(StartStage[i], true);
        // }
    }

    public class Doors
    {
        public static void CloseAllDoorOfUnit(int _unitID)
        {
            Elevators[_unitID].CloseAllDoorOfUnit();
        }

        public static void CloseElevatorDoor(int _unitID)
        {
            Elevators[_unitID].CloseElevatorDoor();
        }

        public static void CloseHallDoor(int _unitID, int _hallID)
        {
            Elevators[_unitID].CloseHallDoor(_hallID);
        }

        public static void OpenAllDoorOfUnit(int _unitID)
        {
            Elevators[_unitID].OpenAllDoorOfUnit();
        }

        public static void OpenElevatorDoor(int _unitID)
        {
            Elevators[_unitID].OpenElevatorDoor();
        }

        public static void OpenHallDoor(int _unitID, int _hallID)
        {
            Elevators[_unitID].OpenHallDoor(_hallID);
        }
    }

    public class ButtonPanel
    {
        public static void Undraw(int _unitID)
        {
            Elevators[_unitID].BPUndraw();
        }

        public static void DisableButtons(int _unitID)
        {
            Elevators[_unitID].BPDisableButtons();
        }

        public static void EnableButton(int _unitID, int _buttonID)
        {
            Elevators[_unitID].BPEnableButton(_buttonID);
        }

        public static void DisableButton(int _unitID, int _buttonID)
        {
            Elevators[_unitID].BPDisableButton(_buttonID);
        }

        public static void DrawArrow(int _unitID, bool _up, bool _down)
        {
            Elevators[_unitID].BPDrawArrow(_up, _down);
        }

        public static void DrawNum(int _unitID, int _num)
        {
            Elevators[_unitID].BPDrawNum(_num);
        }

        public static void Demonstration(int _unitID, bool _on)
        {
            Elevators[_unitID].BPDemonstration(_on);
        }

        public static bool DemoActive(int _unitID)
        {
            return Elevators[_unitID].BPDemoActive();
        }
    }

    public class Elevator
    {
        public static void CallToStagePhysically(int _unitID, int _stageID)
        {
            Elevators[_unitID].ChangeTargetStage(_stageID, false);
        }

        public static void CallToStageSimulate(int _unitID, int _stageID)
        {
            Elevators[_unitID].ChangeTargetStage(_stageID, true);
        }

        public static void CallToStageNow(int _unitID, int _stageID)
        {
            Elevators[_unitID].ChangeTargetStage(_stageID, true, true);
        }

        public static bool ElevatorBusy(int _unitID)
        {
            return Elevators[_unitID].ElevatorBusy();
        }
    }

    public class CallButton
    {
        public static void EnableButton(int _unitID, int _buttonID)
        {
            Elevators[_unitID].CBEnableButton(_buttonID);
        }

        public static void DisableButton(int _unitID, int _buttonID)
        {
            Elevators[_unitID].CBDisableButton(_buttonID);
        }

        public static void DisableAllButton(int _unitID)
        {
            for (int i = 0; i < Elevators[_unitID].HallDoorController.Length; i++)
            {
                Elevators[_unitID].CBDisableButton(i);
            }
        }

        public static void EnableAllButton(int _unitID)
        {
            for (int i = 0; i < Elevators[_unitID].HallDoorController.Length; i++)
            {
                Elevators[_unitID].CBEnableButton(i);
            }
        }
    }

    public class SFX
    {
        public static void PlayBing(int _unitID)
        {
            Elevators[_unitID].PlaySFX(BingSFXstatic);
        }

        public static void PlayDoor(int _unitID)
        {
            Elevators[_unitID].PlaySFX(DoorSFXstatic);
        }

        public static void PlayMotor(int _unitID)
        {
            Elevators[_unitID].PlaySFX(MotorSFXstatic);
        }

        public static void StopSFX(int _unitID)
        {
            Elevators[_unitID].StopSFX();
        }
    }

    public class Player
    {
        public static Transform SetPlayerAsChildOfElevator(int _unitID)
        {
            return Elevators[_unitID].GetComponent<Elevator_UnitContoller>().ElevatorDoorController.transform;
        }
    }
}
