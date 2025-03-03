using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float _bounceModifier = 1f;
    private Animator _trampolineAnimator;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _trampolineAnimator = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( _player.transform.position.y > transform.position.y)
        {
            _player.TrampolineJump(_bounceModifier);
            _trampolineAnimator.SetTrigger("Bounce");
        }
    }
}
