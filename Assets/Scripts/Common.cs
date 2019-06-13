using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : MonoBehaviour
{
    public static float timedValue(float value) {
        return (Time.deltaTime * value);
    } 
}
