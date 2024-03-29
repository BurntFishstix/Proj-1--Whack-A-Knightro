using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField] private List<Mole> moles;

  [Header("UI objects")]
  [SerializeField] private GameObject playButton;
  [SerializeField] private GameObject gameUI;
  [SerializeField] private GameObject outOfTimeText;
  [SerializeField] private GameObject bombText;
  [SerializeField] private GameObject winText;
  [SerializeField] private GameObject loseText;
  [SerializeField] private TMPro.TextMeshProUGUI timeText;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;

  //Start Time
  private float startingTime = 11f;

  // Global variables
  private float timeRemaining;
  private HashSet<Mole> currentMoles = new HashSet<Mole>();
  private int score;
  private bool playing = false;

  // This is public so the play button can see it.
  public void StartGame() {
    // Hide/show the UI elements we don't/do want to see.
    playButton.SetActive(false);
    outOfTimeText.SetActive(false);
    bombText.SetActive(false);
    winText.SetActive(false);
    loseText.SetActive(false);
    gameUI.SetActive(true);
   
    // Hide all the visible moles.
    for (int i = 0; i < moles.Count; i++) {
      moles[i].Hide();
      moles[i].SetIndex(i);
    }
    // Remove any old game state.
    currentMoles.Clear();
    // Start with 10 seconds.
    timeRemaining = startingTime;
    score = 0;
    scoreText.text = "0";
    playing = true;
  }

  public void GameOver(int type) {
    // Show the message.
    if (type == 0) {
      outOfTimeText.SetActive(true);
    } else {
      bombText.SetActive(true);
    }
    if (score >= 12)
        {
            winText.SetActive(true);
        }
    if (score <=11)
        {
            loseText.SetActive(true);
        }
        // Hide all moles.
        foreach (Mole mole in moles) {
      mole.StopGame();
    }
    // Stop the game and show the start UI.
    playing = false;
    playButton.SetActive(true);
    }

  // Update is called once per frame
  void Update() {
        // Update time.
        if (playing) {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver(0);
        }
        timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
      // Check if we need to start any more moles.
      if (currentMoles.Count <= (score / 10)) {
        // Choose a random mole from the random mole numbers
        int index = Random.Range(0, moles.Count);
        // Doesn't matter if it's already doing something, we'll just try again next frame.
        if (!currentMoles.Contains(moles[index])) {
          currentMoles.Add(moles[index]);
          moles[index].Activate(score / 10);
        }
      }
    }
  }

  public void AddScore(int moleIndex) {
    // Add and update score.
    score += 1;
    scoreText.text = $"{score}";
    // Remove from active moles.
    currentMoles.Remove(moles[moleIndex]);
  }

  public void Missed(int moleIndex, bool isMole) {
    // Remove from active moles.
    currentMoles.Remove(moles[moleIndex]);
  }
}
