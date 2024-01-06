// using UnityEngine;

// public class PlayerDataHandler : MonoBehaviour
// {
//     void Start()
//     {
//         // Gọi hàm lưu và tải thông tin người chơi khi bắt đầu game
//         SavePlayerData("Người chơi 1", 100);
//         LoadPlayerData();
//     }

//     void SavePlayerData(string playerName, int playerScore)
//     {
//         // Lưu thông tin người chơi vào PlayerPrefs
//         PlayerPrefs.SetString("PlayerName", playerName);
//         PlayerPrefs.SetInt("PlayerScore", playerScore);

//         // Lưu PlayerPrefs để đảm bảo dữ liệu được ghi vào ổ đĩa
//         PlayerPrefs.Save();
//     }

//     void LoadPlayerData()
//     {
//         // Lấy thông tin người chơi từ PlayerPrefs
//         string playerName = PlayerPrefs.GetString("PlayerName", "Người chơi mặc định");
//         int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);

//         // In thông tin người chơi ra Console
//         Debug.Log($"PlayerName: {playerName}, PlayerScore: {playerScore}");
//     }
// }
