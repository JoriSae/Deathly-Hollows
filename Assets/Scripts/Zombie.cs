﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    //declare varialbes

    public float Health;
    public int EXPReward;
    public float zombieDamage;
    private float timer;
    public float attackcd;

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }

    void Damage(float _Damage)
    {   //reduce damage and kill if required
        Health -= _Damage;
        if (Health <= 0)
        {
           
                System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units);
            list.Remove(this.gameObject);
            this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units = list.ToArray();

            this.gameObject.GetComponent<Unit>().zombiedead = true;
            //remove the zombie
            Destroy(this.gameObject);

            //reward the player with EXP
            Player.instance.Exp += EXPReward;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (timer < 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player.instance.Health -= zombieDamage * Time.deltaTime;
                timer = attackcd;
            }
        }
    }
}
