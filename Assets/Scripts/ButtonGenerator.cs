using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGenerator : MonoBehaviour
{

    [SerializeField] ElevatorButton button;
    int num = 5;

    List<ElevatorButton> buttons;

    float ordinarySize = 1f;
    // float inflatedSize = 2f;

    float animationSpeed = 0.2f; // 0 to 1f. Kind of a hack / change later.

    void Start()
    {
        // Instantiate and initialize buttons
        buttons = new List<ElevatorButton>();
        for(int i = 0; i < num; i ++) {
            ElevatorButton b = Instantiate(button, Vector3.zero, Quaternion.identity);
            b.SetFloor(i);
            b.transform.SetParent(transform);
            buttons.Add(b);
        }
    }

    void Update()
    {
        // Probably use the is dirty trick later...
        foreach (var b in buttons) {
            int dy = 0;
            if (ElevatorButton.HoveredButton != null) {
                dy = b.floorNumber - ElevatorButton.HoveredButton.floorNumber;
            }

            // Set scale
            float s = ordinarySize;
            if (ElevatorButton.HoveredButton != null) {
                s += HeightFn(dy);
            }
            b.transform.localScale = Vector3.Lerp(b.transform.localScale, Vector3.one * s, animationSpeed);

            // Set position
            Vector3 pos = new Vector3(0, b.floorNumber * 1.2f, 0);
            if (ElevatorButton.HoveredButton != null) {
                pos += new Vector3(0, DistFn(dy), 0);
            }
            b.transform.position = Vector3.Lerp(b.transform.position, pos, animationSpeed);
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
