using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UHFrameworkLite;

public class BrailleCharacter : MonoBehaviourSingleton<BrailleCharacter>
{
    // Must be in braille order
    [SerializeField] GameObject[] brailleDots;
    [SerializeField] bool debugMode = false;

    float showTime = 0.2f;
    float pauseTime = 0.2f;

    List<int> NUMBER_1 = new List<int>{2, 3, 4, 5, 6};
    List<int> NUMBER_2 = new List<int>{2, 3, 4, 5, 6, 7};
    List<int> NUMBER_3 = new List<int>{2, 3, 4, 5, 6, 9};
    List<int> NUMBER_4 = new List<int>{2, 3, 4, 5, 6, 9, 10};
    List<int> NUMBER_5 = new List<int>{2, 3, 4, 5, 6, 10};

    IEnumerator braillePlayer;

    int num_to_play = 1;

    List<int> IdxToFrequency = new List<int>{
        200, 140, 120, 160, 180, 100,
        200, 140, 120, 160, 180, 100
    };

    TactilePoint tactilePoint;

    // Start is called before the first frame update
    void Start()
    {
        braillePlayer = null;
    }

    void Update() {
        if (debugMode && Input.GetKeyDown(KeyCode.Space)) {
            PlayBraille(num_to_play);
            num_to_play ++;
            if (num_to_play > 5) {
                num_to_play = 1;
            }
        }
    }

    // Only supports 1 to 5
    public void PlayBraille(int number) {
        // Stop existing braille if any
        StopBraille();

        // Get appropriate number
        List<int> brailleSequence;
        switch (number) {
            case 1:
                brailleSequence = NUMBER_1;
                break; 
            case 2:
                brailleSequence = NUMBER_2;
                break;
            case 3:
                brailleSequence = NUMBER_3;
                break;
            case 4:
                brailleSequence = NUMBER_4;
                break;
            case 5:
                brailleSequence = NUMBER_5;
                break;
            default:
                throw new System.Exception("Error: " + number + " not supported in braille");
        }

        braillePlayer = PlayBrailleSequence(brailleSequence);
        StartCoroutine(braillePlayer);
    }

    public void StopBraille() {
        // Stop existing braille if any
        if (braillePlayer != null) {
            StopCoroutine(braillePlayer);
        }

        RemoveTactilePoint();
    }

    IEnumerator PlayBrailleSequence(List<int> indices) {
        
        RemoveTactilePoint();

        foreach(var i in indices) {
            GameObject b = brailleDots[i];
            // Get position of braille dot relative to tactile runner (our device) and play there 
            Vector3 p = TactileRunner.Instance.transform.InverseTransformPoint(b.transform.position);
            tactilePoint = new TactilePoint(p.ToUH(), 1f, IdxToFrequency[i]);
            TactileRunner.Instance.AddShape(tactilePoint);

            yield return new WaitForSeconds(showTime);
            RemoveTactilePoint();
            yield return new WaitForSeconds(pauseTime);
        }
    }

    void RemoveTactilePoint() {
        if (tactilePoint != null) {
            TactileRunner.Instance.RemoveShape(tactilePoint);
            tactilePoint = null;
        }
    }
}
