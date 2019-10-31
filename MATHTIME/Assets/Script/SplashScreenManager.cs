using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{

    float timer;
    void Start()
    {
        timer = 6;
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (PlayerPrefs.GetInt("Autenticato", 0) == 1)
                Application.LoadLevel("MainMenu");
            else
                Application.LoadLevel("FormUser");
        }
    }
}
