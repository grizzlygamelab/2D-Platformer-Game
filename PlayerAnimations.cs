using System.Collections;
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
    
    private IEnumerator DeactivatePlayerBody()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("Level Manager").GetComponent<LevelManager>().LoadNextLevel();
        
    }

    public void ExitAnimation()
    {
        _playerRb.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger("playerExit");
    }
}
