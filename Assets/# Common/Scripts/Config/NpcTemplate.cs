using System;
using UnityEngine;

[Serializable]
public class NpcTemplate : Template<NpcController>
{

    [SerializeField, Min(1), Tooltip("Health of each NPC spawned by this template")]
    private int health;

    [SerializeField, Min(0f), Tooltip("Speed of each NPC spawned by this template\nZero if stand still")]
    private float speed;

    public int Health => health;
    public float Speed => speed;
}
