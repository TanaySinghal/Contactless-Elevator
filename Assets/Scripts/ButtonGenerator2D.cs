using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class ButtonGenerator2d : MonoBehaviour
// {

//     [SerializeField] ElevatorButton button;
//     int numX = 5;
//     int numY = 5;

//     List<ElevatorButton> buttons;

//     float ordinarySize = 1f;
//     // float inflatedSize = 2f;

//     float animationSpeed = 0.2f; // 0 to 1f. Kind of a hack / change later.

//     void Start()
//     {
//         // Instantiate and initialize buttons
//         buttons = new List<ElevatorButton>();
//         for(int x = -numX; x < numX; x ++) {
//             for(int y = -numY; y < numY; y ++) {
//                 ElevatorButton b = Instantiate(button, Vector3.zero, Quaternion.identity);
//                 b.SetGridPosition(x, y);
//                 b.transform.SetParent(transform);
//                 buttons.Add(b);
//             }
//         }

//         // Set their positions
//         // SetActualPosition();
//     }

//     void Update()
//     {
//         // Probably use the is dirty trick later...
//         foreach (var b in buttons) {
//             int dx = 0;
//             int dy = 0;
//             if (ElevatorButton.HoveredButton != null) {
//                 dx = b.x - ElevatorButton.HoveredButton.x;
//                 dy = b.y - ElevatorButton.HoveredButton.y;
//             }

//             // Set scale
//             float s = ordinarySize;
//             if (ElevatorButton.HoveredButton != null) {
//                 s += HeightFn2D(dx, dy);
//             }
//             b.transform.localScale = Vector3.Lerp(b.transform.localScale, Vector3.one * s, animationSpeed);

//             // Set position
//             Vector3 pos = new Vector3(Linear(b.x), Linear(b.y), 0);
//             if (ElevatorButton.HoveredButton != null) {
//                 pos += new Vector3(
//                     DistFn(dx),
//                     DistFn(dy),
//                     0
//                 );
//             }
//             b.transform.position = Vector3.Lerp(b.transform.position, pos, animationSpeed);
//         }
//     }

//     void SetActualPosition() {
//         foreach (var b in buttons) {
//             Vector3 pos = new Vector3(Linear(b.x), Linear(b.y), 0);
//             b.transform.position = pos;
//         }
//     }

//     float Linear(float x) {
//         return x * 1.2f;
//     }

//     float HeightFn2D(float x, float y) {
//         var dist = Mathf.Sqrt(x*x + y*y);
//         return HeightFn(dist);
//     }

//     float HeightFn(float x) {
//         // This is cos^2(x) but clamped
//         float clampedX = Mathf.Clamp(x, - Mathf.PI / 2, Mathf.PI / 2);
//         float c = Mathf.Cos(clampedX);
//         return c * c;
//     }

//     float DistFn(float x) {
//         // Integral of HeightFn
//         float clampedX = Mathf.Clamp(x, - Mathf.PI / 2, Mathf.PI / 2);
//         float val = clampedX + 0.5f * Mathf.Sin(2f * clampedX);
//         return 0.5f * val;
//     }
// }
