using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score, powerupSpeed, lastSP;

    public enum typeGem {Blue, Green, Red, Black};
    public int[] gemQuantity;

    private void Awake()
    {
        if (instance == null)
        {
           instance = this;
           DontDestroyOnLoad(gameObject);
           score = 0;
           powerupSpeed = 1;
           gemQuantity = new int[Enum.GetNames(typeof(typeGem)).Length];
        }else{
            Destroy(gameObject);
        }
    }

}
