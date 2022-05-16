using UnityEngine;

public abstract class PlayModeBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        if (!Application.isPlaying)
            DestroyImmediate(this);
    }
}
