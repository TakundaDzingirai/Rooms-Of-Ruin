using UnityEngine;

public class NextLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"){
            Debug.Log("NExtScene!!!");
            SceneSwitch.instance.LoadNext();
                
        }
       
    }


}
