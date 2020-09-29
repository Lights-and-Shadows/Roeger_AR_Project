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

        }
    }

    public void Start()
    {
        n = 2000;
        x0 = y0 = z0 = 1.0;
        positionData = new Vector3[n];
    }
}
