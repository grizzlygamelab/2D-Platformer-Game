using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _animator;

    private void Awake()
    {
        _playerRb = GetComponentInParent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private IEnumerator ActivatePlayerBody()
    {
        yield return new WaitForSeconds(0.1f);
        _playerRb.bodyType = RigidbodyType2D.Dynamic;
    }
}
