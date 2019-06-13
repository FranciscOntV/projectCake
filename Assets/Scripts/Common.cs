using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    public static float timedValue(float value) {
        return (Time.deltaTime * value);
    } 
}
