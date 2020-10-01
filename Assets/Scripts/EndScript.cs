using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour
{
    public SpriteRenderer background;
    public SpriteRenderer wizard;

    public Sprite[] wizardSprite;
    public Sprite[] backgroundSprite;

    GameObject managerObject;
    GameManager managerScript;

    public Text endText;

    [TextArea(2, 10)]
    public string[] endStrings;

    public AudioSource wooshSource;
    public AudioClip wooshClip;

    // Start is called before the first frame update
    void Start()
    {
        managerObject = GameObject.Find("GameManager");
        managerScript = managerObject.GetComponent<GameManager>();

        background.sprite = backgroundSprite[managerScript.greatestElement];
        wizard.sprite = wizardSprite[managerScript.greatestElement];

        endText.text = endStrings[managerScript.greatestElement];

        wooshSource.PlayOneShot(wooshClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
