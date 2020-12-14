using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Drive : UnitParent {

 	public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    public GameObject bullet;
    public GameManager gameManager;
    public GameObject turret;
    float fireRate = 0.5f;
    float currentFireRate = 0;
    protected override void OnDeath()
    {
        StartCoroutine(gameManager.RestartGame());
        base.OnDeath();
    }

    void Update() {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if(Input.GetKeyDown(KeyCode.Space) && currentFireRate <= 0)
        {
            Fire();
            currentFireRate = fireRate;
        }
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime;
        }
	}
    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        Physics.IgnoreCollision(b.GetComponent<Collider>(), GetComponent<Collider>());
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }
}
