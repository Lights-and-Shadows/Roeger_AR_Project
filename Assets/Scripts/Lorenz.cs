using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lorenz : MonoBehaviour
{
    public double sigma = 10.0;
    public double rho = 28.0;
    public double beta = 8.0 / 3.0;
    public int n = 3;
    public double[] x;
    public double[] xprime;
    public double[] xstore;
    public double[] k1;
    public double[] k2;
    public double[] k3;
    public double[] k4;

    // Initialization
    public void Start()
    {
        x = new double[n];
        xprime = new double[n];
        xstore = new double[n];
        k1 = new double[n];
        k2 = new double[n];
        k3 = new double[n];
        k4 = new double[n];
    }

    void RatesOfChange(double[] xin)
    {
        double x = xin[0];
        double y = xin[1];
        double z = xin[2];
        xprime[0] = sigma * (y - x);
        xprime[1] = x * (rho - z) - y;
        xprime[2] = x * y - beta * z;
    }

    public void FixedUpdate()
    {
        double h = (double)Time.fixedDeltaTime;
        RatesOfChange(x); // Start at current position
        for (int i = 0; i < n; i++)
        {
            k1[i] = xprime[i] * h;
            xstore[i] = x[i] + 0.5 * k1[i];
        }
    }

    // Called once per frame
    public void Update()
    {
        
    }
}
