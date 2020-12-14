using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : UnitParent
{
    public GameObject player;
    public GameObject bullet;
    public GameObject turret;
    public GameObject[] waypoints;
    protected override void OnEnable() 
    {
        base.OnEnable();
        health.SendHealthInfo += SetHealthState;
    }
    protected override void OnDisable() 
    {  
        base.OnDisable();
        health.SendHealthInfo -= SetHealthState;
    }
    void SetHealthState()
    {
        if(health.currentHp <= health.MaxHp * 0.2f)
            anim.SetBool("flee", true);
    }
    public GameObject GetPlayer()
    {
        return player;
    }
    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }
    public void StopFiring()
    {
        CancelInvoke("Fire");
    }
    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        Physics.IgnoreCollision(b.GetComponent<Collider>(), GetComponent<Collider>());
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }
    private void Update() {
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
    }

}
