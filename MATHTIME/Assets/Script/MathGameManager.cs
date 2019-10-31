using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data.SqlClient;
using System.Text;
using System;

public class MathGameManager : MonoBehaviour
{
    float timer;
    Slider slider;
    Text seconds;

    Text espressione;
    int primo_fattore;
    int secondo_fattore;

    int numero;
    Text result;

    int punti;
    [SerializeField]
    GameObject riepilogo;
    [SerializeField]
    Text riepilogoText;


    SqlConnectionStringBuilder builder;
    SqlConnection connection;
    SqlCommand command;
    SqlDataReader reader;

    void Start()
    {
        timer = 59;
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        seconds = GameObject.Find("second").GetComponent<Text>();

        espressione = GameObject.Find("espressione").GetComponent<Text>();

        numero = 0;
        result = GameObject.Find("result").GetComponent<Text>();

        punti = 0;

        AggiornaTabellina();
        builder = new SqlConnectionStringBuilder();
        builder.DataSource = "xxxxxx";
        builder.UserID = "xxxxx";
        builder.Password = "xxxxx";
        builder.InitialCatalog = "xxxxx";
        connection = new SqlConnection(builder.ConnectionString);


    }

    // Update is called once per frame
    void Update()
    {
        seconds.text = timer.ToString("00.00")+"s";
        timer -= Time.deltaTime;
        slider.value = timer;

        if (timer < 0)
            VisualizzaRiepilogo();
    }

    public void PremiTasto()
    {
        string numeroString = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        
        if (numeroString == "C")
            numero = 0;
        else
        {
            string temp = result.text + numeroString;
            numero = System.Convert.ToInt32(temp);
            VerificaCorrettezza();
        }
        result.text = numero.ToString();

    }

    void VerificaCorrettezza() {
        if (numero == primo_fattore * secondo_fattore)
        {
            punti++;
            AggiornaTabellina();
        }
    }

    void AggiornaTabellina()
    {
        primo_fattore = UnityEngine.Random.Range(0, 10);
        secondo_fattore = UnityEngine.Random.Range(0, 10);
        espressione.text = primo_fattore + "x" + secondo_fattore;
        result.text = "";
        numero = 0;
    }

    void VisualizzaRiepilogo()
    {
        riepilogo.SetActive(true);
        riepilogoText.text = "Il tuo punteggio è \n " + punti;
        Aggiorna();
    }

    public void TornaIndietro()
    {
        Application.LoadLevel("MainMenu");
    }

    public void Aggiorna()
    {
        int puntiDB = AcquisisciPunti();

        if (punti > puntiDB)
        {
            connection.Open();
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE utenti SET punti = " + punti + "  WHERE nickname = '" + PlayerPrefs.GetString("nickname") + "'");
            String sql = sb.ToString();
            command = new SqlCommand(sql, connection);
            reader = command.ExecuteReader();
            connection.Close();
        }
    }

    public int AcquisisciPunti()
    {
        int p=0;
        connection.Open();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT passwordd fROM utenti WHERE nickname = '" + PlayerPrefs.GetString("nickname") + "'");
        String sql = sb.ToString();
        command = new SqlCommand(sql, connection);
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            p = reader.GetInt32(0);
            
        }
        connection.Close();
        return p;
    }

}
