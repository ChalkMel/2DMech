using UnityEngine;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
  [SerializeField] private PlayerMovement playerMovement;
  [SerializeField] private Camera mainCamera;
    
  private InputSystem_Actions _inputSystemActions;
  private Vector2 _mousePosition;
    
  private void Awake()
  {
    _inputSystemActions = new InputSystem_Actions();
    if (mainCamera == null)
      mainCamera = Camera.main;
  }
    
  private void OnEnable()
  {
    _inputSystemActions.Enable();
  }
    
  private void Update()
  {
    _mousePosition = Mouse.current.position.ReadValue();
    Vector2 moveDirection = _inputSystemActions.Player.Move.ReadValue<Vector2>();
    playerMovement.Move(moveDirection);

    if (_inputSystemActions.Player.Attack.WasPressedThisFrame())
    {
      Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, 0));
      mouseWorldPos.z = 0;
      
      Vector2 shootDirection = (mouseWorldPos - playerMovement.FirePoint.position).normalized;
      playerMovement.Shoot(shootDirection);
    }
  }
    
  private void OnDisable()
  {
    _inputSystemActions.Disable();
  }
}