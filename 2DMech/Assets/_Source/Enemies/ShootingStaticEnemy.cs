using UnityEngine;

public class ShootingStaticEnemy : MonoBehaviour
{
  [SerializeField] private GameObject bulletPrefab;
  [SerializeField] private float fireRate = 2f;
  [SerializeField] private Transform firePoint;
  [SerializeField] private bool isStatic = true;
  [SerializeField] private float detectionRange = 10f;
    
  private float _timer;
  private Transform _player;
    
  private void Start()
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
      _player = player.transform;
  }
    
  private void Update()
  {
    if (_player == null) return;
        
    float distance = Vector3.Distance(transform.position, _player.position);
    if (distance > detectionRange) return;
        
    _timer += Time.deltaTime;
    if (_timer >= fireRate)
      Shoot();
  }
    
  private void Shoot()
  {
    if (!isStatic && _player != null)
    {
      Vector2 direction = (_player.position - transform.position).normalized;
      transform.up = direction;
    }
        
    Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    _timer = 0f;
  }
}