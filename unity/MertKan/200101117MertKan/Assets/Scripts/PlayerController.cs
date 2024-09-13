using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    public float jumpForce = 500f; // Zıplama kuvveti
    private Rigidbody2D rb;
    private bool isGrounded;
    [SyncVar] public int health = 3; // Can sayısı

    // Can göstergeleri için referanslar
    private Image can2;
    private Image can1;
    private Image can;

    // ScoreManager referansı
    private ScoreManager scoreManager;

    // OnlineGameManager referansı
    private OnlineGameManager onlineGameManager;

    [SyncVar(hook = nameof(OnColorChanged))] private Color playerColor; // Player rengini sync var olarak tanımlıyoruz

    private bool isGameOver = false; // Oyun bitiş durumu

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (isLocalPlayer)
        {
            can2 = GameObject.Find("Can2").GetComponent<Image>();
            can1 = GameObject.Find("Can1").GetComponent<Image>();
            can = GameObject.Find("Can").GetComponent<Image>();

            scoreManager = FindObjectOfType<ScoreManager>();
            onlineGameManager = FindObjectOfType<OnlineGameManager>();

            UpdateHealthUI(); // Oyunun başında can göstergelerini güncelle

            if (onlineGameManager == null)
            {
                Debug.LogError("OnlineGameManager is not assigned or found in the scene.");
            }
        }

        SetPlayerColor();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Player prefabını doğru pozisyonda spawn et
        Transform spawnPoint = isServer ? GameObject.Find("HostSpawnPoint").transform : GameObject.Find("ClientSpawnPoint").transform;
        transform.position = spawnPoint.position;

        if (isServer)
        {
            playerColor = Color.blue;
        }
        else
        {
            playerColor = Color.red;
        }

        CmdSyncPlayerColor(playerColor);
    }

    [Command]
    void CmdSyncPlayerColor(Color color)
    {
        playerColor = color;
    }

    void OnColorChanged(Color oldColor, Color newColor)
    {
        playerColor = newColor;
        SetPlayerColor();
    }

    void SetPlayerColor()
    {
        GetComponent<SpriteRenderer>().color = playerColor;
    }

    void Update()
    {
        if (!isLocalPlayer || isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;

        if (isLocalPlayer && scoreManager != null)
        {
            scoreManager.IncreaseScore();
        }

        CmdJump();
    }

    [Command]
    void CmdJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        RpcJump();
    }

    [ClientRpc]
    void RpcJump()
    {
        if (!isLocalPlayer)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
            CmdTakeDamage();
        }
    }

    [Command]
    void CmdTakeDamage()
    {
        health--;
        RpcUpdateHealthUI();
        Debug.Log("Health: " + health);

        if (health <= 0)
        {
            // Oyun sona erdiğinde diğer oyuncuya bildirim gönder
            RpcHandleGameOver(netId);
        }
    }

    [ClientRpc]
    void RpcUpdateHealthUI()
    {
        if (isLocalPlayer)
        {
            UpdateHealthUI();
        }
    }

    void UpdateHealthUI()
    {
        if (health < 3)
        {
            can2.enabled = false;
        }
        if (health < 2)
        {
            can1.enabled = false;
        }
        if (health < 1)
        {
            can.enabled = false;
        }
    }

    [ClientRpc]
    void RpcHandleGameOver(uint loserNetId)
    {
        if (onlineGameManager == null)
        {
            onlineGameManager = FindObjectOfType<OnlineGameManager>();
        }

        if (onlineGameManager != null)
        {
            onlineGameManager.RpcGameOver(loserNetId);
        }
        else
        {
            Debug.LogError("OnlineGameManager could not be found.");
        }

        // Oyun bitti, zıplamayı durdur
        isGameOver = true;
    }
}
