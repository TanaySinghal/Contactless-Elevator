using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UHFrameworkLite.Demo
{
    public class CircleDemo : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)] float intensity = 1f;

        [SerializeField, Range(0f, 500f)] float drawFrequency = 100f;
        [SerializeField, Range(0f, 0.1f)] float radius = 0.02f;

        TactileCircle tactileCircle;
        Vector3 startPosition;

        void Start()
        {
            // Record start position
            startPosition = transform.position;

            // Create tactile position
            tactileCircle = new TactileCircle(
                transform.position.ToUH(),
                intensity,
                drawFrequency,
                radius
            );
            TactileRunner.Instance.AddShape(tactileCircle);
        }

        // Update is called once per frame
        void Update()
        {
            // Move circle
            transform.position = startPosition + Vector3.right * Mathf.Sin(Time.time) * 0.05f;

            // Update tactile position
            tactileCircle.position = transform.position.ToUH();
            tactileCircle.intensity = intensity;
            tactileCircle.drawFrequency = drawFrequency;
            tactileCircle.radius = radius;
        }
    }
}