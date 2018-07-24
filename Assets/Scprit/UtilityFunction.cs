using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunction  {
    public static float Mapping(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {

        float outVal = ((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin);

        return outVal;
    }

}
