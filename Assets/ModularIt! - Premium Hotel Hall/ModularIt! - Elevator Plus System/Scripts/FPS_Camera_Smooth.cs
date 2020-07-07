using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Camera_Smooth : MonoBehaviour {

    public Transform ControlGO;

    private void Update()
    {
        transform.rotation = ControlGO.rotation;
        transform.position = Vector3.Lerp(transform.position, ControlGO.position, Time.deltaTime);
    }
}
