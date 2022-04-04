using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _score_Text;
    [SerializeField]
    private Image _livesIMG;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _restartText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _score_Text.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is Null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _score_Text.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesIMG.sprite = _liveSprites[currentLives];

        if (currentLives <= 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        _gameOverText.text = "Game Over";
        yield return new WaitForSeconds(0.3f);
        _gameOverText.text = "Start Over";
        yield return new WaitForSeconds(0.3f);
    }

}
