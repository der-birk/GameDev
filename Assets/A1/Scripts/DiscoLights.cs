using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLights : MonoBehaviour
{
    SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.HSVToRGB(Random.value, Random.Range(0.4f, 1f), Random.Range(0.4f, 1f));
        StartCoroutine(ChangeColors());
    }
    public IEnumerator ChangeColors()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            sr.color = Color.HSVToRGB(Random.value, Random.Range(0.4f, 1f), Random.Range(0.4f, 1f));
        }
    }
}
