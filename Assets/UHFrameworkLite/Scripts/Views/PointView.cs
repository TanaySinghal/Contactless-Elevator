using UnityEngine;

namespace UHFrameworkLite
{

    public class PointView : MonoBehaviour
    {
        [HideInInspector] public TactilePoint tactilePoint;
        Renderer renderer;

        void Start()
        {
            renderer = this.GetComponent<Renderer>();
            if (tactilePoint != null)
            {
                transform.position = tactilePoint.position.ToUnity();

                // Display frequency as color
                renderer.material.SetColor("_Color", TactileRunner.Instance.GetColorFromFrequency(tactilePoint.drawFrequency));
            }
        }

        void Update()
        {
            if (tactilePoint != null)
            {
                transform.position = tactilePoint.position.ToUnity();

                // Display frequency as color
                renderer.material.SetColor("_Color", TactileRunner.Instance.GetColorFromFrequency(tactilePoint.drawFrequency));
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

}
