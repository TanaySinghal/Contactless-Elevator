using System;
using Ultrahaptics;

public abstract class TactileShape
{
    // The position of the control point
    public Vector3 position;

    // The intensity of the control point
    public float intensity;

    // The frequency that control point will draw shape in Hz
    public float drawFrequency;

    protected TactileShape(Vector3 position, float intensity, float drawFrequency)
    {
        this.position = position;
        this.intensity = intensity;
        this.drawFrequency = drawFrequency;
    }

    // Given seconds, produce a point on the shape
    public abstract Vector3 EvaluateAt(double seconds);
}


/// <summary>
/// This script contains logic for drawing a tactile Circle sensation.
/// </summary>
public class TactileCircle : TactileShape
{
    // The radius of the circle (meters)
    public float radius;

    public TactileCircle(Vector3 position, float intensity, float drawFrequency, float radius) 
        : base(position, intensity, drawFrequency)
    {
        this.radius = radius;
    }

    public override Vector3 EvaluateAt(double seconds)
    {
        var theta = 2 * Math.PI * drawFrequency * seconds;
        Vector3 circle = new Vector3(
            // Calculate the x and y positions of the circle and set the height
            (float)(Math.Cos(theta) * radius),
            (float)(Math.Sin(theta) * radius),
            0
        );
        return position + circle;
    }
};

/// <summary>
/// This script contains logic for drawing a tactile Line sensation.
/// </summary>
public class TactileLine : TactileShape
{
    // In local coordinates (meters)
    // E.g. a 2 cm line could be (-0.01,0,0) to (0.01,0,0)
    public Vector3 startPoint;
    public Vector3 endPoint;

    public TactileLine(Vector3 position, float intensity, float drawFrequency, Vector3 startPoint, Vector3 endPoint)
        : base(position, intensity, drawFrequency)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }

    public override Vector3 EvaluateAt(double seconds)
    {
        // t is within 0 to 1 (wraps around) and represents how far along we are from
        // startPosition (0) to endPosition (1)
        double t = drawFrequency * seconds;
        t = t - Math.Floor(t);

        float tf = (float) t;
        Vector3 diff = endPoint - startPoint;
        Vector3 point = startPoint + new Vector3(diff.x * tf, diff.y * tf, diff.z * tf);
        return position + point;
    }
};
