using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject infoMenu;
    [SerializeField]
    GameObject menuBase;

    public void Info()
    {
        infoMenu.SetActive(true);
        menuBase.SetActive(false);
    }

    public void Play()
    {
        Application.LoadLevel("MathGame");
    }

    public void Back()
    {
        infoMenu.SetActive(false);
        menuBase.SetActive(true);
    }
    public void Classifica()
    {
        Application.LoadLevel("Ranking");
    }
}
