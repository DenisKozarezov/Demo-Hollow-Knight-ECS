using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Utils
    {
        public static float CalculateJumpForce(float gravity, float height)
        {
            return Mathf.Sqrt(2 * gravity * height);
        }
    }
}