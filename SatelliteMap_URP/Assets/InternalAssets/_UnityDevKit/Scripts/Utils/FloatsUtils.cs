using UnityEngine;

namespace UnityDevKit.Utils
{
    public static class FloatsUtils
    {
        public static float AddError(this float value, float error)
        {
            return value * (1 + Random.Range(-error, error));
        }
        
        public static float CalculateThreePointAngle(Vector3 first, Vector3 second, Vector3 third)
        {
            var threePointAngle = Vector2.Angle(
                new Vector2(first.x, first.z) - new Vector2(second.x, second.z),
                new Vector2(third.x, third.z) - new Vector2(second.x, second.z));

            return threePointAngle;
        }
        
        public static float CalculateThreePointSignedAngle(Vector3 first, Vector3 second, Vector3 third)
        {
            var threePointAngle = Vector2.SignedAngle(
                new Vector2(first.x, first.z) - new Vector2(second.x, second.z),
                new Vector2(third.x, third.z) - new Vector2(second.x, second.z));

            return threePointAngle;
        }
    }
}