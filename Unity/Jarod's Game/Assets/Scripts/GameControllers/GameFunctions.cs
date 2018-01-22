using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

//Public Enum that specifies different weapon typess
public enum WeaponType
{
    HandGun=15,
    Rifle=30,
    Shotgun=3,
    RocketLauncher=1,
    Sword=0,
    Axe=-1,
    None=-2
}


public class GameFunctions : MonoBehaviour {


    //GameObjects that may or may not be used in these functions, most should be check and privatized
    public GameObject GameController;
    public GameObject player;
    public GameObject playerCamera;
    public GameObject playerLight;
    public GameObject bullet;
    public GameObject bulletLeft;
    public GameObject bulletRight;
    public GameObject rocket;
    private GameObject bulletSpawnPoint;
    public GameObject weaponHandSpot;
    public GameObject HandGunDrop;
    public GameObject RifleDrop;
    public GameObject ShotgunDrop;
    public GameObject RocketLauncherDrop;
    public GameObject SwordDrop;
    public GameObject AxeDrop;
    public GameObject HandGun;
    public GameObject Rifle;
    public GameObject Shotgun;
    public GameObject RocketLauncher;
    public GameObject Sword;
    public GameObject Axe;
    public GameObject grenade;
    public GameObject explosion;
    public GameObject bulletSound;

    //Text UI elements that are used to display information to the user
    private Text killsText;
    private Text healthText;
    private Text messageText;
    private Text ammoText;

    public Texture enemyHit;

    public Terrain terr;

    //Various variables that are technically settings in game. Global so they are editable in unity. Most are pretty self explanatory. Call 4023679129 if you need help on figuring out what any of them mean
    //Actually this one needs some explaining: This keeps track of the rotation of the player about the yaxis, because it is easier to use my own book-keeping for this sometimes. I do it better
    //TODO: This variable should be put into the Player Controller file and be taken into functions making it universal
    public float xRotationBullet;
    public float handGunFireRate = 0.25f;
    public float rifleFireRate = 0.1f;
    public float shotgunFireRate = 1;
    public float rocketLauncherFireRate = 2;
    public float globalBulletSpeed = 50;
    public float globalRocketSpeed = 30;
    public float globalGrenadeSpeed = 10;
    public float enemyGunRange = 10;
    public float enemyBulletSpeed = 50;


    // Function that runs the first frame that this object, the GameFunctions, exisits, which should be the first frame of the entire game
    void Start () {
        killsText = GameObject.Find("Canvas/NumberOfKillsText").GetComponent<Text>();
        healthText = GameObject.Find("Canvas/HealthText").GetComponent<Text>();
        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
        ammoText = GameObject.Find("Canvas/AmmoText").GetComponent<Text>();
        UpdateAmmoText();
        bulletSpawnPoint = GameObject.FindGameObjectWithTag("BulletSpawnPointHandGun");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        Debug.Log(HandGun.tag+" "+Rifle.tag);
	}
	
	// Update is called once per frame, not used here, but kept because I want it here. its cool
	void Update () {
		
	}

