using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class video_WSP2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WSP2");
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;

    }
    IEnumerator WSP2()
    {
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene(6);

    }
}
