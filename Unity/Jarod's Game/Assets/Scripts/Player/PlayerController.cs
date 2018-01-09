using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    private GameObject GameFunctions;
    private GameObject GameController;
    private GameObject laser;
    public GameObject explosion;
    private Text messageText;
    private Text healthText;

    public float health=100;
    public float playerJumpStrength = 1;
    private float nextJump=0;
    public float jumpRecovery = 0.75f;
    public float nextFire = 0;
    public float loadedAmmo;
    public float totalAmmo;
    public float totalGrenades = 1;
    public float movementSpeed=8;
    public float rotationSpeed = 5;

    public float yRotation = 90;
    public float xRotation = 0;


    public WeaponType currentWeapon;

    // Sets the current weapon of the player to none since he starts without one and finds the GameFunctions Object
    void Start () {
        currentWeapon = WeaponType.None;
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        laser = GameObject.FindGameObjectWithTag("Laser");
        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
        healthText = GameObject.Find("Canvas/HealthText").GetComponent<Text>();
        loadedAmmo = 0;
        totalAmmo = 0;
    }

    // every frame to sets rotation and if the player has a weapon checks if they want to shoot it
    void Update()
    {
        LaserOnOff();
        GameFunctions.GetComponent<GameFunctions>().SetPlayerRotation(this.gameObject, rotationSpeed, ref yRotation,ref xRotation);
        CheckWeaponStuff();
        CheckDeath();
        CheckThrowGrenade();
        CheckConstrants();
    }

    //Called each frame before physics stuff is calculated
    //Checks for base movement and checks if the player wants to freeze their movement or jump
    void FixedUpdate()
    {
        GameFunctions.GetComponent<GameFunctions>().PlayerMovement(this.gameObject, movementSpeed, yRotation);

        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y,0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > nextJump)
            {
                GameFunctions.GetComponent<GameFunctions>().Jump(this.gameObject, playerJumpStrength);
                nextJump = Time.time + jumpRecovery;
            }
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            healthText.text = "Health: 0";
            GameFunctions.GetComponent<GameFunctions>().DestroyBall(this.gameObject, explosion);
            GameFunctions.GetComponent<GameFunctions>().PlayerDead();
        }
    }

    private void CheckWeaponStuff()
    {
        if (currentWeapon != WeaponType.None && currentWeapon!=WeaponType.Sword && currentWeapon != WeaponType.Axe)
        {

            CheckAmmoAndFire();
            Reload();

        }

    }

    private void CheckAmmoAndFire()
    {
        if (loadedAmmo != 0)
        {
            GameFunctions.GetComponent<GameFunctions>().CheckFireBulletAndAct(currentWeapon, ref loadedAmmo, ref nextFire, GameFunctions.GetComponent<GameFunctions>().weaponHandSpot, yRotation,xRotation);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                transform.GetChild(0).GetComponent<AudioSource>().Play();
            }
            messageText.text = "Out of Ammo, Press 'R' to reload";
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            nextFire = Time.time + 1.5f;
            GetComponent<AudioSource>().Play();
            if (totalAmmo < (float)currentWeapon)
            {
                loadedAmmo = totalAmmo;
                totalAmmo = 0;
            }
            else
            {
                totalAmmo -= ((float)currentWeapon - loadedAmmo);
                loadedAmmo = (float)currentWeapon;
            }
            GameFunctions.GetComponent<GameFunctions>().UpdateAmmoText();
            messageText.text = "";
        }
    }

    private void LaserOnOff()
    {
        if (currentWeapon == WeaponType.None || currentWeapon==WeaponType.Sword || currentWeapon == WeaponType.Axe)
        {
            laser.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            laser.GetComponent<LineRenderer>().enabled = true;
        }
    }

    private void CheckThrowGrenade()
    {
        if (Input.GetKeyDown(KeyCode.G) && totalGrenades>0)
        {
            GameFunctions.GetComponent<GameFunctions>().ThrowGrenade(GameFunctions.GetComponent<GameFunctions>().weaponHandSpot,yRotation,xRotation);
            totalGrenades--;
            GameFunctions.GetComponent<GameFunctions>().UpdateAmmoText();
        }
    }
    
    private void CheckConstrants()
    {
        if (transform.position.x>GameController.GetComponent<GameController>().xPlayerConstrant)
        {
            transform.position = new Vector3(GameController.GetComponent<GameController>().xPlayerConstrant, transform.position.y,transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (transform.position.x < -GameController.GetComponent<GameController>().xPlayerConstrant)
        {
            transform.position = new Vector3(-GameController.GetComponent<GameController>().xPlayerConstrant, transform.position.y, transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (transform.position.z > GameController.GetComponent<GameController>().zPlayerConstrant)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,GameController.GetComponent<GameController>().zPlayerConstrant);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (transform.position.z < -GameController.GetComponent<GameController>().zPlayerConstrant)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -GameController.GetComponent<GameController>().zPlayerConstrant);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
