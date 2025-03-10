using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
public class TitleSceneController : MonoBehaviour
{
    public string SceneToLoad;
    public Button PlayButton;
    public Button QuitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(OnPlayButtonPressed);
        QuitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    private void OnQuitButtonPressed()
    {
        //Application.Quit();
        
        EditorApplication.ExitPlaymode();
    }
}
