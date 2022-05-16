using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BombTemplate : Template<GameObject>
{

    [SerializeField, Min(1), Tooltip("Damage of each bomb spawned by this template")]
    private int damage;

    [SerializeField, Min(0.5f), Tooltip("Radius of damage of each bomb spawned by this template")]
    private float radius;

    public int Damage => damage;
    public float Radius => radius;
}
