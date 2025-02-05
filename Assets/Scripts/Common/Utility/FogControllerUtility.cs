using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Utility 
{
    public static class FogControllerUtility

    {
        public static void SetFogColor(Color color)
        {
            RenderSettings.fogColor = color;
        }

        public static Color GetFogColor()
        {
            return RenderSettings.fogColor;
        }

        public static void SetFogDensity(float density)
        {
            RenderSettings.fogDensity = density;
        }

        public static void SetFogMode(FogMode mode)
        {
            RenderSettings.fogMode = mode;
        }

        public static void SetFogStartDistance(float distance)
        {
            RenderSettings.fogStartDistance = distance;
        }

        public static float GetFogStartDistance()
        {
            return RenderSettings.fogStartDistance;
        }

        public static void SetFogEndDistance(float distance)
        {
            RenderSettings.fogEndDistance = distance;
        }

        public static void DecreaseFogStartDistance(float decrement)
        {
            RenderSettings.fogStartDistance -= decrement;
        }

        public static void IncreaseFogStartDistance(float increment)
        {
            RenderSettings.fogStartDistance += increment;
        }

        public static void DecreaseFogEndDistance(float decrement)
        {
            RenderSettings.fogEndDistance -= decrement;
        }

        public static void IncreaseFogEndDistance(float increment)
        {
            RenderSettings.fogEndDistance += increment;
        }

        public static float GetFogEndDistance()
        {
            return RenderSettings.fogEndDistance;
        }

        public static void SetFogEnabled(bool enabled)
        {
            RenderSettings.fog = enabled;
        }
    }
}
