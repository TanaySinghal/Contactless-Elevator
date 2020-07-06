using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity.Interaction;
using UHFrameworkLite;

// For now, this is just a pure data class
public class ElevatorButton : MonoBehaviour
{
    public int floorNumber;
    [SerializeField] InteractionButton interactionButton;

    [SerializeField, Range(0f, 1f)] float tactileIntensity = 1f;
    [SerializeField, Range(0f, 500f)] float tactileFrequency = 100f;
    [SerializeField, Range(0f, 0.1f)] float tactileRadius = 0.02f;

    public static ElevatorButton HoveredButton;
    public static ElevatorButton PressedButton;

    TactileCircle circleSensation;

    void Start() {
        circleSensation = new TactileCircle(
            Vector3.zero.ToUH(),
            tactileIntensity,
            tactileFrequency,
            tactileRadius
        );
    }

    void Update()
    {
        if (interactionButton.isPrimaryHovered && HoveredButton != this)
        {
            HoveredButton = this;
        }
        else if (!interactionButton.isPrimaryHovered && HoveredButton == this)
        {
            HoveredButton = null;
        }

        if (interactionButton.isPressed && PressedButton != this) {
            circleSensation.position = transform.position.ToUH();
            TactileRunner.Instance.AddShape(circleSensation);
            PressedButton = this;
        }
        else if (!interactionButton.isPressed && PressedButton == this) {
            TactileRunner.Instance.RemoveShape(circleSensation);
            PressedButton = null;
        }
    }

    public void SetFloor(int number)
    {
        this.floorNumber = number;
    }
    // void OnMouseOver() {
    //     HoveredButton = this;
    // }

    // void OnMouseExit() {
    //     HoveredButton = null;
    // }

}
