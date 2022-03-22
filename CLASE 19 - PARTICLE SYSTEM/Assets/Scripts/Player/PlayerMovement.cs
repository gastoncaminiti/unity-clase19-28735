using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] GameObject shootPoint;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Text playerNameLabel;
    [SerializeField] private ParticleSystem stepVFX;

    private GameObject parentBullets;
    private AudioSource audioPlayer;
    private Rigidbody rbPlayer;
    private InventoryManager mgInventory;
    private SavepointsManager svManager;

    private float cameraAxisX = 0f;
    private float timePass = 0;
    private float rotateSensitivity = 0.4f;
    private bool isJump, isBack, isForward, isStatic, canJump;
    private bool canShoot = true;

    void Start()
    {
        parentBullets = GameObject.Find("DinamycBullets");
        audioPlayer = GetComponent<AudioSource>();
        rbPlayer = GetComponent<Rigidbody>();
        mgInventory = GetComponent<InventoryManager>();
        svManager = FindObjectOfType<SavepointsManager>();
        if (svManager != null)
        {
            transform.position = svManager.GetSavePoint(GameManager.instance.lastSP).position;
        }
        LoadProfile();
        PlayerEvent.onDeath += GameOverBehaviour;
    }

    public void LoadProfile()
    {
        if (ProfileManager.instance != null)
        {
            playerNameLabel.text = ProfileManager.instance.GetPlayerName();
            playerNameLabel.enabled = ProfileManager.instance.GetVisibleName();
            rotateSensitivity = ProfileManager.instance.GetMouseSensitivity();
        }
        else
        {
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
        gem.transform.position = transform.position + (Vector3.forward * 2f);
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
        bool isLimit = (playerSpeed > playerData.SpeedLimit);

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
            rbPlayer.AddForce(Vector3.up * playerData.SpeedJump, ForceMode.Impulse);
            isJump = false;
        }

        if (!isBack && !isForward && canJump && !isJump)
        {
            Vector3 stopVelocity = new Vector3(1f, rbPlayer.velocity.y, 1f);
            rbPlayer.velocity = stopVelocity;
        }

    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            audioPlayer.PlayOneShot(playerData.JumpSound, 0.5f);
            isJump = true;
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

        if (timePass > playerData.Cooldown)
        {
            timePass = 0;
            canShoot = true;
            playerAnimator.SetBool("isShoot", !canShoot);
        }
    }

    private void DelayShoot()
    {
        audioPlayer.PlayOneShot(playerData.ShootSound, 0.5f);
        GameObject newBullet = Instantiate(playerData.BulletPrefab, shootPoint.transform.position, transform.rotation);
        newBullet.transform.parent = parentBullets.transform;
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerAnimator.SetBool("isRun", true);
            isForward = true;
            EnableVFXStep();
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerAnimator.SetBool("isRun", true);
            isBack = true;
            EnableVFXStep();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.SetBool("isRun", false);
            isForward = false;
            DiseableVFXStep();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnimator.SetBool("isRun", false);
            isBack = false;
            DiseableVFXStep();
        }
    }

    private void EnableVFXStep()
    {
        if (!stepVFX.isPlaying)
        {
            stepVFX.Play();
        }
    }


    private void DiseableVFXStep()
    {
        if (stepVFX.isPlaying)
        {
            stepVFX.Stop();
        }
    }

    private void MoveRelativeForce(Vector3 direction)
    {
        rbPlayer.AddRelativeForce(playerData.Speed * direction, ForceMode.VelocityChange);
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

    public InventoryManager GetPlayerInventory()
    {
        return mgInventory;
    }

    public void GameOverBehaviour()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        playerAnimator.SetBool("isRun", false);
        this.enabled = false;
    }
}
