using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    //declare varialbes

    public float Health;
    public int EXPReward;

    void Damage(float _Damage)
    {   //reduce damage and kill if required
        Health -= _Damage;
        if (Health <= 0)
        {
            //remove the zombie
            Destroy(this.gameObject);
            //reward the player with EXP
            Player.instance.Exp += EXPReward;
        }
    }
}
