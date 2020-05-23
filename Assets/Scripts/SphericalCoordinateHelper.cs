using System;
using UnityEngine;

public static class SphericalCoordinateHelper {
    //Theta goes from -pi/2 to pi/2
    //Phi goes from -pi to pi

    public static float getXFromSpherical(float theta, float phi, float radius) {
        return radius * Mathf.Sin(toRadians(theta)) * Mathf.Cos(toRadians(phi));
    }

    public static float getYFromSpherical(float theta, float phi, float radius) {
        return radius * Mathf.Sin(toRadians(theta)) * Mathf.Sin(toRadians(phi));
    }

    public static float getZFromSpherical(float theta, float phi, float radius) {
        return radius * Mathf.Cos(toRadians(theta));
    }

    public static Vector3 getVector3FromSpherical(float theta, float phi, float radius) {
        return new Vector3(getXFromSpherical(theta, phi, radius), getYFromSpherical(theta, phi, radius), getZFromSpherical(theta, phi, radius));
    }

    public static float getRadiusFromCartesian(Vector3 position) {
        return Mathf.Sqrt(Mathf.Pow(position.x, 2f) + Mathf.Pow(position.y, 2f) + Mathf.Pow(position.z, 2f));
    }

    public static float getThetaFromCartesian(Vector3 position) {
        float radius = getRadiusFromCartesian(position);

        return toDegree(Mathf.Acos(position.z / radius));
    }

    public static float getPhiFromCartesian(float x, float y) {
        return toDegree(Mathf.Atan(y / x));
    }

    private static float toDegree(float radian) {
        return (180 / Mathf.PI) * radian;
    }

    private static float toRadians(float degree) {
        return degree * (Mathf.PI / 180);
    }
}
