using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    public static ProfileManager instance;
    // Start is called before the first frame update

    [SerializeField] private string playerName;
    [SerializeField] private bool isVisibleName;
    [SerializeField] private float mouseSensitivity;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetVisibleName(true);
            SetPlayerName("Conejo");
            SetMouseSensitivity(0.5f);
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMouseSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity;
    }

    public float GetMouseSensitivity()
    {
        return mouseSensitivity;
    }

    public void SetPlayerName(string newName)
    {
        playerName = newName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetVisibleName(bool newStatus)
    {
        isVisibleName = newStatus;
    }

    public bool GetVisibleName()
    {
        return isVisibleName;
    }

}

