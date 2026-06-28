using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] private float bulletLifetime = 3f;
  [SerializeField] private float bulletImpulse = 10f;
  [SerializeField] private float damage = 1f;
    
  private GameObject _owner;
  private Rigidbody2D _rb;
    
  private void Awake()
  {
    _rb = GetComponent<Rigidbody2D>();
    Destroy(gameObject, bulletLifetime);
  }
    
  private void Start()
  {
    _rb.AddForce(transform.up * bulletImpulse, ForceMode2D.Impulse);
  }
    
  public void SetOwner(GameObject owner)
  {
    _owner = owner;
  }
    
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (_owner != null && other.gameObject == _owner)
      return;

    if (other.gameObject.TryGetComponent(out Health health))
    {
      health.GetDamage(damage);
    }

    Destroy(gameObject);
  }
}