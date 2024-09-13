using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public Button menuButton;

    void Start()
    {
        if (menuButton == null)
        {
            Debug.LogError("Menu butonu inspector'da atanmadı.");
            return;
        }

        menuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void ReturnToMainMenu()
    {
        // Ana menü sahnesini yükle
        SceneManager.LoadScene("Menu");
    }
}
