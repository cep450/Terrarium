using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*
        Manages scene switching and loading/restarting the sim as a blank slate properly.

        Underscores mark non-static functions for use with built in buttons. 
    */

    void Update() {
        if (Input.GetKey(KeyCode.R)) {
            ReloadLevel();
        }
    }

    public static void CloseGame() {
        Application.Quit();
    }

    public void _GoToLevelSelect() { GoToLevelSelect(); }
    public static void GoToLevelSelect() {
        SceneManager.LoadScene("_LevelSelect");
    }

    public void _GoToMainMenu() { GoToMainMenu(); }
    public static void GoToMainMenu() {
        SceneManager.LoadScene("_MainMenu");
    }

    public void LoadLevel1() { LoadLevel("_Level1"); }
    public void LoadLevel2() { LoadLevel("_Level2"); }
    public void LoadLevel3() { LoadLevel("_Level3"); }
    public void ReloadLevel() {
        LoadLevel(SceneManager.GetActiveScene().name);
    }
    public void LoadLevel(string sceneName) {
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    IEnumerator LoadLevelAsync(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.

        //Hide UI that could cover the loading screen
        FindObjectOfType<Canvas>().gameObject.SetActive(false);

        //Display the loading screen 
        Instantiate(Resources.Load("LoadingScreen"));

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }
        //it will never get here 
    }


}
