using UnityEngine;

public class MainCameraController : MonoBehaviour
{
  [SerializeField] private Transform player;
  [SerializeField] private float smoothSpeed = 0.125f;
  [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    
  private void LateUpdate()
  {
    Vector3 desiredPosition = player.position + offset;
    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    transform.position = smoothedPosition;
  }
}