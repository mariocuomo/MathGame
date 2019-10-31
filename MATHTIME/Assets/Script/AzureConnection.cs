using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Text;
using System;
using UnityEngine.UI;

public class AzureConnection : MonoBehaviour
{
    SqlConnectionStringBuilder builder;
    SqlConnection connection;
    SqlCommand command;
    SqlDataReader reader;

    Text nick;
    Text pass;
    Text error;

    // Start is called before the first frame update
    void Start()
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "xxxxxx";
        builder.UserID = "xxxxx";
        builder.Password = "xxxxx";
        builder.InitialCatalog = "xxxxx";
        connection = new SqlConnection(builder.ConnectionString);

        error = GameObject.Find("error").GetComponent<Text>();
        nick = GameObject.Find("NickText").GetComponent<Text>();
        pass = GameObject.Find("PasText").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Presente()
    {
        connection.Open();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT passwordd fROM utenti WHERE nickname = '" + nick.text + "'");
        String sql = sb.ToString();
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();
        bool esiste = false;
        while (reader.Read())
        {
            esiste = true;
        }
        connection.Close();
        return esiste;
    }

    public void Inserisci()
    {
        if (Presente() == false)
        {
            connection.Open();
            StringBuilder sb = new StringBuilder();
            String query = "INSERT INTO USERS (nickname, passwordd) VALUES ('" + nick.text + "', '" + pass.text + "')";
            sb.Append(query);
            String sql = sb.ToString();
            command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
            PlayerPrefs.SetInt("Autenticato", 1);
            PlayerPrefs.SetString("nickname", nick.text);
            Application.LoadLevel("MainMenu");
        }
        else
        {
            error.text= "l’utente è già registrato";
        }
    }

    public void Entra()
    {

        if (Presente() == false)
           error.text = "l’utente non è registrato ";
        else
        {
            connection.Open();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT passwordd fROM utenti WHERE nickname = '" + nick.text + "'");
            String sql = sb.ToString();
            command = new SqlCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string passwordDB = reader.GetString(0);
                if (passwordDB == pass.text)
                {
                    PlayerPrefs.SetInt("Autenticato", 1);
                    PlayerPrefs.SetString("nickname", nick.text);
                    Application.LoadLevel("MainMenu");
                }
                else
                    error.text = "la password non è corretta";
            }
            connection.Close();
        }
    }
}
