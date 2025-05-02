using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScorePinball : MonoBehaviour {
    [SerializeField] TMP_Text text_Score;
    int score;
    void Update() {
        UpdateScore();
    }
    public void AddScore(int amount) {
        score += amount;
        UpdateScore();
    }
    void UpdateScore() {
        text_Score.text = " " + score;
    }
}
