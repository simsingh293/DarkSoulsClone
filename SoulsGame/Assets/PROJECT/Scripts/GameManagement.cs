using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance = null;
    private int level = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        // Check if instance already exists
        if(instance == null)
        {
            // if not, set instance to this
            instance = this;
            Debug.Log("Game Manager Connected");
        }
        // if instance already exists and it's not this:
        else if(instance != this)
        {
            // Then destroy this. This enforces singleton pattern, meaning there can only ever be one instance of a GameManager
            Destroy(gameObject);
            Debug.Log("Previous Game Manager Deleted");
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // Call the InitGame function to initialize the first level
        InitGame();
    }

    // Initializes the game for each level
    void InitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        SceneManager.LoadScene(2);
        //Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
