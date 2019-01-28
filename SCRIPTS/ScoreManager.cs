using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text hiScoreText;

	public float ScoreCount;
	public float HiScoreCount;
	public float pointsPerSecond;

	public bool scoreIncreasing;


	void Start () {
			HiScoreCount = PlayerPrefs.GetFloat ("HighScore");
	}


	void Update () {
		if (scoreIncreasing) {
			ScoreCount += pointsPerSecond * Time.deltaTime;
		}

		if (ScoreCount > HiScoreCount) {
			HiScoreCount = ScoreCount;

		}

		scoreText.text = "Score: " + Mathf.Round (ScoreCount);
		hiScoreText.text = "High Score: " + Mathf.Round (HiScoreCount);

	
}
}

