﻿using UnityEngine;
using System.Collections.Generic;
using Ultrahaptics;
using TMPro;

namespace UHFrameworkLite
{
    /// <summary>
    /// Declares this script as a singleton that can be referenced statically with T.Instance
    /// </summary>
    /// <typeparam name="T"></typeparam> hello
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Object
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                }
                return _instance;
            }
        }
    }

    // See the following resource for a basic example of using TPS
    // https://github.com/ultraleap/ultraleap-labs/blob/130e56147c70eb1b41da58089e7b3a13390fdb6b/HapticTextures/Assets/HapticTextures/Scripts/HapticRunner

    /// <summary>
    /// A singleton that manages and renders all tactile shapes to the UltraHaptics device
    /// </summary>
    public class TactileRunner : MonoBehaviourSingleton<TactileRunner>
    {
        TimePointStreamingEmitter _emitter;
        bool _firstTime = true;
        double _startTime;

        // Dictionary from tactile shape data to its view
        Dictionary<TactileShape, GameObject> tactileShapes;
        readonly uint maxShapes = 4;

        // Status
        [SerializeField] TextMeshProUGUI deviceDetectedText;
        [SerializeField] TextMeshProUGUI deviceEmittingText;

        [SerializeField] bool displayTactileShapes = false;
        [SerializeField] Color lowFrequencyColor;
        [SerializeField] int lowFrequencyValue = 50;
        [SerializeField] Color highFrequencyColor;
        [SerializeField] int highFrequencyValue = 200;

        // View prefabs
        [SerializeField] CircleView circleViewPrefab;
        [SerializeField] LineView lineViewPrefab;
        [SerializeField] PointView pointViewPrefab;
        [SerializeField] CylinderView cylinderViewPrefab;

        bool _deviceDetected;
        bool DeviceDetected {
            get { return _deviceDetected; }
            set { 
                _deviceDetected = value;
                UpdateDeviceStatus();
            }
        }

        bool _deviceEmitting;
        bool DeviceEmitting {
            get { return _deviceEmitting; }
            set {
                _deviceEmitting = value;
                UpdateDeviceStatus();
            }
        }
        
        void Awake()
        {
            // Initialize shape list
            tactileShapes = new Dictionary<TactileShape, GameObject>();


            // Initialize the emitter
            _emitter = new TimePointStreamingEmitter();
            _emitter.initialize();

            uint sampleRate = _emitter.setMaximumControlPointCount(maxShapes);
            Debug.Log("Tactile sample rate: " + sampleRate);

            // Set callback and start emitter (no need to pass object)
            _emitter.setEmissionCallback(MyEmitterCallback, null);

            StartEmitting();
        }

        void StartEmitting() {
            bool started = _emitter.start();
            DeviceEmitting = started;
        }

        // Whenever an editor variable changes
        void OnValidate()
        {
            foreach (UnityEngine.Transform child in transform)
            {
                child.gameObject.SetActive(displayTactileShapes);
            }
        }

        // Ensure the emitter is stopped on exit
        void OnDisable()
        {
            _emitter.stop();
            DeviceEmitting = false;
        }

        void Update() {
            bool isConnected = _emitter.isConnected();
            if (DeviceDetected != isConnected) {
                DeviceDetected = isConnected;
                if (isConnected) {
                    StartEmitting();
                }
                else {
                    DeviceEmitting = false;
                }
            }
        }

        // Ensure the emitter is immediately disposed when destroyed
        void OnDestroy()
        {
            _emitter.Dispose();
            DeviceEmitting = false;
            _emitter = null;
        }


        void UpdateDeviceStatus() {
            deviceDetectedText.text = "UltraHaptics Detected: " + DeviceDetected;
            deviceDetectedText.color = DeviceDetected ? Color.white : Color.red;

            deviceEmittingText.text = "UltraHaptics Emitting: " + DeviceEmitting;
            deviceEmittingText.color = DeviceEmitting ? Color.white : Color.red;
        }

        public void AddShape(TactileShape shape)
        {
            if (!_emitter.isConnected())
            {
                Debug.LogWarning("Failed to add shape. Tactile emitter is disconnected.");
                DeviceEmitting = false;
                return;
            }

            if (tactileShapes.Count < maxShapes)
            {
                // Instantiate view and add it to shape
                GameObject view = InstantiateViewFromShape(shape);
                view.transform.SetParent(transform);
                view.gameObject.SetActive(displayTactileShapes);
                tactileShapes.Add(shape, view);
            }
            else
            {
                Debug.LogWarning("Failed to add shape. Maximum shapes allowed: " + maxShapes);
            }
        }

        public bool ContainsShape(TactileShape shape)
        {
            return tactileShapes.ContainsKey(shape);
        }

        public void RemoveShape(TactileShape shape)
        {
            if (ContainsShape(shape)) {
                // Destroy the shape's view
                GameObject view = tactileShapes[shape];
                Destroy(view);

                // Remove the shape
                tactileShapes.Remove(shape);
            }
        }

        public Color GetColorFromFrequency(float frequency)
        {
            float t = Mathf.InverseLerp(lowFrequencyValue, highFrequencyValue, frequency);
            return Color.Lerp(lowFrequencyColor, highFrequencyColor, t);
        }

        // Note: errors here may go silent. For some reason, we cannot reference transform here at all.
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

                // Note: technically there is no guarantee that the tactile shapes will be in the right order
                // but this should work for now...
                int i = 0;
                foreach (TactileShape shape in tactileShapes.Keys)
                {
                    // Set the position and intensity of the persistent control 
                    // point to that of the modulated wave at this point in time.
                    Ultrahaptics.Vector3 point = shape.EvaluateAt(t);
                    sample.persistentControlPoint(i).setPosition(point);
                    sample.persistentControlPoint(i).setIntensity(shape.intensity);
                    ++i;
                }
            }
        }

        GameObject InstantiateViewFromShape(TactileShape shape)
        {
            switch (shape)
            {
                case TactileCircle circle:
                    CylinderView circleView = Instantiate(cylinderViewPrefab);
                    circleView.tactileCircle = (TactileCircle)shape;
                    return circleView.gameObject;
                case TactileLine line:
                    LineView lineView = Instantiate(lineViewPrefab);
                    lineView.tactileLine = (TactileLine)shape;
                    return lineView.gameObject;
                case TactilePoint point:
                    PointView pointView = Instantiate(pointViewPrefab);
                    pointView.tactilePoint = (TactilePoint)shape;
                    return pointView.gameObject;
                default:
                    Debug.LogError("Shape of type " + shape.GetType() + " is not associated with a view");
                    return null;
            }
        }
    }
}
