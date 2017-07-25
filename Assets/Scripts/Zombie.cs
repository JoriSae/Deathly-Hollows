using System.Collections;
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
            this.gameObject.GetComponent<Unit>().zombiedead = true;
            //remove the zombie
            Destroy(this.gameObject);
           
            //reward the player with EXP
            Player.instance.Exp += EXPReward;
        }
    }

    public void OnTriggerStay(Collider collision)
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
