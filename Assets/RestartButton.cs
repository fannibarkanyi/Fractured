using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1f; // unfreeze if game over paused time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
