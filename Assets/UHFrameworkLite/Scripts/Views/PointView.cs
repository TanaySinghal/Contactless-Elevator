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
            UpdateView();
        }

        void Update()
        {
            UpdateView();
        }

        void UpdateView() {
            if (tactilePoint != null)
            {
                transform.localPosition = tactilePoint.position.ToUnity();
                // Display frequency as color
                renderer.material.SetColor("_Color", TactileRunner.Instance.GetColorFromFrequency(tactilePoint.drawFrequency));
            }
        }
    }

}
