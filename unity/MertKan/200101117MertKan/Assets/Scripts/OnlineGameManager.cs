using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class OnlineGameManager : NetworkBehaviour
{
    public Text resultText;

    // Singleton instance
    public static OnlineGameManager instance;

    private bool gameEnded = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [ClientRpc]
    public void RpcGameOver(uint loserNetId)
    {
        if (resultText == null)
        {
            Debug.LogError("Result Text UI element is not assigned.");
            return;
        }

        if (gameEnded)
        {
            return; // Oyun zaten bitti, tekrar mesaj göstermeyelim
        }

        if (NetworkClient.localPlayer.netId == loserNetId)
        {
            resultText.text = "You Lose";
        }
        else
        {
            resultText.text = "You Win";
        }

        resultText.gameObject.SetActive(true);
        gameEnded = true; // Oyun bitti
    }
}
