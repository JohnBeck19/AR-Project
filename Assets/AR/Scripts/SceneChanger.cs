using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void OnPressPlay()
    {
      SceneManager.LoadScene("MarkerTest 2");
    }
}
