using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;  // The name of the scene or its build index

    // Option 1: Public method for button or other triggers
    public static SceneSwitch instance;
    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("instance is null ,being assigned to this");
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Destroyed!!!");
            Destroy(gameObject);
        }
    }
    public void LoadScene()
    {
        // Load by scene name
        SceneManager.LoadScene(sceneToLoad);
    }

    // Option 2: Load by index
    public void LoadNext()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("index: "+index);
        SceneManager.LoadSceneAsync(index);
    }

    // Option 3: Collisions/Triggers (if you want an OnTriggerEnter)
    private void OnTriggerEnter(Collider other)    // or OnTriggerEnter2D for 2D
    {
        if (other.gameObject.tag=="Player")
        {
            Debug.Log("Scene Loaded");
            LoadNext();
        }
    }
}
