using UnityEngine;
using TMPro;

public class PlayerNameDisplay : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Text playerNameText;
    // public string savedPlayerName;

    void Start()
    {
        // PlayerPrefs.DeleteAll();
        // PlayerPrefs.Save();
        playerNameInput.onValueChanged.AddListener(UpdatePlayerName);       
        playerNameInput.onEndEdit.AddListener(EndEditPlayerName);
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
        {
            // Nếu có, hiển thị playerName
            playerNameText.text = PlayerPrefs.GetString("PlayerName");
            playerNameInput.gameObject.SetActive(false);
        }
        else
        {
            // Nếu không, yêu cầu người chơi nhập tên
            playerNameInput.gameObject.SetActive(true);
        }
        
    }

    void UpdatePlayerName(string newName)
    {
        playerNameText.text = newName;
        Debug.Log("starting player name");
    }

    void EndEditPlayerName(string newName)
    {
        Debug.Log("ending player name");
        playerNameInput.DeactivateInputField();
        playerNameInput.gameObject.SetActive(false);
        PlayerPrefs.SetString("PlayerName", newName);
        PlayerPrefs.Save();
        Debug.Log("ending player name");
    }
}

