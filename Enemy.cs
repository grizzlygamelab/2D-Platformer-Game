using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage = 3;

    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerHealth.TakeDamage(damage);
        }
    }
}
