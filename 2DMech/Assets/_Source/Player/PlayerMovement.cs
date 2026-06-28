using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float Speed = 5f;
    
    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundCheckMask;
    
    [Header("Shooting")]
    [SerializeField] private GameObject bulletPrefab;

    public Transform FirePoint;
    [SerializeField] private float fireRate = 0.5f;
    
    private Rigidbody2D _rb;
    private float _fireTimer;
    private Vector2 _lastMoveDirection = Vector2.right;

    private bool _isGrounded;
    
    public bool IsGrounded => _isGrounded;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundCheckMask);
    }

    public void Move(Vector2 movement)
    {
        _rb.linearVelocity = movement.normalized * Speed;
        
        if (movement.magnitude > 0.01f)
        {
            _lastMoveDirection = movement.normalized;
             transform.up = movement.normalized;
        }
    }
    
    public void Shoot(Vector2 direction)
    {
        if (Time.time < _fireTimer) return;
        if (bulletPrefab == null || FirePoint == null) return;
        
        GameObject bullet = Instantiate(bulletPrefab, FirePoint.position, Quaternion.identity);

        bullet.transform.up = direction;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.SetOwner(gameObject);
        
        _fireTimer = Time.time + fireRate;
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}