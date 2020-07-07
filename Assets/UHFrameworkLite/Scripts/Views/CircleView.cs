using UnityEngine;

namespace UHFrameworkLite
{
    public class CircleView : MonoBehaviour
    {
        readonly int numRadSegments = 24; // Number of horizontal splits
        readonly int numSides = 18; // Number of vertical splits
        readonly float THICKNESS = 0.001f; // in meters

        [HideInInspector] public TactileCircle tactileCircle;
        float oldIntensity;
        float oldRadius;

        int updateCounter;

        Vector3[] vertices;
        Vector3[] normales;
        Vector2[] uvs;

        Renderer renderer;

        void Awake()
        {
            updateCounter = 0;

            // Allocate all mesh arrays
            if (vertices == null)
            {
                int len = (numRadSegments + 1) * (numSides + 1);
                vertices = new Vector3[len];
                normales = new Vector3[len];
                uvs = new Vector2[len];
            }
        }

        void Start()
        {
            renderer = this.GetComponent<Renderer>();
        }

        void Update()
        {
            if (tactileCircle != null)
            {
                transform.localPosition = tactileCircle.position.ToUnity();

                // Align with haptic device orientation
                transform.localRotation = Quaternion.identity;

                float intensity = tactileCircle.intensity;
                float radius = tactileCircle.radius;
                if (radius != oldRadius || intensity != oldIntensity)
                {
                    updateCounter++;
                    if (updateCounter == 1000)
                    {
                        Debug.LogWarning("You are updating CircleView's mesh way too often. Consider optimizing.");
                    }
                    UpdateMesh(radius, intensity * THICKNESS);
                }
                oldIntensity = intensity;
                oldRadius = radius;

                // Display frequency as color
                renderer.material.SetColor("_Color", TactileRunner.Instance.GetColorFromFrequency(tactileCircle.drawFrequency));
            }
        }

        // Note: this is an expensive function and should not be run too frequently
        // Mostly copied from https://wiki.unity3d.com/index.php/ProceduralPrimitives#C.23_-_Torus
        public void UpdateMesh(float majorRadius, float minorRadius)
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();
            if (filter == null)
            {
                filter = gameObject.AddComponent<MeshFilter>();
            }
            Mesh mesh = filter.mesh;
            mesh.Clear();
            #region Vertices		
            // Uses pre-allocated array "vertices"
            float _2pi = Mathf.PI * 2f;
            for (int seg = 0; seg <= numRadSegments; seg++)
            {
                int currSeg = seg == numRadSegments ? 0 : seg;

                float t1 = (float)currSeg / numRadSegments * _2pi;
                Vector3 r1 = new Vector3(Mathf.Cos(t1) * majorRadius, 0f, Mathf.Sin(t1) * majorRadius);

                for (int side = 0; side <= numSides; side++)
                {
                    int currSide = side == numSides ? 0 : side;

                    Vector3 normale = Vector3.Cross(r1, Vector3.up);
                    float t2 = (float)currSide / numSides * _2pi;
                    Vector3 r2 = Quaternion.AngleAxis(-t1 * Mathf.Rad2Deg, Vector3.up) * new Vector3(Mathf.Sin(t2) * minorRadius, Mathf.Cos(t2) * minorRadius);

                    vertices[side + seg * (numSides + 1)] = r1 + r2;
                }
            }
            #endregion

            #region Normales		
            // Uses pre-allocated array "normales"
            for (int seg = 0; seg <= numRadSegments; seg++)
            {
                int currSeg = seg == numRadSegments ? 0 : seg;

                float t1 = (float)currSeg / numRadSegments * _2pi;
                Vector3 r1 = new Vector3(Mathf.Cos(t1) * majorRadius, 0f, Mathf.Sin(t1) * majorRadius);

                for (int side = 0; side <= numSides; side++)
                {
                    normales[side + seg * (numSides + 1)] = (vertices[side + seg * (numSides + 1)] - r1).normalized;
                }
            }
            #endregion

            #region UVs
            // Uses pre-allocated array "uvs"
            for (int seg = 0; seg <= numRadSegments; seg++)
                for (int side = 0; side <= numSides; side++)
                    uvs[side + seg * (numSides + 1)] = new Vector2((float)seg / numRadSegments, (float)side / numSides);
            #endregion

            #region Triangles
            int nbFaces = vertices.Length;
            int nbTriangles = nbFaces * 2;
            int nbIndexes = nbTriangles * 3;
            int[] triangles = new int[nbIndexes];

            int i = 0;
            for (int seg = 0; seg <= numRadSegments; seg++)
            {
                for (int side = 0; side <= numSides - 1; side++)
                {
                    int current = side + seg * (numSides + 1);
                    int next = side + (seg < (numRadSegments) ? (seg + 1) * (numSides + 1) : 0);

                    if (i < triangles.Length - 6)
                    {
                        triangles[i++] = current;
                        triangles[i++] = next;
                        triangles[i++] = next + 1;

                        triangles[i++] = current;
                        triangles[i++] = next + 1;
                        triangles[i++] = current + 1;
                    }
                }
            }
            #endregion

            mesh.vertices = vertices;
            mesh.normals = normales;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            mesh.RecalculateBounds();
            mesh.Optimize();
        }
    }

}