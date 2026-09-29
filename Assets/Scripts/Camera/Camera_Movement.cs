using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [Header("Camera Smooth")]
    [SerializeField] private Transform Target;
    [SerializeField] private Camera cam;
    [SerializeField] private float BaseSize = 5;
    [SerializeField] private float MaxSize = 10;
    [SerializeField] private float Smooth_Time = 0.15f;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 Offset = new Vector3 (0, 0, -10);
    private Vector3 Velocity;
    private float MaxSpeed = 20f;
    private float ZoomVelocity;

    private void LateUpdate()
    {
        Vector3 new_position = Target.position + Offset;

        float Speed = rb.linearVelocity.magnitude;
        float t = Mathf.InverseLerp(0, MaxSpeed, Speed);
        float TargetSize = Mathf.Lerp(BaseSize, MaxSize, t);
        float CurrentSize = Mathf.SmoothDamp(cam.orthographicSize, TargetSize, ref ZoomVelocity, Smooth_Time);

        cam.orthographicSize = CurrentSize;
        transform.position = Vector3.SmoothDamp(transform.position, new_position, ref Velocity, Smooth_Time);
    }
}
