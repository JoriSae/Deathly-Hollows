using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //variables

    //weapon variables
    public GameObject ArrowGO;
    public float AttackCooldown;
    private float AttackTimer;
    private bool SwordSwinging = false;
    public int SlashSpeed;
    private float slashtimer;
    public int WeaponSelected;

    //exp varialbes
    public int Level;
    public int Exp;
    public int NextLevelExp;
    public int ExpExpo;

    public GameObject head;
    public GameObject SwordGO;

    public static Player instance;

    // awake and declare singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        AttackTimer = AttackCooldown;
        SwordGO.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        AttackFunction();
        swordswing();
        Levelupfunction();
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
                
                if (WeaponSelected == 1)//swing sword / sword selected
                {
                    SwordGO.transform.rotation = head.transform.rotation;
                    SwordGO.transform.Rotate(Vector3.forward * 280);
                    SwordSwinging = true;
                }
                if (WeaponSelected == 0)//shootbow
                {
                    Instantiate(ArrowGO, transform.position, head.transform.rotation);
                }
                             
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

    public void ChangeWeapon(int wepsel)
    {
        if (wepsel == 0)
            WeaponSelected = 0;
        if (wepsel == 1)
            WeaponSelected = 1;
    }


    //resource collection code area
    public void OnTriggerStay2D(Collider2D collision)
    {
        //you can place more resources in this function if you need more resources to be able to be picked up
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "Resource1")
            {
                Debug.Log("Collected Resource1");
                //place collection resource code here
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Resource2")
            {
                Debug.Log("Collected Resource2");
                //place collection resource code here
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Resource3")
            {
                Debug.Log("Collected Resource3");
                //place collection resource code here
                Destroy(collision.gameObject);
            }
        }
        
    }

    //EXP and level up Area
    public void Levelupfunction()
    {
        if (Exp >= NextLevelExp)
        {
            NextLevelExp += ExpExpo * Level;
            Level += 1;
        }      
    }

}

