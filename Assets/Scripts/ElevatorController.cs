using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UHFrameworkLite;

public class ElevatorController : MonoBehaviourSingleton<ElevatorController>
{
    [SerializeField] Elevator_ButtonPanel_Contoller elevatorPanel;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip doorsOpening;
    [SerializeField] AudioClip doorsClosing;

    
    [Header("You are on floor")]
    [SerializeField] AudioClip onFloor1;
    
    [SerializeField] AudioClip onFloor2;
    
    [SerializeField] AudioClip onFloor3;
    
    [SerializeField] AudioClip onFloor4;
    
    [SerializeField] AudioClip onFloor5;
    
    
    [Header("You are on floor")]
    [SerializeField] AudioClip selectingFloor1;
    
    [SerializeField] AudioClip selectingFloor2;
    
    [SerializeField] AudioClip selectingFloor3;
    
    [SerializeField] AudioClip selectingFloor4;
    
    [SerializeField] AudioClip selectingFloor5;
    

    int currFloor;
    public bool doorIsOpen;
    bool inTransit;

    void Start() {
        currFloor = 1;
        OpenDoor();
        inTransit = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            OpenDoor();
        }
        if (Input.GetKeyDown(KeyCode.Y )) {
            CloseDoor();
        }
    }

    public void GoToFloor(int targetFloor) {
        if (!inTransit) {
            Debug.Log("Going to floor " + targetFloor);
            StartCoroutine(TravelToFloor(targetFloor));
        }
        else {
            Debug.LogWarning("Cannot move! Elevator is in transit.");
        }
    }

    IEnumerator TravelToFloor(int targetFloor) {
        CloseDoor();
        inTransit = true;
        yield return new WaitForSeconds(1f);
        
        Elevator_GlobalSystem.SFX.PlayMotor(0);
        while (currFloor != targetFloor) {
            yield return new WaitForSeconds(1f);
            if (currFloor < targetFloor) {
                currFloor ++;
                elevatorPanel.DrawNum(currFloor);
                elevatorPanel.DrawArrow(true, false);
            }
            else {
                currFloor --;
                elevatorPanel.DrawNum(currFloor);
                elevatorPanel.DrawArrow(false, true);
            }
        }

        switch (targetFloor) {
            case 1:
                audioSource.PlayOneShot(onFloor1);
                break;
            case 2:
                audioSource.PlayOneShot(onFloor2);
                break;
            case 3:
                audioSource.PlayOneShot(onFloor3);
                break;
            case 4:
                audioSource.PlayOneShot(onFloor4);
                break;
            case 5:
                audioSource.PlayOneShot(onFloor5);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(2f);
        Elevator_GlobalSystem.SFX.StopSFX(0);
        Elevator_GlobalSystem.SFX.PlayBing(0);

        inTransit = false;
        OpenDoor();
    }

    public void OpenDoor() {
        if (!doorIsOpen && !inTransit) {
            Elevator_GlobalSystem.Doors.OpenAllDoorOfUnit(0);
            elevatorPanel.DrawArrow(false, false);
            doorIsOpen = true;
            audioSource.PlayOneShot(doorsOpening);
        }
    }
    
    public void CloseDoor() {
        if (doorIsOpen && !inTransit) {
            Elevator_GlobalSystem.Doors.CloseAllDoorOfUnit(0);
            elevatorPanel.DrawArrow(false, false);
            doorIsOpen = false;
            audioSource.PlayOneShot(doorsClosing);
        }
    }

    public void PlayAudioSelectingFloor(int num) {

        if (audioSource.isPlaying) {
            audioSource.Stop();
        }

        switch (num) {
            case 1:
                audioSource.PlayOneShot(selectingFloor1);
                break;
            case 2:
                audioSource.PlayOneShot(selectingFloor2);
                break;
            case 3:
                audioSource.PlayOneShot(selectingFloor3);
                break;
            case 4:
                audioSource.PlayOneShot(selectingFloor4);
                break;
            case 5:
                audioSource.PlayOneShot(selectingFloor5);
                break;
            default:
                break;
        }
    }
}
