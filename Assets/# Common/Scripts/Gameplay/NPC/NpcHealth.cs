using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHealth : MonoBehaviour
{
    private Collider npcCollider;
    private float health;
    public float Health => health;

    private void Awake()
    {
        npcCollider = GetComponent<Collider>();
    }

    private void Reset()
    {
        if (!Application.isPlaying)
            DestroyImmediate(this);
    }

    public void Init(int health)
    {
        this.health = health;
    }
}
