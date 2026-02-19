using UnityEngine;
using TMPro; //Pqra que recoñeza "TMP Text"
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int p1Score;
    int p2Score;
    bool running = false;

    [SerializeField] TMP_Text txtP1Score;
    [SerializeField] TMP_Text txtP2Score;
    [SerializeField] GameObject pelota;

    public void AddPointP1()
    {
        p1Score++;
        txtP1Score.text = p1Score.ToString();
        GuardarRecord(p1Score);
    }
    public void AddPointP2()
    {
        p2Score++;
        txtP2Score.text = p2Score.ToString();
        GuardarRecord(p2Score);
    }

    void GuardarRecord(int puntuacion)
    {
        int recordActual = PlayerPrefs.GetInt("MaxPuntuacion", 0);

        if (puntuacion > recordActual)
        {
            PlayerPrefs.SetInt("MaxPuntuacion", puntuacion);
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (!running && Input.GetKeyDown(KeyCode.Space))
        {
            // Activamos la pelota 
            pelota.SetActive(true);
            // Indicamos que el juego ha comenzado
            running = true;
        }

        // Si se pulsa la tecla Escape, salimos de la aplicación 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        CargarMenu();
    }
    }

    public void CargarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
