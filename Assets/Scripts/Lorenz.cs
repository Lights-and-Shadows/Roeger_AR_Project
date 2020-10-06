using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorenz : MonoBehaviour
{
    private LineRenderer line;
    private ScaleFactor scale; // Made the scale it's own script component to make UI controls flexible

    public double sigma = 10.0;
    public double rho = 28.0;
    public double beta = 8.0 / 3.0;

    public double x0, y0, z0; // Starting positions
    public double x, y, z; // Our variables to edit over time

    public double delta; // Delta value to be used in position plotting
    public double dx, dy, dz; // Functions

    Vector3[] positionData;

    public int n; // Iterations

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
        n = 4000;
        x0 = y0 = z0 = 1.0;
        positionData = new Vector3[n];
        line = GetComponent<LineRenderer>();
        line.loop = false;
        line.useWorldSpace = false;
        line.positionCount = n;
        line.material = new Material(Shader.Find("Sprites/Default"));

        PlotPoints();
    }

    public void Update()
    {
        StartCoroutine(DrawEquation());
    }

    IEnumerator DrawEquation()
    {
        for (int i = 0; i < n; i++)
        {
            line.SetPosition(i, positionData[i]);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
