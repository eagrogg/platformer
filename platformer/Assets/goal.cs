using UnityEngine;
using UnityEngine.SceneManagement;


public class goal : MonoBehaviour
{
    public string levelName = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(levelName);
    }
}
