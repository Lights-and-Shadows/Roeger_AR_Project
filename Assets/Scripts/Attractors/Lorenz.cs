using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorenz : MonoBehaviour
{
    private LineRenderer line;
    private ScaleFactor scale; // Made the scale it's own script component to make UI controls flexible

    private double sigma = 10.0;
    private double rho = 28.0;
    private double beta = 8.0 / 3.0;

    private double x0, y0, z0; // Starting positions
    private double x, y, z; // Our variables to edit over time

    private double delta; // Delta value to be used in position plotting
    private double dx, dy, dz; // Functions

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
            dx = sigma * (y - x);
            dy = x * (rho - z) - y;
            dz = x * y - beta * z;

            // New coordinates
            x = x + delta * dx;
            y = y + delta * dy;
            z = z + delta * dz;

            positionData[i] = new Vector3((float)x * (float)scale.scaleFactor, (float)y * (float)scale.scaleFactor, (float)z * (float)scale.scaleFactor);
        }
    }

    public void Start()
    {
        n = 3000;
        x0 = y0 = z0 = 1.0;
        positionData = new Vector3[n];
        line = GetComponent<LineRenderer>();
        line.loop = false;
        line.useWorldSpace = false;
        line.positionCount = n;
        line.material = new Material(Shader.Find("Sprites/Default"));
        scale = gameObject.GetComponent<ScaleFactor>();
        delta = 0.01;
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
