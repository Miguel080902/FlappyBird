using UnityEngine;
using UnityEngine.SceneManagement;

//añadir rb2d obligatorio
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;

    public bool inGame = false;
    public bool volar = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //GameManager.instance.ReiniciarJuego();
        inGame = false;
        volar = false;
        rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            rb.gravityScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inGame)
            {
                Saltar();
            } else
            {
                GameManager.instance.EmpezarJuego();
            }

        }

        animator.SetBool("inGame", inGame);
        animator.SetBool("volar", volar);
    }

    void Saltar()
    {
        //rb.linearVelocity = Vector2.zero;
        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.linearVelocity = Vector2.up * jumpForce;
        volar = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tubo"))
        {
            Debug.Log("Colisionaste con el Tubo");
            GameManager.instance.MostrarGameOver();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); esto es para cargar la escena
            //SceneManager.LoadScene("Prueba");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Punto"))
        {
            Debug.Log("Obtuviste punto");
            GameManager.instance.AddPoint();
        }
    }

    public void desactivarVuelo()
    {
        volar = false;
    }
}
