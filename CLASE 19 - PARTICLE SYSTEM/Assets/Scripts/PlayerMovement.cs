using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject shootPoint;
    [SerializeField] float cooldown = 2f;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private float timePass = 0;

    [SerializeField] private float speedJump = 1f;

    [SerializeField] private Animator playerAnimator;

    private bool canJump;

    private GameObject parentBullets;
    float cameraAxisX = 0f;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip shootSound;

    private AudioSource audioPlayer;
    private Rigidbody rbPlayer;
    //VARIABLES BOOLEANAS PARA CONTROLAR INPUTS
    private bool isJump, isBack, isForward, isStatic;
    public float speedLimit = 15f;

    private InventoryManager mgInventory;

    [SerializeField] private Text playerNameLabel;
    [SerializeField] private float rotateSensitivity  = 0.4f;
    private SavepointsManager svManager;


    void Start()
    {
        parentBullets = GameObject.Find("DinamycBullets");
        audioPlayer = GetComponent<AudioSource>();
        rbPlayer = GetComponent<Rigidbody>();
        mgInventory = GetComponent<InventoryManager>();
        svManager = FindObjectOfType<SavepointsManager>();
        if(svManager != null){
            transform.position = svManager.GetSavePoint(GameManager.instance.lastSP).position;
        }
        LoadProfile();

        //PlayerCollision.OnDeath += GameOverBehaviour;
        PlayerEvent.onDeath += GameOverBehaviour;
    }

    public void LoadProfile(){
        if(ProfileManager.instance != null){
            playerNameLabel.text = ProfileManager.instance.GetPlayerName();
            playerNameLabel.enabled = ProfileManager.instance.GetVisibleName();
            rotateSensitivity = ProfileManager.instance.GetMouseSensitivity();
        }else{
             playerNameLabel.enabled = false;
        }
    }

    void Update()
    {
        MovePlayer();
        RotatePlaye();
        ShootPlayer();
        JumpPlayer();
        UseItemInventoryOne();
        UseItemInventoryTwo();
        UseItemInventoryThree();

    }

    private void UseItemInventoryOne()
    {
        if (Input.GetKeyDown(KeyCode.G) && mgInventory.InventoryOneHas())
        {
            GameObject gem = mgInventory.GetInventoryOne();
            mgInventory.SeeInventoryOne();
            UseGem(gem);
        }
    }

    private void UseGem(GameObject gem)
    {
        gem.SetActive(true);
        gem.transform.position = transform.position + (Vector3.forward  * 2f);
    }

    private void UseItemInventoryTwo()
    {
        if (Input.GetKeyDown(KeyCode.H) && mgInventory.InventoryTwoHas())
        {
            GameObject gem = mgInventory.GetInventoryTwo();
            mgInventory.SeeInventoryTwo();
            UseGem(gem);
        }
    }

    private void UseItemInventoryThree()
    {
        if (Input.GetKeyDown(KeyCode.J) && mgInventory.InventoryThreeHas())
        {
            GameObject gem = mgInventory.GetInventoryThree("Gem");
            mgInventory.SeeInventoryThree();
            UseGem(gem);
        }
    }


    private void FixedUpdate()
    {
        float playerSpeed = rbPlayer.velocity.magnitude;
        bool isLimit = (playerSpeed > speedLimit);

        if (isForward && !isLimit)
        {
            MoveRelativeForce(Vector3.forward);
        }

        if (isBack && !isLimit)
        {
            MoveRelativeForce(Vector3.back);
        }

        if (isJump)
        {
            rbPlayer.AddForce(Vector3.up * speedJump, ForceMode.Impulse);
            isJump = false;
        }

    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            audioPlayer.PlayOneShot(jumpSound, 0.5f);
            //ACTIVO VARIABLE BOOLEAN AL DETECTAR INPUT.
            isJump = true;
            //transform.Translate(Vector3.up * speedJump);
            //rbPlayer.AddForce(Vector3.up * speedJump, ForceMode.Impulse);
        }
    }

    public void SetJumpStatus(bool status)
    {
        canJump = status;
        playerAnimator.SetBool("isJump", !status);
    }

    private void ShootPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E) && canShoot)
        {
            canShoot = false;
            playerAnimator.SetBool("isShoot", !canShoot);
            Invoke("DelayShoot", 1f);
        }

        if (!canShoot)
        {
            timePass += Time.deltaTime;
        }

        if (timePass > cooldown)
        {
            timePass = 0;
            canShoot = true;
            playerAnimator.SetBool("isShoot", !canShoot);
        }
    }

    private void DelayShoot()
    {
        audioPlayer.PlayOneShot(shootSound, 0.5f);
        GameObject newBullet = Instantiate(bulletPrefab, shootPoint.transform.position, transform.rotation);// PROYECTILES
        newBullet.transform.parent = parentBullets.transform;
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //MovePlayer(Vector3.forward);
            playerAnimator.SetBool("isRun", true);
            isForward = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //MovePlayer(Vector3.back);
            playerAnimator.SetBool("isRun", true);
            isBack = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.SetBool("isRun", false);
            isForward = false;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("isRun", false);
            isBack = false;
        }
    }

    private void MoveRelativeForce(Vector3 direction)
    {
        //transform.Translate(speed * Time.deltaTime * direction);
        rbPlayer.AddRelativeForce(speed * direction, ForceMode.Acceleration);
    }

    private void RotatePlaye()
    {
        //1 UN VALOR PARA ROTAR EN Y
        cameraAxisX += Input.GetAxis("Horizontal");
        //2 UN ANGULO A CALCULAR EN FUNCION DEL VALOR DEL PRIMER PASO
        Quaternion angulo = Quaternion.Euler(0f, cameraAxisX * rotateSensitivity, 0f);
        //3 ROTAR
        transform.localRotation = angulo;
    }

    public InventoryManager GetPlayerInventory(){
        return mgInventory;
    }

    public void GameOverBehaviour(){
        GetComponent<Rigidbody>().isKinematic = true;        
        playerAnimator.SetBool("isRun", false);
        this.enabled = false;
    }
}
