using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitParent : MonoBehaviour
{
    protected Health health;
    protected Animator anim;
    protected virtual void Awake() {
        health = this.GetComponent<Health>();
        anim = this.GetComponent<Animator>();
    }
    protected virtual void OnEnable() 
    {   
        health.DeathInfo += OnDeath;
    }
    protected virtual void OnDisable() 
    {
        health.DeathInfo -= OnDeath;
    }
    protected virtual void OnDeath()
    {
        this.gameObject.SetActive(false);
    }
}
