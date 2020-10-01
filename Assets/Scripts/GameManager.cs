using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager managerThing;

    public int waterPoints;
    public int windPoints;
    public int firePoints;
    public int lightPoints;
    public int twilightPoints;

    public int greatestElement;

    public GameObject convoObject;
    public ConvoScript convoScript;

    public AudioSource musicSource;
    public AudioClip shopMusic;

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Manager");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            convoObject = GameObject.Find("ConvoManager");
            convoScript = convoObject.GetComponent<ConvoScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(SceneManager.GetActiveScene().buildIndex == 0)
       {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                greatestElement = 1;

                waterPoints = 0;
                windPoints = 0;
                firePoints = 0;
                lightPoints = 0;
                twilightPoints = 0;

                SceneManager.LoadScene(1);
            }
        }
       else if(SceneManager.GetActiveScene().buildIndex == 2)
       {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    

    public void AddPoints(Answer answer)
    {
        if(answer.primaryElement == Answer.element.water)
        {
            waterPoints += 2;
        }
        else if(answer.secondaryElement == Answer.element.water)
        {
            waterPoints += 1;
        }

        if (answer.primaryElement == Answer.element.wind)
        {
            windPoints += 2;
        }
        else if (answer.secondaryElement == Answer.element.wind)
        {
            windPoints += 1;
        }

        if (answer.primaryElement == Answer.element.fire)
        {
            firePoints += 2;
        }
        else if (answer.secondaryElement == Answer.element.fire)
        {
            firePoints += 1;
        }

        if (answer.primaryElement == Answer.element.light)
        {
            lightPoints += 2;
        }
        else if (answer.secondaryElement == Answer.element.light)
        {
            lightPoints += 1;
        }

        if (answer.primaryElement == Answer.element.twilight)
        {
            twilightPoints += 2;
        }
        else if (answer.secondaryElement == Answer.element.twilight)
        {
            twilightPoints += 1;
        }
    }

    public void EndQuiz()
    {
        CheckGreatest();
        SceneManager.LoadScene(2);
    }

    public void CheckGreatest()
    {
        if (waterPoints >= windPoints && waterPoints >= firePoints && waterPoints >= lightPoints && waterPoints >= twilightPoints)
        {
            greatestElement = 1;
        }
        else if (windPoints > waterPoints && windPoints >= firePoints && windPoints >= lightPoints && windPoints >= twilightPoints)
        {
            greatestElement = 2;
        }
        else if (firePoints > waterPoints && firePoints > windPoints && firePoints >= lightPoints && firePoints >= twilightPoints)
        {
            greatestElement = 3;
        }
        else if (lightPoints > waterPoints && lightPoints > firePoints && lightPoints > windPoints && lightPoints >= twilightPoints)
        {
            greatestElement = 4;
        }
        else
        {
            greatestElement = 5;
        }
    }
}
