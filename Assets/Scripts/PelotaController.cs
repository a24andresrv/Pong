using UnityEngine;
using System.Collections;

public class PelotaController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float force = 1f;
    [SerializeField] float delay;
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioClip sfxPaddel;  // Sonido al chocar con la pala
    [SerializeField] AudioClip sfxWall;
    [SerializeField] AudioClip sfxGol;
    AudioSource sfx;  // Componente AudioSource
    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;
    // Declaramos dos constantes con las posiciones y máximas y mínimas.
    const float MAX_Y = 2.5f;
    const float MIN_Y = -2.5f;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        int direccionX = Random.Range(0, 2) == 0 ? -1 : 1;
        StartCoroutine(LanzarPelota(direccionX));
    }

    IEnumerator LanzarPelota(int direccionX)
    {
        yield return new WaitForSeconds(delay);
        // Calculamos la posición vertical de forma aleatoria.
        float posY = Random.Range(MIN_Y, MAX_Y);
        transform.position = new Vector3(0, posY, 0);

        // Definimos el ángulo en radianes usando Range, especificando el mínimo y máximo.
        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angulo) * direccionX;

        // Determinamos si nos movemos hacia la derecha o izquierda.
        // Si el valor devuelto es 0, la dirección en Y será negativa; si es 1, será positiva.
        int direccionY = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Mathf.Sin(angulo) * direccionY;

        Vector2 impulso = new Vector2(x, y);
        // Resetear la velocidad lineal de la pelota
        // rb.velocity = new Vector2(0, 0);
        rb.linearVelocity = Vector2.zero;
        // Ahora podemos aplicar el impulso
        rb.AddForce(impulso * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Pa1"))
        {
            sfx.clip = sfxPaddel;
            sfx.Play();
            Debug.Log("Colisión con Pala 1!");
        }
        else if (tag.Equals("Pa2"))
        {
            sfx.clip = sfxPaddel;
            sfx.Play();
            Debug.Log("Colisión con Pala 2!");
        }
        else if (tag.Equals("Límite"))
        {
            sfx.clip = sfxWall;
            sfx.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gol en " + other.tag + "!!");

        if (other.tag == "Portería Esquerda")
        {
            // Lanzaremos la pelota hacia la derecha
            gameManager.AddPointP1();
            StartCoroutine(LanzarPelota(1));
            sfx.clip = sfxGol;
            sfx.Play();
        }
        else if (other.tag == "Portería Dereita")
        {
            // Lanzaremos la pelota hacia la izquierda
            gameManager.AddPointP2();
            StartCoroutine(LanzarPelota(-1));
            sfx.clip = sfxGol;
            sfx.Play();
        }
    }


}