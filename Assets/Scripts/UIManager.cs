using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _gameOverText;
    [SerializeField] private Text _gameWinText;
	[SerializeField] private Text _restartText;
    [SerializeField] private Text _ammoText;
	[SerializeField] private Sprite[] _livesSprites;
	[SerializeField] private Image _livesImage;
    [SerializeField] private Image _thrusterImage;

    private float _thrusterCooldown = 10.0f;
    private bool _canThruster = true;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player == null)
        {
            Debug.LogError("Player was NULL");
        }
        _scoreText.text = "Score: " + 0;
        _ammoText.text = "Ammo: " + 15 + "/15";
        _thrusterImage.fillAmount = 0.0f;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && _canThruster == true)
        {
            _thrusterImage.fillAmount += 1.0f / 3.0f * Time.deltaTime;
        }
        else
        {
            _thrusterImage.fillAmount -= 1.0f / 10.0f * Time.deltaTime;
        }

        if(_thrusterImage.fillAmount == 1.0f && _canThruster == true)
        {
            _canThruster = false;
            player.ThrusterDown();
        }

        if(_thrusterImage.fillAmount < 0.5f && _canThruster == false)
        {
            _canThruster = true;
            player.ThrusterApproved();
        }
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

    public void UpdateAmmo(int currentAmmo)
    {
        if(currentAmmo <= 0)
        {
            _ammoText.text = "Ammo: " + 0 + "/15";
            _ammoText.color = Color.red;
        }
        else
        {
            _ammoText.text = "Ammo: " + currentAmmo + "/15";
            _ammoText.color = Color.white;
        }
    }

    public void GameWin()
    {
        _gameWinText.enabled = true;
        _restartText.enabled = true;
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
