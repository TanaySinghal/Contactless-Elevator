using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UHFrameworkLite
{
    public class CylinderView : MonoBehaviour
    {
        [HideInInspector] public TactileCircle tactileCircle;
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

        void UpdateView()
        {
            if (tactileCircle != null)
            {
                transform.localPosition = tactileCircle.position.ToUnity();
                transform.localScale = new Vector3(tactileCircle.radius * 2f, transform.localScale.y, tactileCircle.radius * 2f);
                transform.localRotation = Quaternion.identity;
            }
        }
    }
}