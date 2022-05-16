using System;
using UnityEngine;

[Serializable]
public abstract class Template
{

    [SerializeField, Min(0), Tooltip("Count of spawned objects\nZero is infinity")]
    private int count;

    [SerializeField, Min(0f), Tooltip("Delay between start of game and first spawn")]
    private float time;

    [SerializeField, Min(0f), Tooltip("Delay between objects spawn in seconds\nNot 0 if count is infinity")]
    private float spawnDelay;

    public int Count => count;
    public float Time => time;
    public float SpawnDelay => spawnDelay;
    public abstract bool ReadyForSpawn();
}

[Serializable]
public abstract class Template<T> : Template
    where T : UnityEngine.Object
{
    [SerializeField, Tooltip("Prefab for  spawn")]
    private T prefab;

    public T Prefab => prefab;
    public override bool ReadyForSpawn() => prefab && (Count > 0 || SpawnDelay > 0f);
}
