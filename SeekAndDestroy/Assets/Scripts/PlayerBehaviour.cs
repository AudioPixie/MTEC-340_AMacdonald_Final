using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameBehaviour GameBehaviour;
    public DoorBehaviour Door1;
    public DoorBehaviour Door2;
    public MusicFade Music1;
    public MusicFade Music2;

    // Movement
    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;

    private float xInput;
    private float yInput;

    private Rigidbody rb;

    // Particle Effect
    public GameObject Explosion;

    // GUI
    public CanvasGroup deathGUI;
    public CanvasGroup winGUI;

    // Laser Gun
    public GameObject Laser;
    public float LaserSpeed = 1f;
    public bool canShoot;
    public bool _isShooting;

    // Key Card
    public bool hasKey = false;

    private void Start()
    {
        deathGUI.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();

        canShoot = false;
        hasKey = false;
    }

    void FixedUpdate()
    {
        if (GameBehaviour.Instance.State == "Seek" || GameBehaviour.Instance.State == "Destroy")
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            // WASD movement
            Vector3 movement = new Vector3(xInput, 0f, yInput);
            movement.Normalize();
            rb.velocity = movement * MoveSpeed;

            // Rotation follows cursor
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(this.transform.position.y - mousePos.y, mousePos.x - this.transform.position.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

            // Shooting
            if (_isShooting)
            {
                GameObject newLaser = Instantiate(Laser,
                    this.transform.position,
                    this.transform.rotation);
            }
        }
    }

    private void Update()
    {
        if (canShoot == true)
        {
            if (Input.GetMouseButtonDown(0))
                _isShooting = true;
            if (Input.GetMouseButtonUp(0))
                _isShooting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("EnemyLaser"))
        {
            GameBehaviour.PlayerHealth -= 1;

            if (GameBehaviour.PlayerHealth <= 0)
                Death();
        }

        if (other.gameObject.tag == "Weapon")
        {
            GameBehaviour.Instance.PlaySound(GameBehaviour.weaponPickup, 0.7f);
            GameBehaviour.Instance.State = "Destroy";

            canShoot = true;

            Music1.FadeOut();
            Music2.FadeIn();
        }

        if (other.gameObject.tag == "Health")
        {
            GameBehaviour.Instance.PlaySound(GameBehaviour.healthPickup, 1.5f);
            GameBehaviour.PlayerHealth += 20;
        }

        if (other.gameObject.tag == "Key")
        {
            GameBehaviour.Instance.PlaySound(GameBehaviour.keyPickup, 1.5f);
            hasKey = true;
        }

        if (other.gameObject.tag == "EndPanel")
        {
            if (hasKey == true)
            {
                GameBehaviour.Instance.PlaySound(GameBehaviour.keyPickup, 1f);
                GameBehaviour.Instance.PlaySound(GameBehaviour.doorOpen, 0.3f);
                Door1.Open();
                Door2.Open();
            }
        }

        if (other.gameObject.tag == "EndZone")
        {
            GameBehaviour.Instance.State = "End";
            GameBehaviour.EnemiesRemainingGUI.gameObject.SetActive(false);
            StartCoroutine(Win());
        }
    }

    public void Death()
    {
        GameBehaviour.Instance.PlaySound(GameBehaviour.deathSound, 0.7f);
        deathGUI.gameObject.SetActive(true);

        GameObject newExplosion = Instantiate(Explosion,
            this.transform.position,
            this.transform.rotation);

        this.gameObject.SetActive(false);
    }

    IEnumerator Win()
    {
        yield return new WaitForSecondsRealtime(2f);
        winGUI.gameObject.SetActive(true);

    }
}
