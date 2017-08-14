using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    //declare varialbes

    public GameObject bloodSplatter;

    public float Health;
    public int EXPReward;
    public float zombieDamage;
    private float timer;
    public float attackcd;
    private float timerSound;
    private float rndsound;
    public float growlchancepersecond;
    public float growlCD;
    public float buildingsoundhitfreq;

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        //zombie sound
        timerSound -= Time.deltaTime;
        if (timerSound < 0)
        {
            rndsound = Random.Range(0, 300);
            if (rndsound <= growlchancepersecond)
            {
                SoundManageScript.instance.playZombieGrowl(this.transform);
            }
            timerSound = growlCD;
        }

    }

    void Damage(float _Damage)
    {   //reduce damage and kill if required
        Health -= _Damage;

        Vector3 angle = transform.rotation.eulerAngles;

        Quaternion newAngle = Quaternion.Euler(angle.x, angle.y, angle.z + 90);

        GameObject newBloodSplatter = Instantiate(bloodSplatter, transform.position, newAngle) as GameObject;

        if (Health <= 0)
        {

            this.gameObject.GetComponent<FlockUnit>().Leader.GetComponent<AllUnits>().units.Remove(this.gameObject);

            this.gameObject.GetComponent<Unit>().zombiedead = true;

            //drop loot chance
            DropSpawnScript.instance.Drop(this.transform);
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
            if (collision.gameObject.tag == "Building")
            {
                collision.gameObject.SendMessage("BuildingTakeDamage", 0.2f);
                int rnd = Random.Range(0, 100);
                if (rnd < buildingsoundhitfreq)
                {
                    SoundManageScript.instance.playBuildingAttacked(this.transform);
                }
                
            }
                
        }
    }
}
