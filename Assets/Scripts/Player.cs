using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float Damage;
    public float AttackCooldown;
    private float AttackTimer;
    private bool SwordSwinging = false;
    public int SlashSpeed;
    private float slashtimer;

    public GameObject head;
    public GameObject SwordGO;

	// Use this for initialization
	void Start () {
        AttackTimer = AttackCooldown;
        SwordGO.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        AttackFunction();
        swordswing();
    }

    void AttackFunction()
    {
        //manages the attack cooldown
        if (AttackTimer > 0)
            AttackTimer -= Time.deltaTime;

        //if attack is ready 
        if (AttackTimer <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                SwordGO.transform.rotation = head.transform.rotation;
                SwordGO.transform.Rotate(Vector3.forward * 280);

               SwordSwinging = true;   
                             
                //if attack is initiated then reset the cooldown
                AttackTimer = AttackCooldown;
            }//end attack key
        }//end attack cooldown
    }//end attack function

    void swordswing()
    {
        if (SwordSwinging == true)
        {
            
            SwordGO.SetActive(true);
            //line up with forward
            
            //slash
            SwordGO.transform.Rotate(Vector3.forward * Time.deltaTime * SlashSpeed);
            slashtimer += Time.deltaTime * SlashSpeed;
            if (slashtimer > 180)
            {
                SwordGO.transform.rotation = new Quaternion(0, 0, 0, 0);
                slashtimer = 0;
                SwordGO.SetActive(false);
                SwordSwinging = false;
            }
        }
    }
}
