using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For now, this is just a pure data class
public class ElevatorButton : MonoBehaviour
{
    public int floorNumber;

    public static ElevatorButton HoveredButton;

    public void SetFloor(int number) {
        this.floorNumber = number;
    }
    
    // public void SetActualPosition() {
    // }

    void OnMouseOver() {
        HoveredButton = this;
    }

    void OnMouseExit() {
        HoveredButton = null;
    }
}
