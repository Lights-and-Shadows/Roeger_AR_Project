using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorenz : MonoBehaviour
{
    private LineRenderer line;

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

            positionData[i] = new Vector3((float)x, (float)y, (float)z);
        }
    }

    public void Start()
    {
        n = 4000;
        x0 = y0 = z0 = 1.0;
        positionData = new Vector3[n];
        line = GetComponent<LineRenderer>();
        line.loop = true;
        line.positionCount = n;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] 
            {
                new GradientColorKey(Color.red, 0.0f),
                new GradientColorKey(Color.green, 0.5f),
                new GradientColorKey(Color.blue, 1f)
            },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 0.5f),
                new GradientAlphaKey(1.0f, 1.0f)
            }
        );

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
