using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    public GameObject panelGameOver;
    public GameObject panelInicio;

    public bool enJuego;

    public AudioSource audioSource;
    //public AudioClip pointClip;
    //public AudioResource pointResource;

    private void Start()
    {
        scoreText.text = score.ToString();
        bestScoreText.text = "Best Score:" + "\n" + PlayerPrefs.GetInt("BestScore", 0);
        panelInicio.SetActive(true);
        enJuego = false;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoint()
    {
        score++;
        PlayPointSound();
        Debug.Log("Tu puntuación actual es:" + score);

        if (score > PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        ActualizarPuntosUI();
    }

    void ActualizarPuntosUI()
    {
        scoreText.text = score.ToString();
        bestScoreText.text = "Best Score:" + "\n" + PlayerPrefs.GetInt("BestScore", 0);
    }

    private void Update()
    {
        if (scoreText == null || bestScoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            bestScoreText = GameObject.Find("BestScoreText").GetComponent<TextMeshProUGUI>();
        }
        if (panelGameOver == null)
            panelGameOver = GameObject.Find("PanelGameOver");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enJuego)
            {
                ReiniciarJuego();
            }
            else
            {
                exitGame();
            }
        }
    }

    public void EmpezarJuego()
    {
        panelInicio.SetActive(false);
        GameObject.Find("Player").GetComponent<PlayerController>().inGame = true;
        enJuego = true;
    }

    public void ReiniciarJuego()
    {
        score = 0;
        ActualizarPuntosUI();
        //Debug.Log("Puntuación reiniciada.");
        Time.timeScale = 1f;
        enJuego = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reinicia la escena actual
        panelGameOver.SetActive(false);
        panelInicio.SetActive(true);
    }

    public void MostrarGameOver()
    {
        panelGameOver.SetActive(true);
        enJuego = false;
        Time.timeScale = 0f;
    }

    public void PlayPointSound() 
    {
        Debug.Log("Playing point sound");
        audioSource.Play();
    }

    public void exitGame()
    {
        //salir/cerrar del juego al presionar solo en windows esc
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