    //Function that checks if the player light is currently on or off by the bool LightOn, it then will check for input and turn on or off accordingly
    //TODO: Switch from using the bool variable to check to using the intensity of the light. that way it can be used with any light, not just player light
    public void CheckLightOnOff(GameObject light, ref bool lightOn, float playerLightIntensity)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (lightOn)
            {
               TurnLightOff(light);
                lightOn = false;
            }
            else
            {
               TurnLightOn(light, playerLightIntensity);
                lightOn = true;
            }
        }
    }

    //Turns light off by changing intensity to 0
    public void TurnLightOff(GameObject light)
    {
        light.GetComponent<Light>().intensity = 0;
    }

    //Turns light on by setting intensity to a given value
    public void TurnLightOn(GameObject light, float intensity)
    {
        light.GetComponent<Light>().intensity = intensity;
    }

    //Function that takes in a Transform and finds its position offset with the Player Object
    //TODO: Make Function take in 2 Transforms and finds the offset between them, this way it will be universal and not reliant on the player
    public Vector3 FindPositionOffset(Transform objectTransform, Transform parentObjectTransform)
    {
        return objectTransform.position - parentObjectTransform.transform.position;
    }

    //Function that takes in a Transform and finds the rotational offset with the Player Object
    //TODO: Make Function take in 2 Transforms and finds the offset between them, this way it will be universal and not reliant on the player
    public Vector3 FindRotationOffset(Transform objectTransform, Transform parentObjectTransform)
    {
        return objectTransform.eulerAngles - parentObjectTransform.transform.eulerAngles;
    }

    //Function that takes in the an object and gets input from the user to move that object
    //Takes input from awsd and arrow keys and moves object by applying forces, this makes the ball roll, pretty sweet, Stolen Straight from Roll-A-Ball
    //Also boosts the speed of movement, by increasing the force applied, when the left shift is held down. Like running in most shooters
    public void PlayerMovement(GameObject player, float movementSpeed, float yRotation)
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        float theta = DegreesToRadians(yRotation);

        Vector3 movement = new Vector3(verticalMovement * Mathf.Cos(theta) + horizontalMovement * Mathf.Sin(theta), 0.0f, verticalMovement * Mathf.Sin(theta) + horizontalMovement * -Mathf.Cos(theta));

        float adjustedMovementSpeed = movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            adjustedMovementSpeed *= 2f;
        }

        player.GetComponent<Rigidbody>().AddForce(movement * adjustedMovementSpeed);
    }

    public void SetHeightRelativeToTerrian(GameObject workingObject)
    {
        float Offest = terr.SampleHeight(workingObject.transform.position);
        workingObject.transform.position = new Vector3(workingObject.transform.position.x, workingObject.transform.position.y + Offest, workingObject.transform.position.z);
    }

    //Function that takes in an object and then gets input from the mouse to rotate the player with the mouse
    //It adds the mouse input only on the X axis to the rotation of the object, this way there is only movement on the horizontal plane
    public void SetPlayerRotation(GameObject player, float rotationSpeed, ref float yRotation, ref float xRotation)
    {
        float mouseRotationX = Input.GetAxis("Mouse X");
        float mouseRotationY = Input.GetAxis("Mouse Y");

        player.transform.eulerAngles += new Vector3(0.0f, mouseRotationX, 0.0f) * rotationSpeed;
        yRotation -= mouseRotationX * rotationSpeed;
        xRotation -= mouseRotationY/2;
        if (xRotation > 45)
        {
            xRotation = 45;
        }else if (xRotation < -45)
        {
            xRotation = -45;
        }

    }

    //Function that takes offsets and an object and rotates it with the player and as the player rotates
    //It does this in the same way as the player rotation function but takes into account how the rotation and offsets will affect the objects position in space and adjusts with trig stuff
    public void SetCircularRotation(Vector3 rotationOffset, Vector3 positionOffset, GameObject workingObject, GameObject parentObject)
    {
        float mouseRotationX = Input.GetAxis("Mouse X");

        if (parentObject.CompareTag("Player")) {
            workingObject.transform.eulerAngles += new Vector3(0.0f, mouseRotationX, 0.0f) * parentObject.GetComponent<PlayerController>().rotationSpeed;
        }else if (parentObject.CompareTag("Enemy"))
        {
            workingObject.transform.eulerAngles += new Vector3(0.0f, mouseRotationX, 0.0f) * parentObject.GetComponent<GunEnemyController>().rotationSpeed;
        }

        float theta = DegreesToRadians(rotationOffset.y - workingObject.transform.eulerAngles.y);

        Vector3 adjustedPositionOffset= new Vector3(-positionOffset.z * Mathf.Sin(theta)+ positionOffset.x * Mathf.Cos(theta), positionOffset.y, positionOffset.z * Mathf.Cos(theta)+ positionOffset.x * Mathf.Sin(theta));

        workingObject.transform.position = parentObject.transform.position + adjustedPositionOffset;
    }

    public void RotationToFaceObject(GameObject workingObject, GameObject targetObject, ref float yRotation, ref float xRotation)
    {
        float diffZ = workingObject.transform.position.z - targetObject.transform.position.z;
        float diffX= workingObject.transform.position.x- targetObject.transform.position.x ;
        yRotation = 175+RadiansToDegrees(Mathf.Atan2(diffX,diffZ));
    }

    public void SetCameraAngle(GameObject workingObject, float xRotation)
    {
        workingObject.transform.eulerAngles = new Vector3(xRotation+35,workingObject.transform.eulerAngles.y,workingObject.transform.eulerAngles.z);
    }


    public void checkRange(GameObject workingObject, GameObject targetObject, ref float nextFire, float fireRate, float yRotation, float xRotation)
    {
        if (GetDistance(workingObject.transform.position, targetObject.transform.position)<enemyGunRange && Time.time>nextFire)
        {
            CreateBullet(bullet, workingObject,yRotation,xRotation,enemyBulletSpeed);
            nextFire = Time.time + fireRate;
        }
    }

    public float GetDistance(Vector3 workingObject, Vector3 targetObject)
    {
        return Mathf.Sqrt(Mathf.Pow(workingObject.x - targetObject.x, 2) + Mathf.Pow(workingObject.z - targetObject.z, 2));
    }

    //Function that takes in the current weapon the player has and checks to make sure its within the fire rate restrction
    //It then checks for different inputs depending on the type of weapon and calls the fireBullet funciton to shoot if conditions are met
    public void CheckFireBulletAndAct(WeaponType currentWeapon, ref float loadedAmmo, ref float nextFire, GameObject weaponHandSpot, float yRotation, float xRotation)
    {
        if (Time.time>nextFire) {
            if (Input.GetKey(KeyCode.Mouse0)&&currentWeapon==WeaponType.Rifle)
            {
                loadedAmmo--;
                UpdateAmmoText();
                FireBullet(currentWeapon, ref nextFire, weaponHandSpot, yRotation, xRotation);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0)&&currentWeapon!=WeaponType.Rifle)
            {
                loadedAmmo--;
                UpdateAmmoText();
                FireBullet(currentWeapon, ref nextFire, weaponHandSpot, yRotation, xRotation);
            }
        }
    }

    //This function Takes in the type of weapon and creates the nessesary bullet instances based on that weapon type
    //It finds the position that it needs to shoot the bullet from by looking at various bullet spawn point objects and their transformations
    //If weapon is shotgun it calls another function to handle how shotguns shoot
    //TODO: This function needs to be universalized so it can be used with enemy weapons. The Bullet spawn point is the main issue. It is kindof broken and needs to be fixed
    public void FireBullet(WeaponType type, ref float nextFire, GameObject weaponHandSpot, float yRotation, float xRotation)
    {
        if (type==WeaponType.HandGun)
        {
            nextFire = Time.time + handGunFireRate;
            CreateBullet(bullet, weaponHandSpot, yRotation, xRotation, globalBulletSpeed);
        } else if (type==WeaponType.Rifle)
        {
            nextFire = Time.time + rifleFireRate;
            CreateBullet(bullet, weaponHandSpot, yRotation, xRotation, globalBulletSpeed);
        }
        else if (type == WeaponType.Shotgun)
        {
            nextFire = Time.time + shotgunFireRate;
            ShotgunShot(weaponHandSpot, Quaternion.identity, yRotation, xRotation);
            ShotgunShot(weaponHandSpot, Quaternion.identity, yRotation, xRotation);
        }else if (type == WeaponType.RocketLauncher)
        {
            nextFire = Time.time + rocketLauncherFireRate;
            CreateBullet(rocket, weaponHandSpot, yRotation, xRotation, globalRocketSpeed);
        }
    }

    //Function that takes in the position of a bullet spawn point and fires 3 different bullets that will go in slightly different directions like shot gun blasts would
    public void ShotgunShot(GameObject weaponHandSpot, Quaternion rotation, float yRotation, float xRotation)
    {

        CreateBullet(bullet, weaponHandSpot, yRotation, xRotation, globalBulletSpeed);
        CreateBullet(bulletLeft, weaponHandSpot, yRotation, xRotation, globalBulletSpeed);
        CreateBullet(bulletRight, weaponHandSpot, yRotation, xRotation, globalBulletSpeed);
    }

    private void CreateBullet(GameObject bullet, GameObject weaponHandSpot, float yRotation, float xRotation, float bulletSpeed)
    {
        GameObject firedBullet = Instantiate(bullet, weaponHandSpot.transform.position, weaponHandSpot.transform.rotation);
        Instantiate(bulletSound, weaponHandSpot.transform);
        FindBulletVelocity(firedBullet, bulletSpeed, yRotation, xRotation);
    }

    //This function takes in a bullet game object and assigns it a velocity vector3 based on the type of bullet.
    //Normal bullets will go straight in the direction the gun is facing
    //Left bullets will go slightly left in trajectory
    //Right Bullets will go slightly right in trajectory
    public void FindBulletVelocity(GameObject bullet, float bulletSpeed, float yRotation, float xRotation)
    {
        float theta = DegreesToRadians(yRotation);
        float theta2 = DegreesToRadians(xRotation);

        if (bullet.CompareTag("BulletLeft"))
        {
             theta = DegreesToRadians(yRotation+Random.Range(0.5f,2.5f));
        }
        else if (bullet.CompareTag("BulletRight"))
        {
             theta = DegreesToRadians(yRotation- Random.Range(0.5f, 2.5f));
        }

        Vector3 speed=new Vector3(bulletSpeed * Mathf.Cos(theta), bulletSpeed * Mathf.Sin(-theta2), bulletSpeed * Mathf.Sin(theta));

        bullet.GetComponent<Rigidbody>().velocity = speed;
    }

    //Function that takes in the current weapon the player has, the positon of the weaponDrop they are picking up, and game objects for all the different weapons
    //It then creates a Weapon drop for the current weapon the player has and destorys the one in the players hand.
    //TODO: To make this universal must find a way to do something other than pass in every concevable weapon type. there has to be a better way
    public void DestroyCurrentCreateDrop(WeaponType currentWeapon, float loadedAmmo, float totalAmmo, Transform weaponDrop)
    {
        if (currentWeapon == WeaponType.HandGun)
        {
            Instantiate(HandGunDrop, weaponDrop.transform.position, weaponDrop.transform.rotation);
            SetDropAttributes(currentWeapon,weaponDrop.transform.position, player.GetComponent<PlayerController>().loadedAmmo, player.GetComponent<PlayerController>().totalAmmo);
        }
        else if (currentWeapon == WeaponType.Rifle)
        {
            Instantiate(RifleDrop, weaponDrop.transform.position, weaponDrop.transform.rotation);
            SetDropAttributes(currentWeapon, weaponDrop.transform.position, player.GetComponent<PlayerController>().loadedAmmo, player.GetComponent<PlayerController>().totalAmmo);
        }
        if (currentWeapon==WeaponType.Shotgun)
        {
            Instantiate(ShotgunDrop, weaponDrop.transform.position, weaponDrop.transform.rotation);
            SetDropAttributes(currentWeapon, weaponDrop.transform.position, player.GetComponent<PlayerController>().loadedAmmo, player.GetComponent<PlayerController>().totalAmmo);
        }
        if (currentWeapon == WeaponType.RocketLauncher)
        {
            Instantiate(RocketLauncherDrop, weaponDrop.transform.position, weaponDrop.transform.rotation);
            SetDropAttributes(currentWeapon, weaponDrop.transform.position, player.GetComponent<PlayerController>().loadedAmmo, player.GetComponent<PlayerController>().totalAmmo);
        }
        if (currentWeapon == WeaponType.Sword)
        {
            Instantiate(SwordDrop, weaponDrop.transform.position, weaponDrop.transform.rotation);
            SetDropAttributes(currentWeapon, weaponDrop.transform.position, player.GetComponent<PlayerController>().loadedAmmo, player.GetComponent<PlayerController>().totalAmmo);
        }
        if (currentWeapon == WeaponType.Axe)
        {
            Instantiate(AxeDrop, weaponDrop.transform.position, weaponDrop.transform.rotation);
            SetDropAttributes(currentWeapon, weaponDrop.transform.position, player.GetComponent<PlayerController>().loadedAmmo, player.GetComponent<PlayerController>().totalAmmo);
        }
        Destroy(GameObject.FindGameObjectWithTag(currentWeapon.ToString()));
    }

    //Function that sets the ammo within a drop equal to the amount of ammo that you had in the weapon you last had
    public void SetDropAttributes(WeaponType currentWeapon, Vector3 position, float loadedAmmo, float totalAmmo)
    {
        Collider[] colliders = Physics.OverlapSphere(position,0.05f);
        if (colliders.Length>1)
        {
            
            foreach (Collider collider in colliders)
            {
                GameObject go = collider.gameObject;
                if (go.CompareTag(currentWeapon.ToString()+"Drop")) {

                    go.GetComponent<WeaponDropController>().loadedAmmo = loadedAmmo;
                    go.GetComponent<WeaponDropController>().totalAmmo = totalAmmo;
                }
            }
        }

    }

    //Function to update the Ammo text UI object 
    public void UpdateAmmoText()
    {
        ammoText.text = player.GetComponent<PlayerController>().loadedAmmo + "/" + player.GetComponent<PlayerController>().totalAmmo+" G:" + player.GetComponent<PlayerController>().totalGrenades;
    }

    //Function to get the amount of ammmo within the weapon drop when you pick it up based on the type of weapon drop you are grabbing
    public void GetAmmoRefs(GameObject go,ref float loadedAmmo, ref float totalAmmo)
    {
        loadedAmmo = go.GetComponent<WeaponDropController>().loadedAmmo;
        totalAmmo = go.GetComponent<WeaponDropController>().totalAmmo;
    }

    //Function that takes in the gameobject for the weapon drop that the player is picking up and the new current weapon type the player just picked up
    //It will then Create the new weapon type in the players hand and destroy the old drop that they picked it up from
    public void DestroyDropAndCreateNewWeapon(GameObject oldDrop, WeaponType currentWeapon)
    {

        if (currentWeapon==WeaponType.HandGun)
        {
            Debug.unityLogger.Log("Made Handgun at : " + weaponHandSpot.transform.position, oldDrop);
            Debug.Log("Player : " +player.transform.position);
            Instantiate(HandGun,weaponHandSpot.transform);
        }
        if (currentWeapon==WeaponType.Rifle)
        {
            Instantiate(Rifle, weaponHandSpot.transform);
        }
        if (currentWeapon == WeaponType.Shotgun)
        {
            Instantiate(Shotgun, weaponHandSpot.transform);
        }
        if (currentWeapon == WeaponType.RocketLauncher)
        {
            Instantiate(RocketLauncher, weaponHandSpot.transform);
        }
        if (currentWeapon == WeaponType.Sword)
        {
            Instantiate(Sword, weaponHandSpot.transform);
        }
        if (currentWeapon == WeaponType.Axe)
        {
            Instantiate(Axe, weaponHandSpot.transform);
        }

        float loadedAmmoRef = 0;
        float totalAmmoRef = 0;
        GetAmmoRefs(oldDrop, ref loadedAmmoRef, ref totalAmmoRef);
        player.GetComponent<PlayerController>().totalAmmo = totalAmmoRef;
        player.GetComponent<PlayerController>().loadedAmmo = loadedAmmoRef;
        Destroy(oldDrop);
        UpdateAmmoText();
    }

    //This is a function that takes in a target object and a chasing object, as well as a chase speed. 
    //It will find what force vector3, relative to the chase speed, that must be applied to the chase object in order to push it towards the target
    public Vector3 FindChaseForce(Transform targetObject, Transform chaseObject, float chaseSpeed)
    {
        Vector3 positionDifference = targetObject.position - chaseObject.position;

        float hypo = Mathf.Sqrt(Mathf.Pow(positionDifference.x, 2) + Mathf.Pow(positionDifference.z, 2));

        Vector3 movement = new Vector3((chaseSpeed * positionDifference.x)/hypo, 0, (chaseSpeed * positionDifference.z)/hypo);

        return movement;
    }

    //Function that takes in object that got hit, a reference to the enemy's health, the explosion gameObject, and a reference to the number of kills
    //It then takes 10 life off the enemy's health and checks if the enemy;s health is at 0. If it is at 0 it calls destory ball on the enemy, kills it and adds 1 to the kills number
    public void DamageObject(GameObject hitObject, float damage)
    {
        if (hitObject.CompareTag("Enemy"))
        {
            hitObject.GetComponent<EnemyController>().health -= damage;
        }else if (hitObject.CompareTag("Player"))
        {
            hitObject.GetComponent<PlayerController>().health -= damage;
            healthText.text = "Health: " + hitObject.GetComponent<PlayerController>().health;
        }else if (hitObject.CompareTag("GunEnemy"))
        {
            hitObject.GetComponent<GunEnemyController>().health -= damage;
        }
    }

    public void changeMaterial(GameObject workingObject, Texture targetTexture)
    {
        workingObject.GetComponent<Renderer>().material.SetTexture("_MainTex", targetTexture);
    }


    //Function that takes in a ball object and an explosion and destorys the ball and creates an explosion so the ball acn go out with a bang
    public void DestroyBall(GameObject ball, GameObject explosion)
    {
        Instantiate(explosion, ball.transform.position, Quaternion.identity);
        Destroy(ball);
    }

    //Function that changes the EndGame bool to true signaling the rest of the game that the fun is over
    public void PlayerDead()
    {
        GameController.GetComponent<GameController>().EndGame = true;
        Time.timeScale = 0;
    }

    //Functin that adds the jumpStrength of the object to the upward velocity of the object
    public void Jump(GameObject jumper, float jumpStrength)
    {
        jumper.GetComponent<Rigidbody>().velocity+=new Vector3(0,jumpStrength,0);
    }

    public void RocketExplode(GameObject rocket, GameObject hitObject, GameObject rocketExplosion, GameObject explosion)
    {      
            DamageObject(hitObject,150);
            Instantiate(rocketExplosion, rocket.transform.position,rocket.transform.rotation);
            Destroy(rocket);       
    }
    
    public void GrenadeExplode(GameObject grenade, GameObject explosion)
    {
        Instantiate(explosion, grenade.transform.position, grenade.transform.rotation);
        Destroy(grenade);
    }

    public void ThrowGrenade(GameObject weaponHandSpot, float yRotation, float xRotation)
    {
        CreateBullet(grenade, weaponHandSpot, yRotation, xRotation, globalGrenadeSpeed);
    }

    public void SwingWeapon(GameObject swingWeapon, float swingSpeed)
    {
        swingWeapon.transform.Rotate(new Vector3(0,0,360)*Time.deltaTime*(1/swingSpeed));
    }

    //Function taht takes degrees and converts them to radians
    public float DegreesToRadians(float val)
    {
        return (Mathf.PI / 180) * val;
    }
    public float RadiansToDegrees(float val)
    {
        return (180/Mathf.PI) * val;
    }
}
