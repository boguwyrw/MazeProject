using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBall : MonoBehaviour
{

    public Rigidbody rigidbody;
    public Text winText;
    public Text restartText;
    public Text gameOverText;
    public Text quitGameText;
    public Text timerText;

    private bool gameOver;
    private bool restart;
    private float startTime;
    private float ballSpeed;

    private void Start()
    {
        Cursor.visible = false;
        rigidbody = GetComponent<Rigidbody>();
        winText.text = "";
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        quitGameText.text = "";
        startTime = Time.time;
        ballSpeed = 130.0f;
    }

    private void Update()
    {
        
        if (!gameOver)
        {
        float tmpTime = Time.time - startTime;
        string minutes = ((int)tmpTime / 60).ToString();
        string seconds = (tmpTime % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
                Time.timeScale = 1;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }

            Time.timeScale = 0;
        }

    }

    private void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movment = new Vector3(moveHorizontal * ballSpeed * Time.deltaTime, 0.0f, moveVertical * ballSpeed * Time.deltaTime);

        rigidbody.AddForce(movment);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Treasure"))
        {
            other.gameObject.SetActive(false);
            winText.text = "You WIN";
            gameOverText.text = "Game Over";
            gameOver = true;
        }
        if (gameOver)
        {
            restartText.text = "Press 'R' for restart";
            quitGameText.text = "Press 'Q' to quit";
            restart = true;
        }
    }
    
}
