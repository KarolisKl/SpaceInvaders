using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementCycles
{
    //declaring required directions
    static Vector3 u = Vector3.forward;
    static Vector3 d = Vector3.back;
    static Vector3 r = Vector3.right;
    static Vector3 l = Vector3.left;
    static Vector3 ur = new Vector3(0.5f, 0, 0.5f);
    static Vector3 ul = new Vector3(-0.5f, 0, 0.5f);
    static Vector3 dr = new Vector3(0.5f, 0, -0.5f);
    static Vector3 dl = new Vector3(-0.5f, 0, -0.5f);

    public static Vector3[] Circle(bool inversed = false)
    {
        Vector3[] circle = inversed ? 
            new Vector3[] { dl, d, dr, r, ur, u, ul } : 
            new Vector3[] { dr, d, dl, l, ul, u, ur };
        return circle;
    }

    public static Vector3[] Zigzag(bool inversed = false)
    {
        Vector3[] zigzag = inversed ?
            new Vector3[] { dl, l, ul, l, dl, l } :
            new Vector3[] { dr, r, ur, r, dr, r };
        return zigzag;
    }

    public static Vector3[] Curve(bool inversed = false)
    {
        Vector3[] curve = inversed ?
            new Vector3[] { l, dl, d, dr, r, r, ur } :
            new Vector3[] { r, dr, d, dl, dl, l, ul };
        return curve;
    }

    public static Vector3[] Line(bool inversed = false)
    {
        Vector3[] line = inversed ?
            new Vector3[] { r, r, r, r, r, r , r} :
            new Vector3[] { l, l, l, l, l, l , l};
        return line;
    }
 
}
