using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_Text TextoRecord;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        int record = PlayerPrefs.GetInt("MaxPuntuacion", 0);
        TextoRecord.text = "Puntuación máxima: " + record.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CargarJuego();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            TextoRecord.text = "Récord: 0";
        }
    }

    public void CargarJuego()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Salir()
    {
        Application.Quit();
    }
}