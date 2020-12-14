using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    public delegate void OnSendHealthInfo();
    public OnSendHealthInfo SendHealthInfo;
    public OnSendHealthInfo DeathInfo;
    public float currentHp = 100;
    float maxHp = 100;
    public float MaxHp {get{return maxHp;}}

    public void ReduceHp(int damage)
    {
        currentHp -= damage;
        SendHealthInfo?.Invoke();

        if(currentHp <= 0)
        {
            currentHp = 0;
            //Destroy(this.gameObject);
            DeathInfo?.Invoke();
        }    
    }
}
