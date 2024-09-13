using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject catPrefab;
    public GameObject dogPrefab;
    public Vector3 spawnPosition; // Karakterin oluşturulacağı pozisyon

    void Start()
    {
        string selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");

        GameObject characterPrefab = null;

        if (selectedCharacter == "Cat")
        {
            characterPrefab = catPrefab;
        }
        else if (selectedCharacter == "Dog")
        {
            characterPrefab = dogPrefab;
        }

        if (characterPrefab != null)
        {
            Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No character prefab found for: " + selectedCharacter);
        }
    }
}
