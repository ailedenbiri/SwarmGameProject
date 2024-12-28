using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShootingCO : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPoint;
    public Animator animator;

    [HideInInspector]
    public bool isShootingOn;

    private void Awake()
    {
        isShootingOn = false;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerShoot();
    }

    void PlayerShoot()
    {
        if (Input.GetMouseButtonDown(0) && !isShootingOn)
        {
            animator.Play("Shoot");
            isShootingOn = true;
            StartCoroutine(SpawnBullets());
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShootingOn = false;
        }
    }

    private void CreateBullet()
    {

        GameObject bulletGO = Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
        Rigidbody bulletRb = bulletGO.GetComponent<Rigidbody>();

        Debug.LogError("Bullet Direction: " + transform.forward);

        bulletRb.linearVelocity = transform.forward * 15;

        Destroy(bulletGO, 2f);
    }

    IEnumerator SpawnBullets()
    {
        while (isShootingOn)
        {
            CreateBullet();
            yield return new WaitForSeconds(0.5f);
        }
    }
}

