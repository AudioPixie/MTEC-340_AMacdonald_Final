using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour
{
    public GameBehaviour GameBehaviour;
    public CanvasGroup pauseGUI;

    public void Reload(int i)
    {
        GameBehaviour.Instance.PlaySound(GameBehaviour.clickSound, 0.7f);
        Time.timeScale = 1;
        SceneManager.LoadScene(i);
    }

    public void Continue()
    {
        GameBehaviour.Instance.PlaySound(GameBehaviour.clickSound, 0.7f);
        pauseGUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
