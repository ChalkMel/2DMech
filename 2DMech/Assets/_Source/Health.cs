using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Image healthBar;
    [SerializeField] private ParticleSystem destroyParticles;
    
    private int _currentHealth;
    
    private void Awake()
    {
        _currentHealth = maxHealth;
        UpdateHealthBar();
    }
    
    public void GetDamage(float damage)
    {
        _currentHealth -= (int)damage;
        UpdateHealthBar();
        
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.fillAmount = (float)_currentHealth / maxHealth;
    }
    
    private void Die()
    {
        if (destroyParticles != null)
            destroyParticles.Play(true);
        TryGetComponent(out PlayerMovement player);
        if (player != null)
            player.Death();
        Destroy(gameObject, .5f);
    }
}