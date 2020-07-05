using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ultrahaptics;

namespace UHFrameworkLite.Demo
{
    // Copied directly from https://developer.ultrahaptics.com/knowledgebase/amplitude-modulation-api-tutorial/
    // Made minor changes so that intensity & frequency are UI sliders
    public class AMFollow : MonoBehaviour
    {
        AmplitudeModulationEmitter _emitter;
        [SerializeField, Range(0f, 1f)]
        float intensity = 1f; // 0 to 1
        [SerializeField, Range(1f, 500f)]
        float frequency = 140f;

        void Start()
        {
            // Initialize the emitter
            _emitter = new AmplitudeModulationEmitter();
            _emitter.initialize();
        }

        // Update on every frame
        void Update()
        {
            // Set the position to this tranform's position
            Ultrahaptics.Vector3 position = transform.position.ToUH();
            // Create a control point object using this position
            AmplitudeModulationControlPoint point = new AmplitudeModulationControlPoint(position, intensity, frequency);
            // Output this point; technically we don't need to do this every update since nothing is changing.
            _emitter.update(new List<AmplitudeModulationControlPoint> { point });
        }

        // Ensure the emitter is stopped on exit
        void OnDisable()
        {
            _emitter.stop();
        }

        // Ensure the emitter is immediately disposed when destroyed
        void OnDestroy()
        {
            if (_emitter != null)
            {
                _emitter.Dispose();
                _emitter = null;
            }
        }
    }
}
