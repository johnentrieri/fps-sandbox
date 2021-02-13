using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Canvas winnerCanvas;
    private int numEnemies;

    // Start is called before the first frame update
    void Start()
    {
        winnerCanvas.enabled = false;
        numEnemies = GetComponentsInChildren<EnemyHealth>().Length;
    }

    public void EnemyDeathHandler() {
        if( --numEnemies <= 0) {
            ProcessWin();
        }
    }

    private void ProcessWin() {
        winnerCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
