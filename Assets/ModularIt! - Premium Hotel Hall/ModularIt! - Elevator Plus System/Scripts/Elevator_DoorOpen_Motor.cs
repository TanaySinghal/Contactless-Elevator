using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_DoorOpen_Motor : MonoBehaviour {

    Vector3 LTarget;
    Vector3 RTarget;
    Transform L;
    Transform R;
    Elevator_DoorOpen_Controller DOC;

    public void ApplyParam(Vector3 _lt, Vector3 _rt, Transform _l, Transform _r, Elevator_DoorOpen_Controller _doc)
    {
        LTarget = _lt;
        RTarget = _rt;
        L = _l;
        R = _r;
        DOC = _doc;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(LTarget, L.localPosition) < 0.0001f && Vector3.Distance(RTarget, R.localPosition) < 0.0001f)
        {
            DOC.MotorDone();
            Destroy(this);
        }

        L.localPosition = Vector3.Lerp(L.localPosition, LTarget, Time.deltaTime * 3);
        R.localPosition = Vector3.Lerp(R.localPosition, RTarget, Time.deltaTime * 3);
    }
}
