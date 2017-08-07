using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //variables
    public bool leveledUp;
    float levelTimer = 2;

    //weapon variables
    public GameObject ArrowGO;
    public float AttackCooldown;
    private float AttackTimer;
    private bool SwordSwinging = false;
    public int SlashSpeed;
    private float slashtimer;
    public int WeaponSelected;

    //player health variable
    public float Health;
    public float MaxHealth;
    public float Regeneration;

    //exp varialbes
    public int Level;
    public int Exp;
    public int NextLevelExp;
    public int ExpExpo;

    public GameObject head;
    public GameObject SwordGO;
    public Inventory inventory;

    public static Player instance;

    private bool overItem = false;

    public Text pickUpItemText;

    // awake and declare singleton
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        AttackTimer = AttackCooldown;
        SwordGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AttackFunction();
        swordswing();
        Levelupfunction();
        checkhealth();
        regen();
        pickUpItemText.gameObject.SetActive(overItem);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (WeaponSelected == 0)
                ChangeWeapon(1);
            else if (WeaponSelected == 1)
                ChangeWeapon(0);
        }
    }

    void checkhealth()
    {
        if (Health <= 0)
        {
            //check if player is dead
            Debug.Log("Your Dead");
        }
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

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Resource1") || collision.CompareTag("Resource2") || collision.CompareTag("Resource3"))
        {
            overItem = false;
        }
    }

    //resource collection code area
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Resource1") || collision.CompareTag("Resource2") || collision.CompareTag("Resource3"))
        {
            overItem = true;
        }

        //you can place more resources in this function if you need more resources to be able to be picked up
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.gameObject.tag == "Resource1")
            {
                Debug.Log("Collected Resource1");
                inventory.AddItem(collision.GetComponent<AddItem>().itemPrefab);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Resource2")
            {
                Debug.Log("Collected Resource2");
                inventory.AddItem(collision.GetComponent<AddItem>().itemPrefab);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "Resource3")
            {
                Debug.Log("Collected Resource3");
                inventory.AddItem(collision.GetComponent<AddItem>().itemPrefab);
                Destroy(collision.gameObject);
            }
        }

    }

    void regen()
    {
        if (Health < MaxHealth)
        {
            Health += Regeneration * Time.deltaTime;
        }
    }

    //EXP and level up Area
    public void Levelupfunction()
    {
        if (Exp >= NextLevelExp)
        {
            leveledUp = true;
            NextLevelExp += ExpExpo * Level;
            Level += 1;
        }

        if (leveledUp)
        {
            levelTimer -= Time.deltaTime;

            if (levelTimer <= 0)
            {
                levelTimer = 2;
                leveledUp = false;
            }
        }
    }

}

