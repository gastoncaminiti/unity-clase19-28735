using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerCollision : MonoBehaviour
{

    private PlayerMovement pmPlayer;

    [SerializeField] int hitPoints = 3;

    //public static event Action OnDeath;
    public static event Action<int> OnLivesChange;


    private void Start() {
        pmPlayer = GetComponent<PlayerMovement>();
        OnLivesChange?.Invoke(hitPoints);
    }

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(name + " COLISION CON " + other.gameObject.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
           // Debug.Log("GAME OVER");
           hitPoints--;
           OnLivesChange?.Invoke(hitPoints);
           Debug.Log("HP : "+hitPoints);
           if(hitPoints < 1){
               Debug.Log("GAME OVER");
              // OnDeath?.Invoke();  
               PlayerEvent.OnDeath();
               Destroy(this);
               Debug.Log("ENVIAR UNA NOTIFICACION A LOS INTERESADOS QUE ESTOY MUERTO");
           }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<PlayerMovement>().SetJumpStatus(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
       // Debug.Log(name + " EXIT COLISION CON " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<PlayerMovement>().SetJumpStatus(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup")){
           // Debug.Log(name + " Tigger con" + other.gameObject.name);
            Destroy(other.gameObject);
            GameManager.instance.score += 100;
            GameManager.instance.powerupSpeed++;


        }
        
        if (other.gameObject.CompareTag("Savepoint")){
           // Debug.Log(other.name);
            SavepointsManager managerSP = other.transform.parent.GetComponent<SavepointsManager>();
            managerSP.FindSavePoint(other.name);
        }

        if (other.gameObject.CompareTag("Gem")){
            Debug.Log(other.GetComponent<GemType>().typeGem);
            Debug.Log((int)other.GetComponent<GemType>().typeGem);
            GameManager.instance.gemQuantity[(int)other.GetComponent<GemType>().typeGem]++;
            
            /*
            InventoryManager playerInventory = pmPlayer.GetPlayerInventory();
            GameObject gem = other.gameObject;
            gem.SetActive(false);
            playerInventory.AddInventoryOne(gem);
            playerInventory.AddInventoryTwo(gem);
            playerInventory.AddInventoryThree(gem.name, gem);
            Debug.Log("--------- INVETARIO 1 -----------");
            playerInventory.SeeInventoryOne();
            Debug.Log("--------- INVETARIO 2 -----------");
            playerInventory.SeeInventoryTwo();
            Debug.Log("--------- INVETARIO 3 -----------");
            playerInventory.SeeInventoryThree();
            */
        }
    }   
}
