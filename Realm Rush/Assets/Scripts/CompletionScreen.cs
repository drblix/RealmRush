using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletionScreen : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
