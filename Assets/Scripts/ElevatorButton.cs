using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Leap.Unity.Interaction;
using UHFrameworkLite;

// For now, this is just a pure data class
public class ElevatorButton : MonoBehaviour
{
    public int floorNumber;

    // 1 to 9 in order
    [SerializeField] List<GameObject> numberPrefabs;

    [SerializeField] InteractionButton interactionButton;

    [SerializeField, Range(0f, 1f)] float tactileIntensity = 1f;
    [SerializeField, Range(0f, 500f)] float tactileFrequency = 100f;
    [SerializeField, Range(0f, 0.1f)] float tactileRadius = 0.02f;

    public static ElevatorButton HoveredButton;
    public static ElevatorButton PressedButton;

    TactileCircle circleSensation;

    float brailleDistance = 0.07f; // distance from button in meters

    void Start() {
        circleSensation = new TactileCircle(
            Vector3.zero.ToUH(),
            tactileIntensity,
            tactileFrequency,
            tactileRadius
        );

        if (floorNumber <= numberPrefabs.Count) {
            GameObject numberGO = Instantiate(numberPrefabs[floorNumber - 1]);
            numberGO.transform.SetParent(interactionButton.transform);
            numberGO.transform.localPosition = Vector3.zero;
            numberGO.transform.localRotation = Quaternion.Euler(0, 180, 0);
            numberGO.transform.localScale = Vector3.one;
        }
    }

    void Update()
    {
        if (interactionButton.isPrimaryHovered && HoveredButton != this)
        {
            HoveredButton = this;

            // Start playing braille
            BrailleCharacter.Instance.transform.position = transform.position - transform.forward * brailleDistance;
            BrailleCharacter.Instance.PlayBraille(floorNumber);
        }
        else if (!interactionButton.isPrimaryHovered && HoveredButton == this)
        {
            HoveredButton = null;
        }

        if (interactionButton.isPressed && PressedButton != this) {
            // Stop playing braille
            BrailleCharacter.Instance.StopBraille();

            // Play button haptics
            circleSensation.position = TactileRunner.Instance.transform.InverseTransformPoint(transform.position).ToUH();
            TactileRunner.Instance.AddShape(circleSensation);

            // GO TO THE FLOOR
            Debug.Log("Going to floor " + floorNumber);
            ElevatorController.Instance.GoToFloor(floorNumber);
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
