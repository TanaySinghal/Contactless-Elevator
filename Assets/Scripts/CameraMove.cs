using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform locationA;
    
    public Transform locationB;

    Vector3 targetPosition;
    Quaternion targetRotation;

    void Start() {
        transform.position = locationB.position;
        transform.rotation = locationB.rotation;

        targetPosition = locationB.position;
        targetRotation = locationB.rotation;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            targetPosition = locationA.position;
            targetRotation = locationA.rotation;
        }
        
        if (Input.GetKeyDown(KeyCode.B)) {
            targetPosition = locationB.position;
            targetRotation = locationB.rotation;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
    }
#endif
}
