using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPlaying;
    public bool isPaused;
    public Vector2 CreatPos = new(0, -4);

    private GameObject Player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
    }

    public void setPlayer()
    {
        if (!GameObject.Find("Player0"))
        {
            Player = Pool.Get("Player0");
            Player.transform.position = CreatPos;
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void End()
    {
        isPlaying = false;
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Continue()
    {
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LateUpdate()
    {
        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }
}
