using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _collectibleValue = 1;
    private LevelManager _levelManager;
    //private UIManager _uiManager;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _levelManager = FindObjectOfType<LevelManager>();
        //_uiManager = FindObjectOfType<UIManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _levelManager.UpdateCollectible();
            _animator.SetTrigger("hasBeenCollected");
            StartCoroutine(CreateCollectibleFX());
        }
    }
    
    IEnumerator CreateCollectibleFX()
    {
        yield return new WaitForSeconds(0.3f);
        //_uiManager.SetDiamondCount(_collectibleValue);
        gameObject.SetActive(false);
    }
}
