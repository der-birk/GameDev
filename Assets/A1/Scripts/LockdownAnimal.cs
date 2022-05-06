using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockdownAnimal : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Time.deltaTime * 7 * Vector3.right;
        if (!spriteRenderer.isVisible) Destroy(gameObject);
    }
}
