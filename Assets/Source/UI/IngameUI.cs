using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject objectToDisable;

    private GameObject ui = null;
    private bool cursorVisibility;

    void Update ()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    private void OnDestroy()
    {
        if (gameIsPaused)
        {
            Resume();
        }
    }

    private void Resume()
    {
        Destroy(ui);
        Time.timeScale = 1.0f;
        gameIsPaused = false;

        if (objectToDisable != null)
        {
            objectToDisable.SetActive(true);
        }

        Cursor.visible = cursorVisibility;
    }

    private void Pause()
    {
        ui = Instantiate(pauseMenu, transform);
        Time.timeScale = 0.0f;
        gameIsPaused = true;

        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }

        cursorVisibility = Cursor.visible;
        Cursor.visible = true;
    }
}
