using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitAndToMenu");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator WaitAndToMenu() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}
