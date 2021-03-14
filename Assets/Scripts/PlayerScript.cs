using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    
    public float speed;

    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Text loseText;

    private int scoreValue = 0;
    private int livesValue = 3;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = "Score: " + scoreValue.ToString();
        livesText.text = "Lives: " + livesValue.ToString();

        musicSource.clip = musicClipOne;
        musicSource.loop = true;

        SetScoreValue();
        SetLivesText();

        winText.text = "";
        loseText.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
       
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreValue();
            scoreText.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector2(43.0f, 3.0f);
                livesValue = 3;
                livesText.text = "Lives: " + livesValue.ToString();
                scoreValue = 4;
                scoreText.text = "Score: " + scoreValue.ToString();
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetLivesText();
            livesText.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void SetScoreValue()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            winText.text = "You Win! Game create by Abigail Detamore";
            musicSource.loop = false;
            musicSource.clip = musicClipTwo;
            musicSource.Play();

        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            loseText.text = "You Lose! Game created by Abigail Detamore";
        }
    }
}