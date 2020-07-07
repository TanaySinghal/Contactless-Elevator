using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_UnitContoller : MonoBehaviour {

    public Elevator_DoorOpen_Controller ElevatorDoorController;
    public Elevator_DoorOpen_Controller[] HallDoorController;
    public Elevator_ButtonPanel_Contoller[] ButtonPanels;
    [Header("StageInfo - 1 level is zero")]
    public int NowOnStage;
    public int TargetStage;
    public int StartStage;

    public bool DoorIsClosed;
    public void ChangeCurrentStage(int _num)
    {
        NowOnStage = _num;

        //Button Panel
        BPDemonstration(false);
        BPDisableButtons();
        BPEnableButton(TargetStage-1);
        BPDrawNum(_num);
        if (NowOnStage < TargetStage)
            BPDrawArrow(true, false);
        if (NowOnStage > TargetStage)
            BPDrawArrow(false, true);
        //Button Panel END

        if (TargetStage == NowOnStage)
        {
            ElevatorArrived();
        }
    }

    public bool ElevatorInMotion()
    {
        if (ElevatorDoorController.gameObject.GetComponent<Elevator_Box_Motor>())
            return true;
        return false;
    }

    public bool ElevatorBusy()
    {
        if (ElevatorDoorController.gameObject.GetComponent<Elevator_Box_Motor>() || ElevatorDoorController.gameObject.GetComponent<Elevator_DoorOpen_Motor>())
            return true;
        return false;
    }

    public void ChangeTargetStage(
        int _num,
        bool _motionType, // true - simulate and teleport; false - physicaly motor to destination 
        bool _instantly = false // Action Right Now!
        )
    {
        if (ElevatorInMotion())
            return;

        TargetStage = _num;
        if(TargetStage == NowOnStage)
        {
            ElevatorArrived();
        } else
        {
            if (ElevatorInMotion())
                return;
            CloseAllDoorOfUnit();
            Elevator_Box_Motor _motor = ElevatorDoorController.gameObject.AddComponent<Elevator_Box_Motor>();
            _motor.TargetStage = TargetStage;
            _motor.SimulateOrReal = _motionType;
            _motor.InstantlyTeleport = _instantly;
        }

        //Call Button
        for (int i = 0; i < HallDoorController.Length; i++) // Deactivate ALL
        {
            CBDisableButton(i);
        }
        CBEnableButton(_num - 1); // Activate New
        //Call Button END

        //Button Panel
        BPDemonstration(false);
        BPDisableButtons();
        BPEnableButton(TargetStage-1);
        if (NowOnStage < TargetStage)
            BPDrawArrow(true, false);
        if (NowOnStage > TargetStage)
            BPDrawArrow(false, true);
        //Button Panel END
    }

    public void ElevatorArrived()
    {
        PlaySFX(Elevator_GlobalSystem.BingSFXstatic);
        OpenElevatorDoor(true);
        OpenHallDoor(NowOnStage-1, true);
        BPDrawArrow(false, false);
        BPDisableButtons();
        CBDisableButton(NowOnStage-1);
    }

    public void CloseAllDoorOfUnit()
    {
        CloseElevatorDoor();
        for(int _id = 0; _id < HallDoorController.Length; _id++)
        {
            CloseHallDoor(_id);
        }
    }

    public void CloseElevatorDoor()
    {
        PlaySFX(Elevator_GlobalSystem.DoorSFXstatic);
        ElevatorDoorController.DoorAction(false);
    }

    public void CloseHallDoor(int _id)
    {
        if(HallDoorController[_id] == null || !HallDoorController[_id])
        {
            Debug.Log("Empty or Unassigned Stage");
            return;
        }
        PlaySFX(Elevator_GlobalSystem.DoorSFXstatic);
        HallDoorController[_id].DoorAction(false);
    }

    public void OpenAllDoorOfUnit(bool _force = false)
    {
        if(!_force)
            if (ElevatorInMotion())
                return;

        OpenElevatorDoor(_force);
        for (int _id = 0; _id < HallDoorController.Length; _id++)
        {
            OpenHallDoor(_id, _force);
        }
    }

    public void OpenElevatorDoor(bool _force = false)
    {
        if (!_force)
            if (ElevatorInMotion())
                return;

        PlaySFX(Elevator_GlobalSystem.DoorSFXstatic);
        ElevatorDoorController.DoorAction(true);
    }

    public void OpenHallDoor(int _id, bool _force = false)
    {
        if (HallDoorController[_id] == null || !HallDoorController[_id])
        {
            Debug.Log("Empty or Unassigned Stage");
            return;
        }
        if (!_force)
            if (ElevatorInMotion())
                return;

        PlaySFX(Elevator_GlobalSystem.DoorSFXstatic);
        HallDoorController[_id].DoorAction(true);
    }

    public void BPUndraw()
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].Undraw();
        }
    }

    public void BPDisableButtons()
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].DisableButtons();
        }
    }

    public void BPEnableButton(int _buttonID)
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].EnableButton(_buttonID);
        }
    }

    public void BPDisableButton(int _buttonID)
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].DisableButton(_buttonID);
        }
    }

    public void BPDrawArrow(bool _up, bool _down)
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].DrawArrow(_up, _down);
        }
    }

    public void BPDrawNum(int _num)
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].DrawNum(_num);
        }
    }

    public void BPDemonstration(bool _on)
    {
        for (int i = 0; i < ButtonPanels.Length; i++)
        {
            ButtonPanels[i].Demonstration(_on);
        }
    }

    public bool BPDemoActive()
    {
        return ButtonPanels[0].DemoActive();
    }

    public void CBEnableButton(int _buttonID)
    {
        if (HallDoorController[_buttonID] == null || !HallDoorController[_buttonID])
        {
            Debug.Log("Empty or Unassigned Stage");
            return;
        }
        HallDoorController[_buttonID].gameObject.GetComponent<Elevator_CallButton>().ActivateButton();
    }

    public void CBDisableButton(int _buttonID)
    {
        if (HallDoorController[_buttonID] == null || !HallDoorController[_buttonID])
        {
            Debug.Log("Empty or Unassigned Stage");
            return;
        }
        HallDoorController[_buttonID].gameObject.GetComponent<Elevator_CallButton>().DeactivateButton();
    }

    public void PlaySFX(AudioClip _clip)
    {
        if (_clip == Elevator_GlobalSystem.BingSFXstatic)
        {
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().loop = false;
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().PlayOneShot(_clip);
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().volume = Elevator_GlobalSystem.BingVolumeStatic;
        }
        else if (_clip == Elevator_GlobalSystem.DoorSFXstatic)
        {
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().loop = false;
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().PlayOneShot(_clip);
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().volume = Elevator_GlobalSystem.DoorVolumeStatic;
        }
        else if (_clip == Elevator_GlobalSystem.MotorSFXstatic)
        {
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().loop = true;
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().Play();
            ElevatorDoorController.gameObject.GetComponent<AudioSource>().volume = Elevator_GlobalSystem.MotorVolumeStatic;

        }
    }

    public void StopSFX()
    {
        ElevatorDoorController.gameObject.GetComponent<AudioSource>().Stop();
    }

}
