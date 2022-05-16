using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NpcTemplate
{
    [SerializeField, Tooltip("Prefab for each NPC spawned by this template")]
    private NpcController prefab;

    [SerializeField, Min(1), Tooltip("Health of each NPC spawned by this template")]
    private int health;

    [SerializeField, Min(0), Tooltip("Count of spawned NPCs\nZero is infinity")]
    private int count;

    [SerializeField, Min(0f), Tooltip("Delay between start of game and first spawn")]
    private float time;

    [SerializeField, Min(0f), Tooltip("Delay between NPCs spawn in seconds\nNot 0 if count is infinity")]
    private float spawnDelay;

    [SerializeField, Min(0f), Tooltip("Speed of each NPC spawned by this template\nZero if stand still")]
    private float speed;

    public NpcController Prefab => prefab;
    public int Health => health;
    public int Count => count;
    public float Time => time;
    public float SpawnDelay => spawnDelay;
    public float Speed => speed;
    public bool ReadyForSpawn => prefab && (count > 0 || spawnDelay > 0f);
}
