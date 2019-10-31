using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System.Text;
using System;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    
    SqlConnectionStringBuilder builder;
    SqlConnection connection;
    SqlCommand command;
    SqlDataReader reader;
    Text testo;

    // Start is called before the first frame update
    void Start()
    {
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "xxxxxx";
        builder.UserID = "xxxxx";
        builder.Password = "xxxxx";
        builder.InitialCatalog = "xxxxx";
        connection = new SqlConnection(builder.ConnectionString);
        testo = GameObject.Find("classifica").GetComponent<Text>();
        AcquisisciClassifica();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcquisisciClassifica()
    {

        connection.Open();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT nickname, punti fROM utenti");
        String sql = sb.ToString();
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            testo.text = testo.text + reader.GetString(0) + " " + reader.GetInt32(1)+ "\n";
        }
        connection.Close();

    }

    public void Indietro()
    {
        Application.LoadLevel("MainMenu");
    }

}
