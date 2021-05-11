using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _gameOverText;
	[SerializeField] private Text _restartText;
	[SerializeField] private Sprite[] _livesSprites;
	[SerializeField] private Image _livesImage;


    // Start is called before the first frame update
    void Start()
    {
    	//_livesSprites
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int newScore)
    {
    	_scoreText.text = "Score: " + newScore;
    }

    public void UpdateLives(int currentLives)
    {
    	_livesImage.sprite = _livesSprites[currentLives];

    	if(currentLives <= 0)
    	{
    		_restartText.enabled = true;
    		StartCoroutine(GameOverFlicker());
    	}
    }

    IEnumerator GameOverFlicker()
    {
    	while(true)
    	{
    		_gameOverText.enabled = true;
    		yield return new WaitForSeconds(0.5f);
    		_gameOverText.enabled = false;
    		yield return new WaitForSeconds(0.5f);
    	}
    }
}
