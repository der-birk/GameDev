using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingText : MonoBehaviour
{
    [SerializeField] RectTransform textTransform;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Movement());
    }

    void Update()
    {
        if (Input.anyKeyDown) SceneManager.LoadScene("A2_Game");   
    }

    IEnumerator Movement()
    {
        while (true)
        {            
            for (int i = 15; i > 0; i--)
            {
                textTransform.position -= i * Time.deltaTime * Vector3.down;
                yield return null;
            }
            for (int i = 15; i > 0; i--)
            {
                textTransform.position += i * Time.deltaTime * Vector3.down;
                yield return null;
            }
            yield return null;
        }
    }
}
