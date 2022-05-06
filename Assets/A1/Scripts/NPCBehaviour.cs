using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public bool isDancing;
    float timeBetweenDances;
    [SerializeField] Sprite originalSprite;
    void Awake()
    {
        isDancing = false;
        timeBetweenDances = 0f;
        GetComponent<SpriteRenderer>().sprite = originalSprite;
    }

    private void Update()
    {
        if (!isDancing)
        {
            timeBetweenDances += Time.deltaTime;
            if (timeBetweenDances > Random.Range(2, 6))
            {
                switch (Random.Range(0, 3))
                {
                    case 0: StartCoroutine(CrossDance()); break;
                    case 1: StartCoroutine(JumpDance()); break;
                    case 2: StartCoroutine(SpinDance()); break;
                }
            }
        }
    }
    IEnumerator CrossDance()
    {
        float danceTime = Time.time;
        int danceLength = 20;
        isDancing = !isDancing;
        Vector3 moveVector;
        while (Time.time - danceTime <= 2f)
        {
            moveVector = new(-0.1f, -0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition -= moveVector;
                yield return null;
            }
            moveVector = new(-0.1f, 0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition -= moveVector;
                yield return null;
            }
            moveVector = new(0.1f, 0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition -= moveVector;
                yield return null;
            }
            moveVector = new(0.1f, -0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                transform.localPosition -= moveVector;
                yield return null;
            }
        }

        isDancing = !isDancing;
        timeBetweenDances = 0f;
        StopCoroutine(CrossDance());
    }
    IEnumerator JumpDance()
    {
        float danceTime = Time.time;
        isDancing = !isDancing;
        while (Time.time - danceTime <= 2f)
        {
            for (int jumps = 0; jumps < 3; jumps++)
            {
                while (transform.localScale.x < 2f)
                {
                    transform.localScale *= 1.05f;
                    yield return null;
                }
                while (transform.localScale.x >= 1f)
                {
                    transform.localScale *= 0.95f;
                    yield return null;
                }
            }
        }
        transform.localScale = new Vector3(1, 1, 1);
        isDancing = !isDancing;
        timeBetweenDances = 0f;
        StopCoroutine(JumpDance());
    }
    IEnumerator SpinDance()
    {
        float danceTime = Time.time;
        isDancing = !isDancing;
        while (Time.time - danceTime <= 2f)
        {
            transform.rotation *= Quaternion.AngleAxis(5f, new(0, 0, 1));
            yield return null;
        }
        isDancing = !isDancing;
        timeBetweenDances = 0f;
        StopCoroutine(SpinDance());

    }

}
