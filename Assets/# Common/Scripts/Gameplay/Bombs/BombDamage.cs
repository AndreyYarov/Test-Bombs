using System;
using UnityEngine;

public class BombDamage : PlayModeBehaviour
{
    private Action<Collision> OnCollisionCallback;

    public void Init(Action<Collision> OnCollisionCallback)
    {
        this.OnCollisionCallback = OnCollisionCallback;
    }

    private void OnCollisionEnter(Collision collision) 
    {
        OnCollisionCallback?.Invoke(collision);
    }
}
