using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[DefaultExecutionOrder(90)]
public class TextxScroll : MonoBehaviour
{
    [SerializeField] float spd = 60;
    [SerializeField] int waitTime = 25500;//25.5 sec
    [SerializeField] GameObject uiPanel;
    [SerializeField] GameObject txtPanel;
    [SerializeField] Camera cam;

    private void Start()
    {
        cam = Camera.main;
        uiPanel.SetActive(false);
        cam.GetComponent<CameraFollows>().enabled = false;
    }
    async void Update()
    {
        Vector3 pos = GetComponent<RectTransform>().position;

        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
        //Vector3 localVectorBack = transform.TransformDirection(0, 0, 1);

        pos += localVectorUp * spd * Time.deltaTime;
        //pos += localVectorBack * spd * Time.deltaTime;
        transform.position = pos;

        if (Input.GetKey(KeyCode.Space))
        {
            Go();
        }
        await Task.Delay(waitTime);

        Go();
    }

    void Go()
    {
        cam.GetComponent<CameraFollows>().enabled = true;
        uiPanel.SetActive(true);
        txtPanel.SetActive(false);
    }
}

/*
 * the story : It was a peaceful in the kingdom of pardenia.
 All until the goblin attacks...

The goblin leader Nixwixx, brought his clan for pillage and treasure hunting, mainly around the big city of Kill-a-lot.
Among everyone else, Bobs farme got attack and the goblin destroyied his crops, kidnap his wife AND STOLE HIS SHOVEL!

Now, with a bounty on Nixwixxs head and the promise of knighthood from king  Leopord VI, Bob is set on a mission to get vengeance for his kingdom and GET BACK HIS SHOVEL!

When he was almost out of the goblins lair, Bob found a sword, with some weird and strange runes engraved on. Through his  blind rage Bob forgot that he is a farmer with no traning in sword and illiterature enough to not have the ability to read, Bob grabed the sword and everything went black...
 */
