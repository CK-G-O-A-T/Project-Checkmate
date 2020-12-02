using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public enum Vector2DigitalizeOption
{
    None,
    Vertical,
    Horizontal,
    FourDirection,
    EightDirection
}
public static class Vector2Extension
{
    public static Vector2 Digitalize(this Vector2 vector, Vector2DigitalizeOption option = Vector2DigitalizeOption.FourDirection)
    {
        if (option == Vector2DigitalizeOption.None)
            return vector;
        switch (option)
        {
            default:
                return vector;
            case Vector2DigitalizeOption.Vertical:
                return
                    vector.y > 0 ? Vector2.up :
                    vector.y < 0 ? Vector2.down :
                    Vector2.zero;
            case Vector2DigitalizeOption.Horizontal:
                return
                    vector.x > 0 ? Vector2.right :
                    vector.x < 0 ? Vector2.left :
                    Vector2.zero;
            case Vector2DigitalizeOption.FourDirection:
            case Vector2DigitalizeOption.EightDirection:
                {
                    var rawAngle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;

                    if (option == Vector2DigitalizeOption.FourDirection)
                    {
                        if (vector.sqrMagnitude == 0)
                        {
                            return Vector2.zero;
                        }
                        else if (rawAngle >= 135 || rawAngle <= -135)
                        {
                            return Vector2.down;
                        }
                        else if (rawAngle > 45)
                        {
                            return Vector2.right;
                        }
                        else if (rawAngle < -45)
                        {
                            return Vector2.left;
                        }
                        else
                        {
                            return Vector2.up;
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //if (rawAngle < 22.5f && rawAngle > -22.5f)
                        //{
                        //    return Vector2.right;
                        //}
                        //else if (rawAngle < 67.5f)
                        //{
                        //    return new Vector2(0.70710678118654752440084436210485f, 0.70710678118654752440084436210485f);
                        //}
                        //else if (rawAngle < -67.5f)
                        //{
                        //    return new Vector2(0.70710678118654752440084436210485f, -0.70710678118654752440084436210485f);
                        //}
                        //else if (rawAngle < 112.5f)
                        //{
                        //    return Vector2.up;
                        //}
                        //else if (rawAngle < -112.5f)
                        //{
                        //    return Vector2.down;
                        //}
                        //else if (rawAngle < 157.5f)
                        //{
                        //    return new Vector2(-0.70710678118654752440084436210485f, 0.70710678118654752440084436210485f);
                        //}
                        //else if (rawAngle < -157.5f)
                        //{
                        //    return new Vector2(-0.70710678118654752440084436210485f, -0.70710678118654752440084436210485f);
                        //}
                        //else
                        //{
                        //    return Vector2.left;
                        //}
                    }
                }
        }
    }
}