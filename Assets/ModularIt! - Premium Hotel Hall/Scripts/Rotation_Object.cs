using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Object : MonoBehaviour {

    public float speed;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
    }
}
