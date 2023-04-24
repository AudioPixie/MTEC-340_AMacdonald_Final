using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameBehaviour : MonoBehaviour
{
    public static GameBehaviour Instance;
    public PlayerBehaviour Player;

    [SerializeField]
    private string _state;
    public string State
    {
        get => _state;
        set
        {
            _state = value;
        }
    }

    public int PlayerHealth = 10;
    public int EnemiesRemaining;

    public KeyCode pauseKey;

    public CanvasGroup pauseGUI;
    public CanvasGroup winGUI;
    public TextMeshProUGUI EnemiesRemainingGUI;

    public EnemySpawner EnemySpawner;

    // Audio
    private AudioSource m_audioSource;

    public AudioClip clickSound;
    public AudioClip weaponPickup;
    public AudioClip healthPickup;
    public AudioClip keyPickup;
    public AudioClip doorOpen;
    public AudioClip deathSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        State = "Seek";
    }

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        pauseGUI.gameObject.SetActive(false);
        winGUI.gameObject.SetActive(false);

        EnemiesRemaining = EnemySpawner.spawnCount1 + EnemySpawner.spawnCount2;
        EnemySpawner.SpawnEnemies();
        EnemiesRemainingGUI.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Player.hasKey == false)
            EnemiesRemainingGUI.text = "Enemies Remaining: " + EnemiesRemaining + "\n";
        else
            EnemiesRemainingGUI.text = "Got the key!\n";

        if (State != "End")
        {
            if (Input.GetKeyDown(pauseKey))
            {
                Time.timeScale = 0;
                pauseGUI.gameObject.SetActive(true);
            }
        }
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        m_audioSource.volume = volume;
        m_audioSource.PlayOneShot(clip);
    }
}
