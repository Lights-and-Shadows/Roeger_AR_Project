using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // For math functions specific to doubles -- will do conversions to float for Unity-specific code

public class Chua : MonoBehaviour
{
    private LineRenderer line;
    private ScaleFactor scale; // Scale component

    // Constants
    public double a = 15.6;
    public double b = 1.0;
    public double c = 25.58;
    public double d = -1;
    public double e = 0;

    public double x0, y0, z0; // Starting positions
    public double x, y, z; // Variables to edit over time

    public double delta; // Used in position plotting
    public double dx, dy, dz, g; // Functions

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
            // Assigning the "g" helper function
            g = e * x + (d + e) * (Math.Abs(x + 1) - Math.Abs(x - 1));
            // Function assignments
            dx = a * (y - x - g);
            dy = b * (x - y + z);
            dz = -c * y;

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
        n = 2000;
        x0 = y0 = z0 = 1.0;
        positionData = new Vector3[n];
        line = GetComponent<LineRenderer>();
        line.loop = false;
        line.useWorldSpace = false;
        line.positionCount = n;
        line.material = new Material(Shader.Find("Sprites/Default"));
        scale = gameObject.GetComponent<ScaleFactor>();
        PlotPoints();
    }

    // Update is called once per frame
    void Update()
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
