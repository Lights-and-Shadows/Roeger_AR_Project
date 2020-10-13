using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // For math functions specific to doubles -- will do conversions to float for Unity-specific code

public class Chen : MonoBehaviour
{
    private LineRenderer line;
    private ScaleFactor scale; // Scale component

    // Constants
    private double a = 35;
    private double b = 8 / 3;
    private double c = 28;

    private double x0, y0, z0; // Starting positions
    private double x, y, z; // Variables to edit over time

    private double delta; // Used in position plotting
    private double dx, dy, dz, g; // Functions

    Vector3[] positionData;

    private int n; // Iterations

    public void PlotPoints()
    {
        // Assign first positions to origin
        x = x0;
        y = y0;
        z = z0;

        for (int i = 0; i < n; i++)
        {
            // Function assignments
            dx = a * (y - x);
            dy = (c - a) * x - x * z + c * y;
            dz = x * y - b * z;

            // New coordinates
            x = x + delta * dx;
            y = y + delta * dy;
            z = z + delta * dz;

            positionData[i] = new Vector3(
                (float)x * (float)scale.scaleFactor,
                (float)y * (float)scale.scaleFactor,
                (float)z * (float)scale.scaleFactor);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        n = 3000;
        x0 = -3.0;
        y0 = 2.0;
        z0 = 20.0;
        positionData = new Vector3[n];
        line = GetComponent<LineRenderer>();
        line.loop = false;
        line.useWorldSpace = false;
        line.positionCount = n;
        line.material = new Material(Shader.Find("Sprites/Default"));
        scale = gameObject.GetComponent<ScaleFactor>();
        delta = 0.002;
        PlotPoints();
        StartCoroutine(DrawEquation());
    }

    IEnumerator DrawEquation()
    {
        for (int i = 0; i < n; i++)
        {
            line.SetPosition(i, positionData[i]);

            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
    }
}
