using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // references
    public GameObject Player;
    public GameBehaviour GameBehaviour;

    // core
    public int enemyHealth = 10;

    // player detection
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public bool playerDetection;

    public LayerMask playerMask;
    public LayerMask obstacleMask;

    // partolling
    public Transform PatrolRoute;
    public List<Transform> Locations;
    private int _locationIndex;
    private NavMeshAgent _agent;

    // weapon
    public GameObject Laser;
    public float LaserSpeed = 1f;
    public bool _isShooting;

    public GameObject Key;
    public GameObject Explosion;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();

        StartCoroutine(SearchRoutine());
    }

    void Update()
    {
        // Shoots at player
        if (playerDetection == true)
        {
            transform.LookAt(Player.transform);
            _agent.destination = Player.transform.position;
            _isShooting = true;
        }
        else
        {
            _isShooting = false;
        }

        // Agent movement
        if (_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }

        // Death
        if (enemyHealth <= 0)
        {
            // Spawns KeyCard
            if (GameBehaviour.EnemiesRemaining == 1)
            {
                GameObject newKey = Instantiate(Key, this.transform.position, Quaternion.identity);
            }

            GameBehaviour.EnemiesRemaining -= 1;
            this.gameObject.SetActive(false);

            GameBehaviour.Instance.PlaySound(GameBehaviour.deathSound, 0.7f);

            GameObject newExplosion = Instantiate(Explosion,
                this.transform.position,
                this.transform.rotation);
        }
    }

    private void FixedUpdate()
    {
        if (_isShooting)
        {
            GameObject newLaser = Instantiate(Laser,
                this.transform.position,
                this.transform.rotation);
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in PatrolRoute)
            Locations.Add(child);

        transform.position = Locations[0].position;
    }

    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0) return;

        _agent.destination = Locations[_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }

    private IEnumerator SearchRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        // looks for player within sphere
        Collider[] checks = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        if (checks.Length != 0)
        {
            // stores player information if within sphere
            Transform target = checks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // checks if withing viewing angle
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // checks if obstacle mask is in the way
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    playerDetection = true;
                else
                    playerDetection = false;
            }
            else
                playerDetection = false;
        }
        else if (playerDetection)
            playerDetection = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("PlayerLaser"))
            enemyHealth -= 1;
    }

}

