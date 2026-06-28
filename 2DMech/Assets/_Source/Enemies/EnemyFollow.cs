using System.Collections;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
  [SerializeField] private LayerMask playerMask;
  [SerializeField] private float speed = 2f;
  [SerializeField] private float detectionRange = 5f;
  [SerializeField] private float stopRange = 0.5f;
  [SerializeField] private int damage = 1;
  [SerializeField] private float attackCooldown = 1f;
    
  private Rigidbody2D _rb;
  private GameObject _player;
  private PlayerMovement _playerMovement;
  private bool _seePlayer;
  private bool _canAttack = true;

  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    _player = GameObject.FindGameObjectWithTag("Player");
    _playerMovement = _player.GetComponent<PlayerMovement>();
  }
    
  private void FixedUpdate()
  {
    if (_seePlayer)
    {
      float distance = Vector2.Distance(transform.position, _player.transform.position);
            
      if (distance < detectionRange && distance > stopRange)
      {
        MoveToPlayer(_player);
      }
    }
  }
    
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (LayerMaskUtil.ContainsLayer(playerMask, other.gameObject))
    {
      _seePlayer = true;
    }
  }
    
  private void OnTriggerExit2D(Collider2D other)
  {
    if (LayerMaskUtil.ContainsLayer(playerMask, other.gameObject))
    {
      _seePlayer = false;
    }
  }
    
  private void MoveToPlayer(GameObject player)
  {
    if(_playerMovement.IsGrounded) return;
    Vector2 direction = (player.transform.position - transform.position).normalized;
    _rb.MovePosition(_rb.position + direction * speed * Time.fixedDeltaTime);
        
    if (direction != Vector2.zero)
    {
      transform.right = direction;
    }
  }
    
  private void OnCollisionStay2D(Collision2D collision)
  {
    if (!_canAttack) return;
    collision.gameObject.TryGetComponent(out Health health);
    if (health != null)
      StartCoroutine(AttackRoutine(health));
  }
  
  private IEnumerator AttackRoutine(Health health)
  {
    _canAttack = false;
    health.GetDamage(damage);
    yield return new WaitForSeconds(attackCooldown);
    _canAttack = true;
  }

}