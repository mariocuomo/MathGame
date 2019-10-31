using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormUserManager : MonoBehaviour
{
    [SerializeField]
    GameObject SignInMenu;
    [SerializeField]
    GameObject SignUpMenu;
       
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SignUp()
    {
        SignInMenu.SetActive(false);
        SignUpMenu.SetActive(true);
    }

    public void SignIn()
    {
        SignInMenu.SetActive(true);
        SignUpMenu.SetActive(false);
    }
}
