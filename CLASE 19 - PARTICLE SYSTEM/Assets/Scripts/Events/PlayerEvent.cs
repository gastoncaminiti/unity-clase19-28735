using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEvent 
{
    public static event Action onDeath;

    public static void OnDeath()
    {
        onDeath?.Invoke();
    }
}
