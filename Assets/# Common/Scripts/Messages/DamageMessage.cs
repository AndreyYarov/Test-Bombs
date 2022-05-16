using UnityEngine;

public class DamageMessage
{
    public int damage;
    public float radius;
    public Vector3 center;

    public DamageMessage(int damage, float radius, Vector3 center)
    {
        this.damage = damage;
        this.radius = radius;
        this.center = center;
    }
}
