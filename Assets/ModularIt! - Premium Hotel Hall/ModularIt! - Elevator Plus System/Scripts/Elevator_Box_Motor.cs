using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Box_Motor : MonoBehaviour {

    public int TargetStage;
    Elevator_UnitContoller UnitContoller;
    bool SFXPlay = false;
    public bool SimulateOrReal; // true simulate and teleport; false motor to destination
    public bool InstantlyTeleport;
    Vector3 pos;
    float movingSpeed;

    private void Start()
    {
        UnitContoller = transform.parent.GetComponent<Elevator_UnitContoller>();
        pos = transform.position;
        movingSpeed = Elevator_GlobalSystem.ElevatorsSpeedStatic;
    }

    private void FixedUpdate()
    {
        if(!UnitContoller)
            UnitContoller = transform.parent.GetComponent<Elevator_UnitContoller>();

        if (!UnitContoller.DoorIsClosed || GetComponent<Elevator_DoorOpen_Motor>())
            return;

        if (!SFXPlay && !InstantlyTeleport)
        {
            UnitContoller.PlaySFX(Elevator_GlobalSystem.MotorSFXstatic);
            SFXPlay = true;
        }
        
        if (Mathf.Abs(UnitContoller.HallDoorController[TargetStage - 1].transform.position.y - pos.y) <= 0.3f)
            MotorComplete();


        for (int i = 0; i < UnitContoller.HallDoorController.Length; i++)
        {
            if (Mathf.Abs(UnitContoller.HallDoorController[i].transform.position.y - pos.y) < 0.1f)
            {
                UnitContoller.ChangeCurrentStage(i + 1);
            }
        }

        float step = movingSpeed * Time.deltaTime;
        Vector3 tmpP = Vector3.MoveTowards(pos, UnitContoller.HallDoorController[TargetStage-1].transform.position, step);
        pos.y = tmpP.y;

        if (!SimulateOrReal && !InstantlyTeleport)
            transform.position = pos;

        if (InstantlyTeleport)
            MotorComplete();
    }

    private void MotorComplete()
    {
        Vector3 _finishPos = transform.position;
        _finishPos.y = UnitContoller.HallDoorController[TargetStage - 1].transform.position.y;
        transform.position = _finishPos;
        UnitContoller.StopSFX();
        UnitContoller.ChangeCurrentStage(TargetStage);
        Destroy(GetComponent<Elevator_Box_Motor>());
    }
}
