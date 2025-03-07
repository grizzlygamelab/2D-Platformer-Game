using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _collectibleValue = 1;
    [SerializeField] private bool _hasBeenCollected = false;
    private LevelManager _levelManager;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _levelManager = FindObjectOfType<LevelManager>();
        //_uiManager = FindObjectOfType<UIManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasBeenCollected)
        {
            _levelManager.UpdateCollectible();
            _animator.SetTrigger("hasBeenCollected");
            _hasBeenCollected = true;
        }
    }
    
    private void DisableFruitOnCollision()
    {
        gameObject.SetActive(false);
    }
}
