using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 pivot = Vector3.zero;
    [SerializeField] private float rotationSensitivity = 3f;

    private void Update()
    {
        float rot = Input.GetAxis("Mouse X");
        if (rot != 0f)
            transform.RotateAround(pivot, Vector3.up, rot * rotationSensitivity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(pivot, 0.25f);
    }
}
