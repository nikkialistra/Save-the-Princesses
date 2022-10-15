using System;
using UnityEngine;
using static Characters.Common.Direction9;

namespace Characters.Common
{
    public static class Direction9Utils
    {
        private const float DirectionDelta = 0.003f;

        public static Direction9 Vector2ToDirection9(Vector2 direction)
        {
            return (direction.x , direction.y) switch
            {
                (0, 0) => Center,
                (-1, 0) => Left,
                (-1, 1) => UpLeft,
                (0, 1) => Up,
                (1, 1) => UpRight,
                (1, 0) => Right,
                (1, -1) => DownRight,
                (0, -1) => Down,
                (-1, -1) => DownLeft,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static float Vector2ToRotation(Vector2 direction)
        {
            return (direction.x , direction.y) switch
            {
                (0, 0) => 0,
                (-1, 0) => -90,
                (-1, 1) => -135,
                (0, 1) => 180,
                (1, 1) => 135,
                (1, 0) => 90,
                (1, -1) => 45,
                (0, -1) => 0,
                (-1, -1) => -45,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static Vector2 AnyDirectionToSnappedVector2(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) <= DirectionDelta &&
                Mathf.Abs(direction.y) <= DirectionDelta)
                return Vector2.zero;

            return RoundDirection(SnapDirection(direction));
        }

        // https://answers.unity.com/questions/1865222/how-to-get-8-direction-movement-with-controller-st.html
        private static Vector2 SnapDirection(Vector2 vector, int increments = 8)
        {
            var angle = Mathf.Atan2(vector.y, vector.x);
            var direction = ((angle / Mathf.PI) + 1) * 0.5f; // Convert to [0..1] range from [-pi..pi].
            var snappedDirection = Mathf.Round(direction * increments) / increments; // Snap to increment

            snappedDirection = ((snappedDirection * 2) - 1) * Mathf.PI; // Convert back to [-pi..pi] range

            var snappedVector = new Vector2(Mathf.Cos(snappedDirection), Mathf.Sin(snappedDirection));
            return snappedVector.normalized;
        }

        private static Vector2 RoundDirection(Vector2 snappedDirection)
        {
            var result = snappedDirection;

            if (Mathf.Abs(result.x) <= DirectionDelta)
                result.x = 0f;

            if (Mathf.Abs(result.y) <= DirectionDelta)
                result.y = 0f;

            result.x = Normalize(result.x);
            result.y = Normalize(result.y);

            return result;
        }

        private static float Normalize(float value)
        {
            return value switch
            {
                < 0 => -1,
                > 0 => 1,
                _ => 0
            };
        }
    }
}
