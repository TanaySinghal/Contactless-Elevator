using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ultrahaptics;

// Translated from C++
// https://developer.ultrahaptics.com/knowledgebase/time-point-streaming-api-tutorial/

// Also help support gave me this resource:
// https://github.com/ultraleap/ultraleap-labs/blob/130e56147c70eb1b41da58089e7b3a13390fdb6b/HapticTextures/Assets/HapticTextures/Scripts/HapticRunner
// The above resource shows you how to handle multiple circles AND how to track hand position

namespace UHFrameworkLite.Demo
{
    public class TPSFollow : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float intensity = 1f; // 0 to 1
        [Range(1f, 500f)]
        public float frequency = 100f;
        [Range(0.001f, 0.05f)]
        public float radius = 0.01f;

        TimePointStreamingEmitter _emitter;
        bool _firstTime = true;
        double _startTime;

        TactileCircle circle;

        void Start()
        {
            // Initialize circle
            circle = new TactileCircle(
                new Ultrahaptics.Vector3(0.0f, 0.0f, 0.2f),
                intensity,
                frequency,
                radius
            );

            // Initialize the emitter
            _emitter = new TimePointStreamingEmitter();
            _emitter.initialize();

            // Set callback and start emitter
            _emitter.setEmissionCallback(MyEmitterCallback, circle);
            _emitter.start();
        }

        // Update on every frame
        void Update()
        {
            // Set circle to location of object
            circle.position = transform.position.ToUH();

            // Apply any changes to sliders in update
            circle.intensity = intensity;
            circle.drawFrequency = frequency;
            circle.radius = radius;

            // Update callback with updated circle
            _emitter.setEmissionCallback(MyEmitterCallback, circle);
        }

        void OnEnable() {
            if (_emitter != null) {
                _emitter.start();
            }
        }

        // Ensure the emitter is stopped on exit
        void OnDisable()
        {
            _emitter.stop();
        }

        // Ensure the emitter is immediately disposed when destroyed
        void OnDestroy()
        {
            _emitter.Dispose();
            _emitter = null;
        }

        void MyEmitterCallback(TimePointStreamingEmitter emitter, OutputInterval interval, TimePoint deadline, object userObj)
        {
            // Loop through the samples in this interval
            foreach (var sample in interval)
            {
                if (_firstTime)
                {
                    _startTime = sample.seconds();
                    _firstTime = false;
                }
                double t = sample.seconds() - _startTime;
                Ultrahaptics.Vector3 position = circle.EvaluateAt(t);

                // Set the position and intensity of the persistent control 
                // point to that of the modulated wave at this point in time.
                // Note: we use 0 because there is only one circle
                sample.persistentControlPoint(0).setPosition(position);
                sample.persistentControlPoint(0).setIntensity(circle.intensity);
            }
        }
    }
}
