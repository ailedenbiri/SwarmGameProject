using UnityEngine;
using System.Collections;

public class NinjaMovementCO : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerShootingCO playerShooting;
                     private float rotationSpeed = 5f;

    private Transform closestEnemy;

  

    private Vector3 movement;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerShooting = GetComponent<PlayerShootingCO>();
        animator.Play("Idle");
    }

    void Update()
    {
        FindClosestEnemy();

        if (!playerShooting.isShootingOn)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Hareket yönünü karakterin bakýþ açýsýna göre hesapla
            movement = transform.TransformDirection(new Vector3(moveX, 0f, moveZ)).normalized;


            if (movement.magnitude > 0)
            {
                if (moveZ < 0)
                {
                    animator.Play("RunBack");
                }
                else
                {
                    animator.Play("RunForward");
                }
            }
        }
        else
        {
            movement = Vector3.zero;
        }
    }


    void FixedUpdate()
    {
        RotateScreen();
       


        if (!playerShooting.isShootingOn)
        {
            MovePlayer();
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void MovePlayer()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);      
    }

  

    void RotateScreen()
    {

        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(0, mouseX, 0); // Yalnýzca yatay rotasyonu güncellemek için Y ekseninde döndürme
        }
    }
   

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        closestEnemy = nearestEnemy;
    }
}

