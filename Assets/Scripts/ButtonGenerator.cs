using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGenerator : MonoBehaviour
{

    [SerializeField] ElevatorButton button;
    [SerializeField] int numFloors = 5;

    List<ElevatorButton> buttons;

    float ordinarySize = 0.05f;
    // float inflatedSize = 2f;

    float animationSpeed = 0.2f; // 0 to 1f. Kind of a hack / change later.

    // higher number: less height spread / lower number: more height spread
    // e.g. 0.8 means we use cos^2(0.8 * x) for the height distribution
    const float cos2frequency = 0.8f;
    const float buttonOffset = 0.2f;

    void Start()
    {
        // Instantiate and initialize buttons
        buttons = new List<ElevatorButton>();
        for(int i = 0; i < numFloors; i ++) {
            ElevatorButton b = Instantiate(button);
            b.SetFloor(i + 1);
            b.transform.SetParent(transform);
            buttons.Add(b);
        }
    }

    void Update()
    {
        // Probably use the is dirty trick later...
        foreach (var b in buttons) {
            float dy = 0;
            if (ElevatorButton.HoveredButton != null) {
                dy = b.floorNumber - ElevatorButton.HoveredButton.floorNumber;
                dy *= cos2frequency;
            }

            // Set scale
            float s = 1;
            if (ElevatorButton.HoveredButton != null) {
                s += HeightFn(dy);
            }
            b.transform.localScale = Vector3.Lerp(b.transform.localScale, Vector3.one * s * ordinarySize, animationSpeed);

            // Set position
            Vector3 pos = Vector3.up * b.floorNumber * (1 + buttonOffset);
            if (ElevatorButton.HoveredButton != null) {
                pos += Vector3.up * DistFn(dy);
            }
            b.transform.localPosition = Vector3.Lerp(b.transform.localPosition, pos * ordinarySize, animationSpeed);
        }
    }

    float HeightFn(float x) {
        // This is cos^2(x) but clamped
        float clampedX = Mathf.Clamp(x, - Mathf.PI / 2, Mathf.PI / 2);
        float c = Mathf.Cos(clampedX);
        return c * c;
    }

    float DistFn(float x) {
        // Integral of HeightFn
        float clampedX = Mathf.Clamp(x, - Mathf.PI / 2, Mathf.PI / 2);
        float val = clampedX + 0.5f * Mathf.Sin(2f * clampedX);
        return 0.5f * val;
    }
}
