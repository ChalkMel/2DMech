using UnityEngine;

public class GroundSwitch : MonoBehaviour
{
  [SerializeField] private LayerMask playerMask;
  [SerializeField] private Sprite playerSprite;
  [SerializeField] private SpriteRenderer playerSpriteRenderer;
  [SerializeField] private float speed = 3f;
  [SerializeField] private PlayerMovement playerMovement;
    
  private Sprite _originalSprite;
  private float _originalSpeed;
    
  private void Awake()
  {
    if (playerSpriteRenderer != null)
      _originalSprite = playerSpriteRenderer.sprite;
    if (playerMovement != null)
      _originalSpeed = playerMovement.Speed;
  }
    
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (LayerMaskUtil.ContainsLayer(playerMask, other.gameObject))
    {
      if (playerSpriteRenderer != null && playerSprite != null)
        playerSpriteRenderer.sprite = playerSprite;
      if (playerMovement != null)
        playerMovement.Speed = speed;
    }
  }
    
  private void OnTriggerExit2D(Collider2D other)
  {
    if (LayerMaskUtil.ContainsLayer(playerMask, other.gameObject))
    {
      if (playerSpriteRenderer != null && _originalSprite != null)
        playerSpriteRenderer.sprite = _originalSprite;
      if (playerMovement != null)
        playerMovement.Speed = _originalSpeed;
    }
  }
}