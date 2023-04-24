using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{

    public CanvasGroup mainMenu;
    public CanvasGroup howTo1;
    public CanvasGroup howTo2;

    private AudioSource m_audioSource;

    public AudioClip clickSound;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        howTo1.gameObject.SetActive(false);
        howTo2.gameObject.SetActive(false);

        mainMenu.gameObject.SetActive(true);
    }

    public void Play()
    {
        PlaySound(clickSound);
        SceneManager.LoadScene(1);
    }

    public void HowTo1()
    {
        PlaySound(clickSound);
        mainMenu.gameObject.SetActive(false);
        howTo2.gameObject.SetActive(false);

        howTo1.gameObject.SetActive(true);
    }

    public void HowTo2()
    {
        PlaySound(clickSound);
        mainMenu.gameObject.SetActive(false);
        howTo1.gameObject.SetActive(false);

        howTo2.gameObject.SetActive(true);
    }

    public void Reset()
    {
        PlaySound(clickSound);
        howTo1.gameObject.SetActive(false);
        howTo2.gameObject.SetActive(false);

        mainMenu.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        PlaySound(clickSound);
        Application.Quit();
    }

    public void PlaySound(AudioClip clip, float volume = 0.7f)
    {
        m_audioSource.volume = volume;
        m_audioSource.PlayOneShot(clip);
    }
}
