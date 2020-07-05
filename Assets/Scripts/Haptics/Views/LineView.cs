using UnityEngine;

public class LineView : MonoBehaviour
{
    [HideInInspector] public TactileLine tactileLine;
    readonly float THICKNESS = 0.002f; // in meters

    Renderer renderer;

    void Start() {
        renderer = this.GetComponent<Renderer>();
    }

    void Update()
    {
        if (tactileLine != null)
        {
            Vector3 globalPosition = tactileLine.position.ToUnity();
            Vector3 startPoint = tactileLine.startPoint.ToUnity();
            Vector3 endPoint = tactileLine.endPoint.ToUnity();
            UpdateView(globalPosition, startPoint, endPoint, tactileLine.intensity * THICKNESS);
            
            // Display frequency as color
            renderer.material.SetColor("_EmissionColor", TactileRunner.Instance.GetColorFromFrequency(tactileLine.drawFrequency));
        }
    }

    public void UpdateView(Vector3 globalPosition, Vector3 startPoint, Vector3 endPoint, float thickness)
    {
        transform.localPosition = globalPosition + (startPoint + endPoint) / 2f;
        float length = (startPoint - endPoint).magnitude;
        transform.localScale = new Vector3(length, thickness, thickness);
        transform.rotation = Quaternion.FromToRotation(Vector3.right, endPoint - startPoint);
    }
}
