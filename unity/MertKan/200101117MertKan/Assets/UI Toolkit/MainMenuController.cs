using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Mirror;

public class MainMenuController : MonoBehaviour
{
    public VisualTreeAsset mainMenuAsset;
    public VisualTreeAsset singlePlayerAsset;
    public VisualTreeAsset multiPlayerAsset;
    public VisualTreeAsset settingsAsset;

    public GameObject catPrefab;
    public GameObject dogPrefab;

    private VisualElement root;
    private Stack<VisualTreeAsset> menuStack = new Stack<VisualTreeAsset>();
    private string selectedCharacter;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        if (root == null)
        {
            Debug.LogError("Root VisualElement is null. Ensure UIDocument is attached to the GameObject.");
            return;
        }

        LoadMainMenu();
    }

    private void LoadMainMenu()
    {
        root.Clear();
        mainMenuAsset.CloneTree(root);

        var singlePlayerButton = root.Q<Button>("SinglePlayerButton");
        var multiPlayerButton = root.Q<Button>("MultiPlayerButton");
        var settingsButton = root.Q<Button>("SettingsButton");
        var exitButton = root.Q<Button>("ExitButton");

        if (singlePlayerButton == null || multiPlayerButton == null || settingsButton == null || exitButton == null)
        {
            Debug.LogError("One or more buttons are null. Ensure the button names in the UXML file match the names in the script.");
            return;
        }

        singlePlayerButton.clicked += ShowSinglePlayerPage;
        multiPlayerButton.clicked += ShowMultiPlayerPage;
        settingsButton.clicked += ShowSettingsPage;
        exitButton.clicked += ExitGame;
    }

    private void ShowSinglePlayerPage()
    {
        menuStack.Push(mainMenuAsset);
        root.Clear();
        singlePlayerAsset.CloneTree(root);

        var catButton = root.Q<Button>("CatButton");
        var dogButton = root.Q<Button>("DogButton");
        var startButton = root.Q<Button>("StartButton");
        var backButton = root.Q<Button>("BackButton");

        if (catButton != null) catButton.clicked += () => SelectCharacter("Cat");
        if (dogButton != null) dogButton.clicked += () => SelectCharacter("Dog");
        if (startButton != null) startButton.clicked += StartSinglePlayerGame;
        if (backButton != null) backButton.clicked += GoBack;
    }

    private void SelectCharacter(string character)
    {
        selectedCharacter = character;
        Debug.Log(character + " selected");
    }

    private void StartSinglePlayerGame()
    {
        if (string.IsNullOrEmpty(selectedCharacter))
        {
            Debug.LogError("No character selected");
            return;
        }

        // Save the selected character to be used in the Game scene
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);
        SceneManager.LoadScene("Game1");
    }

    private void ShowMultiPlayerPage()
    {
        menuStack.Push(mainMenuAsset);
        root.Clear();
        multiPlayerAsset.CloneTree(root);


        var startButton = root.Q<Button>("StartButton");
        var backButton = root.Q<Button>("BackButton");

        if (startButton != null) startButton.clicked += StartMultiPlayerGame;
        if (backButton != null) backButton.clicked += GoBack;
    }

    private void SelectMultiPlayerCharacter(int player, string character)
    {
        // Multiplayer character selection logic
    }

    private void StartMultiPlayerGame()
    {
        SceneManager.LoadScene("Game");
    }

    private void ShowSettingsPage()
    {
        menuStack.Push(mainMenuAsset);
        root.Clear();
        settingsAsset.CloneTree(root);

        var backButton = root.Q<Button>("BackButton");
        var soundButton = root.Q<Button>("SoundButton");
        if (backButton != null)
        {
            backButton.clicked += GoBack;
        }

        if (soundButton != null)
        {
            soundButton.clicked += ToggleSound;
        }
    }

    private void ToggleSound()
    {
        SoundManager.Instance.ToggleSound();
    }

    private void GoBack()
    {
        if (menuStack.Count > 0)
        {
            var previousMenu = menuStack.Pop();
            root.Clear();
            previousMenu.CloneTree(root);

            if (previousMenu == mainMenuAsset)
            {
                LoadMainMenu();
            }
            else
            {
                var backButton = root.Q<Button>("BackButton");
                if (backButton != null) backButton.clicked += GoBack;

                var startButton = root.Q<Button>("StartButton");
                if (startButton != null) startButton.clicked += () => SceneManager.LoadScene("Game");
            }
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
