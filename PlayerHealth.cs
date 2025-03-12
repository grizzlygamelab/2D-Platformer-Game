using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 10;
    
    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        _player.Knockback();

        if (health <= 0)
        {
            StartCoroutine(RestartLevelRoutine());
        }
    }

    private IEnumerator RestartLevelRoutine()
    {
        _player.Knockback();
        yield return new WaitForSeconds(0.6f);
        GameObject.Find("Level Manager").GetComponent<LevelManager>().RestartLevel();
    }
}
