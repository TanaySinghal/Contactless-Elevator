using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_DoorOpen_Controller : MonoBehaviour {

    public Vector3 LOpen;
    public Vector3 LClose;
    public Vector3 ROpen;
    public Vector3 RClose;
    public Transform L;
    public Transform R;

    public bool NowWantOpen;
    public bool ChechStateOnStart;
    public bool SendStateToUnitContoller;

    private void Start()
    {
        if (ChechStateOnStart)
            DoorAction(NowWantOpen);

        ChechStateOnStart = false;
    }

    public void DoorAction(bool _open)
    {
        if (_open == NowWantOpen && !ChechStateOnStart)
            return;
        
        NowWantOpen = _open;

        if (!GetComponent<Elevator_DoorOpen_Motor>())
            gameObject.AddComponent<Elevator_DoorOpen_Motor>();

        if(NowWantOpen)
            GetComponent<Elevator_DoorOpen_Motor>().ApplyParam(LOpen, ROpen, L , R, this);
        else
            GetComponent<Elevator_DoorOpen_Motor>().ApplyParam(LClose, RClose, L, R, this);
    }

    public void MotorDone()
    {
        if (!SendStateToUnitContoller)
            return;

        if (!NowWantOpen)
            transform.parent.GetComponent<Elevator_UnitContoller>().DoorIsClosed = true;
        else
            transform.parent.GetComponent<Elevator_UnitContoller>().DoorIsClosed = false;
    }
}
