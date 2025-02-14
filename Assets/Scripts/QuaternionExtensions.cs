using UnityEngine;

public static class QuaternionExtensions
{
    public static Vector3 ToEulerYZX(this Quaternion q)
    {
        float y, z, x;

        // Extract angles using YZX rotation order
        y = Mathf.Atan2(q.y * q.w - q.x * q.z, 0.5f - (q.y * q.y + q.z * q.z));
        z = Mathf.Asin(Mathf.Clamp(q.x * q.y + q.z * q.w, -0.5f, 0.5f)) * 2.0f;
        x = Mathf.Atan2(q.x * q.w - q.y * q.z, 0.5f - (q.x * q.x + q.z * q.z));

        // Convert radians to degrees
        return new Vector3(x, y, z) * Mathf.Rad2Deg;
    }
}