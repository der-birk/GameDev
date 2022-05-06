using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Sprite[] playerSprites;
    [SerializeField] GameObject playerSprite;

    SpriteRenderer playerSpiteRenderer;
    bool isInStealth;
    public bool isDancing;
    public Vector3 movement;
    GameObject disco;

    // Start is called before the first frame update
    void Start()
    {
        playerSpiteRenderer = playerSprite.GetComponent<SpriteRenderer>();
        ChangeSprite();
        isInStealth = false;
        disco = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            ChangeSprite();
            if (!isDancing && Input.anyKey)
            {
                Dance();
                Movement();
            }
        }
    }
    void Movement()
    {
        //build movement vector
        movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) movement += Vector3.up;
        if (Input.GetKey(KeyCode.A)) movement += Vector3.left;
        if (Input.GetKey(KeyCode.S)) movement += Vector3.down;
        if (Input.GetKey(KeyCode.D)) movement += Vector3.right;
        movement = 3f * Time.deltaTime * movement.normalized;

        //check for stealth key, stealth and sprint
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) ToggleStealth();
        if (isInStealth) movement *= 0.65f;
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) movement *= 4.25f;
        transform.position += movement;
    }
    void ToggleStealth()
    {
        if (!isInStealth) playerSpiteRenderer.color = new Color(playerSpiteRenderer.color.r, playerSpiteRenderer.color.g, playerSpiteRenderer.color.b, 0.75f);
        else playerSpiteRenderer.color = new Color(playerSpiteRenderer.color.r, playerSpiteRenderer.color.g, playerSpiteRenderer.color.b, 1);
        isInStealth = !isInStealth;
    }
    void ChangeSprite()
    { 
        if (Input.GetKeyDown(KeyCode.F)) playerSpiteRenderer.sprite = playerSprites[Random.Range(0, playerSprites.Length)];
    }
    public void Dance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) StartCoroutine(CrossDance());
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) StartCoroutine(JumpDance());
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) StartCoroutine(SpinDance());
    }
    IEnumerator CrossDance()
    {
        if (isInStealth) ToggleStealth();
        float danceTime = Time.time;
        int danceLength = 20;
        isDancing = !isDancing;
        Vector3 moveVector;
        while (Time.time - danceTime <= 2f)
        {
            moveVector = new(-0.1f, -0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition -= moveVector;
                yield return null;
            }
            moveVector = new (-0.1f, 0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition -= moveVector;
                yield return null;
            }
            moveVector = new (0.1f, 0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition -= moveVector;
                yield return null;
            }
            moveVector = new (0.1f, -0.1f);
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition += moveVector;
                yield return null;
            }
            for (int i = 0; i < danceLength; i++)
            {
                playerSprite.transform.localPosition -= moveVector;
                yield return null;
            }
        }
        playerSprite.transform.localPosition = Vector3.zero;
        isDancing = !isDancing;
        StopCoroutine(CrossDance());
    }
    IEnumerator JumpDance()
    {
        if (isInStealth) ToggleStealth();
        float danceTime = Time.time;
        isDancing = !isDancing;
        while (Time.time - danceTime <= 2f)
        {
            for (int jumps = 0; jumps < 3; jumps++)
            {
                while (playerSprite.transform.localScale.x < 2f)
                {
                    playerSprite.transform.localScale *= 1.05f;
                    yield return null;
                }
                while (playerSprite.transform.localScale.x >= 1f)
                {
                    playerSprite.transform.localScale *= 0.95f;
                    yield return null;
                }
            }
        }
        playerSprite.transform.localScale = new Vector3(1, 1, 1);
        isDancing = !isDancing;
        StopCoroutine(JumpDance());
    }
    IEnumerator SpinDance()
    {
        if (isInStealth) ToggleStealth();
        float danceTime = Time.time;
        isDancing = !isDancing;
        while (Time.time - danceTime <= 2f)
        {
            playerSprite.transform.rotation *= Quaternion.AngleAxis(5f,new(0,0,1));
            yield return null;
        }
        isDancing = !isDancing;
        StopCoroutine(SpinDance());

    }
}
