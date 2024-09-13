using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerController : MonoBehaviour
{
    public float jumpForce = 50f; // Z�plama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;
    public int health = 3; // Can say�s�

    // Can g�stergeleri i�in referanslar
    private Image can2;
    private Image can1;
    private Image can;

    // ScoreManager referans�
    private ScoreManager scoreManager;

    // GameManager referans�
    private GameManager gameManager;

    private bool isGameOver = false; // Oyun bitti mi kontrol�

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Canvas �zerindeki can g�rsellerini bulup referanslar� atama
        can2 = GameObject.Find("Can2").GetComponent<Image>();
        can1 = GameObject.Find("Can1").GetComponent<Image>();
        can = GameObject.Find("Can").GetComponent<Image>();

        // ScoreManager'� bulma
        scoreManager = FindObjectOfType<ScoreManager>();

        // GameManager'� bulma
        gameManager = FindObjectOfType<GameManager>();

        UpdateHealthUI(); // Oyunun ba��nda can g�stergelerini g�ncelle
    }

    void Update()
    {
        if (isGameOver) return; // Oyun bittiyse z�plama engellenir

        // Bilgisayarda test ederken Space tu�u ile z�plamay� test edebiliriz
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Telefon dokunma giri�ini kontrol etme
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        // Skoru art�rma
        if (scoreManager != null)
        {
            scoreManager.IncreaseScore();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void TakeDamage()
    {
        if (isGameOver) return;

        health--;
        UpdateHealthUI();
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            isGameOver = true;
            // Oyun bitti�inde GameManager'daki GameOver fonksiyonunu �a��r
            if (scoreManager != null && gameManager != null)
            {
                gameManager.GameOver(scoreManager.GetScore());
            }
        }
    }

    void UpdateHealthUI()
    {
        // Can g�stergelerini g�ncelle
        if (health < 3)
        {
            can2.enabled = false; // 3 can�n g�rselini gizle
        }
        if (health < 2)
        {
            can1.enabled = false; // 2 can�n g�rselini gizle
        }
        if (health < 1)
        {
            can.enabled = false; // 1 can�n g�rselini gizle
        }
    }
}
