using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);        
        Cursor.visible = false;
    }
    
    public void QuitGame() {
        Time.timeScale = 1;
        //TODO - For some reason this 'Application.Quit' is causing the game to crash when you hit PlayAgain.
        //       Commenting Out as a workaround - Use Alt+F4 to Quit the game.
        //Application.Quit();        
    }
}
