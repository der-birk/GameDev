using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    //references set in editor
    [SerializeField] SpriteRenderer bgRenderer;
    [SerializeField] GameObject[] lightPrefabs;
    [SerializeField] GameObject lightsParent;
    [SerializeField] Sprite dogeSprite;
    [SerializeField] GameObject lockdownAnimal;
    [SerializeField] GameObject NPCParent;
    [SerializeField] GameObject player;

    Camera cam;

    // variables for cheatcodes
    System.Text.StringBuilder cheatString;
    public bool isSquidGame, isRedlight;
    SpriteRenderer[] NPCSpriteRenderers;
    [SerializeField] GameObject[] NPCObjects;

    // Start is called before the first frame update

    void Start()
    {
        cam = Camera.main;
        cheatString = new System.Text.StringBuilder(30);
        NPCSpriteRenderers = NPCParent.GetComponentsInChildren<SpriteRenderer>();
        NPCObjects = new GameObject[NPCParent.transform.childCount];
        for (int i = 0; i < NPCParent.transform.childCount; i++) NPCObjects[i] = NPCParent.transform.GetChild(i).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            ChangeBrightness();
            SpawnLight();
            CheckForCheatCodes();
        }
    }
    void ChangeBrightness()
    {
        //check this out again, color seems to be fucekd
        if (Input.GetKey(KeyCode.Alpha7) || Input.GetKey(KeyCode.Keypad7)) bgRenderer.color -= new Color(0.1f, 0.1f, 0.1f, 0);
        if (Input.GetKey(KeyCode.Alpha8) || Input.GetKey(KeyCode.Keypad8)) bgRenderer.color += new Color(0.1f, 0.1f, 0.1f, 0);

    }
    void SpawnLight()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)))
        {
            Vector3 lightSpawnpoint = cam.ViewportToWorldPoint(new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), 1));
            Instantiate(lightPrefabs[Random.Range(0, 3)], lightSpawnpoint, Quaternion.identity, lightsParent.transform);
        }
    }
    void CheckForCheatCodes()
    {
        cheatString.Append(Input.inputString);
        //check for cheatcodes depending on the size of the code
        string testString = cheatString.ToString();
        if (testString.Contains("doge"))
        {
            //start doge here
            EnableDogeMode();
            Debug.Log("doge cheat was detected");
            cheatString.Clear();
        }
        if (testString.Contains("lockdown"))
        {
            //start lockdown here
            Lockdown();
            Debug.Log("lockdown cheat was detected");
            cheatString.Clear();
        }
        if (testString.Contains("squidgame") || Input.GetKeyDown(KeyCode.C))
        {
            //start squidgame here
            StopAllCoroutines();
            StartCoroutine(Squidgame());
            Debug.Log("squidgame cheat was detected");
            cheatString.Clear();
        }
        // shorten cheatstring for faster searches
        if (cheatString.Length > 15)
        {
            cheatString.Remove(0, 4);
        }
    }
    void EnableDogeMode()
    {
        foreach (SpriteRenderer sr in NPCSpriteRenderers)
        {
            sr.sprite = dogeSprite;
            sr.flipY = true;
        }
    }
    void Lockdown()
    {
        Instantiate(lockdownAnimal, cam.ViewportToWorldPoint(new Vector3(0f,0.5f,1) ), Quaternion.identity );
    }
    IEnumerator Squidgame()
    {
        //Debug.Log("squidgame was started");
        isSquidGame = true;
        while (isSquidGame)
        {
            SpriteRenderer[] lightsRenderers = lightsParent.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in lightsRenderers)
            {
                sr.gameObject.GetComponent<DiscoLights>().StopAllCoroutines();
                sr.color = Color.green;
            }
            isRedlight = false;
            //stay waiting in green phase
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            foreach (SpriteRenderer sr in lightsRenderers) { sr.color = Color.red; }
            //set red phase, countdown and check for movement
            float countdown = Random.Range(0f, 5f);
            isRedlight = true;
            while (countdown > 0)
            {
                //check for npc movement
                foreach (GameObject npc in NPCObjects)
                    if (npc.GetComponent<NPCBehaviour>().isDancing)
                    {
                        npc.SetActive(false);
                        npc.GetComponent<NPCBehaviour>().isDancing = false;
                    }
                //check for player movment
                if (player.GetComponent<Player>().isDancing || player.GetComponent<Player>().movement != Vector3.zero) Reset();
                countdown -= Time.deltaTime;
                yield return null;
            }
        }
    }
    public void Reset()
    {
       // Debug.Log("reset was triggered");
        isRedlight = false;
        isSquidGame = false;
        StopAllCoroutines();
        foreach (GameObject npc in NPCObjects) npc.SetActive(true);
        player.transform.position = Vector3.zero;
        foreach (DiscoLights script in lightsParent.GetComponentsInChildren<DiscoLights>()) StartCoroutine(script.ChangeColors());
    }
}
