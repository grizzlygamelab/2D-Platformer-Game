using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int collectibleAmount;
    public string whatIsNextLevel;
    
    // Start is called before the first frame update
    void Start()
    {
        collectibleAmount = GameObject.FindGameObjectsWithTag("Collectible").Length;    
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(whatIsNextLevel);
    }

    public void UpdateCollectible()
    {
        collectibleAmount--;
        
        if (collectibleAmount <= 0)
        {
            GameObject.Find("Character").GetComponent<PlayerAnimations>().ExitAnimation();
        }
    }
    
}
