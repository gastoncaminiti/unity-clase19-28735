using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Text textGemBlue;
    [SerializeField] private Text textGemGreen;
    [SerializeField] private Text textGemPink;
    [SerializeField] private Text textHP;
    [SerializeField] private TextMeshProUGUI textGameOver;

    [SerializeField] private GameObject planelGem;

    // Update is called once per frame

    private void Awake() {
        //PlayerCollision.OnDeath += EnableGameOverUI;
        PlayerEvent.onDeath  += EnableGameOverUI;
        PlayerCollision.OnLivesChange += UpdateHPUI;
    }
    
    private void Start()
    {
      
    }

    void Update()
    {
        //ACTUALIZAR LA INTERFAZ\
        UpdateGemUI();
    }

    private void UpdateGemUI()
    {
        int[] GemCounts = GameManager.instance.gemQuantity;
        textGemBlue.text = "x " + GemCounts[0];
        textGemGreen.text = "x " + GemCounts[1];
        textGemPink.text = "x " + GemCounts[2];

    }

    public void TooglePanel()
    {
        //RESPUESTA AL PRESIONAR EL BOTTON
        planelGem.SetActive(!planelGem.activeSelf);
    }

    public void EnableGameOverUI()
    {
        textGameOver.gameObject.SetActive(true);
    }

    public void UpdateHPUI(int hp)
    {
        textHP.text = "x " + hp;
    }
}
