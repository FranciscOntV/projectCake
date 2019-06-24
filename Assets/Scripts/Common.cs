using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    /// <summary>
    /// Multiplies a value by Time.deltaTime.
    /// </summary>
    /// <param name="value">The velue to multiply, leave empty to get deltaTime.</param>
    /// <returns>
    /// float the value multiplied.
    /// </returns>
    public static float timedValue(float value = 1f) {
        return (Time.deltaTime * value);
    } 

    public static string grabableTag = "grabable";
    public static string terrainTag = "terrain";
    public static string usableTag = "usable";

    public static Vector3 grabbedPosition = new Vector3(0f, 1.5f, 0f);
    public static Vector3 dropPosition = new Vector3(0f, 1f, 0f);
    public static float throwForce = 3f;
    public static float grabSpeed = 5f;
}
