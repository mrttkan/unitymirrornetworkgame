using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerController : MonoBehaviour
{
    public float jumpForce = 50f; // Zýplama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;
    public int health = 3; // Can sayýsý

    // Can göstergeleri için referanslar
    private Image can2;
    private Image can1;
    private Image can;

    // ScoreManager referansý
    private ScoreManager scoreManager;

    // GameManager referansý
    private GameManager gameManager;

    private bool isGameOver = false; // Oyun bitti mi kontrolü

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Canvas üzerindeki can görsellerini bulup referanslarý atama
        can2 = GameObject.Find("Can2").GetComponent<Image>();
        can1 = GameObject.Find("Can1").GetComponent<Image>();
        can = GameObject.Find("Can").GetComponent<Image>();

        // ScoreManager'ý bulma
        scoreManager = FindObjectOfType<ScoreManager>();

        // GameManager'ý bulma
        gameManager = FindObjectOfType<GameManager>();

        UpdateHealthUI(); // Oyunun baþýnda can göstergelerini güncelle
    }

    void Update()
    {
        if (isGameOver) return; // Oyun bittiyse zýplama engellenir

        // Bilgisayarda test ederken Space tuþu ile zýplamayý test edebiliriz
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Telefon dokunma giriþini kontrol etme
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        // Skoru artýrma
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
            // Oyun bittiðinde GameManager'daki GameOver fonksiyonunu çaðýr
            if (scoreManager != null && gameManager != null)
            {
                gameManager.GameOver(scoreManager.GetScore());
            }
        }
    }

    void UpdateHealthUI()
    {
        // Can göstergelerini güncelle
        if (health < 3)
        {
            can2.enabled = false; // 3 canýn görselini gizle
        }
        if (health < 2)
        {
            can1.enabled = false; // 2 canýn görselini gizle
        }
        if (health < 1)
        {
            can.enabled = false; // 1 canýn görselini gizle
        }
    }
}
