using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class WordUpdater : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private ObjectPool letterPool, peoplePool, notifPool;
    public static WordUpdater inst;
    public string currentWord;

    public TMP_Text wordDisplayText;
    public string wordToDisplay;

    // Default 3, lower means lower gain of fever.
    public int gainMult = 2;
    public float peopleSpawnDelayMax = 3;

    private int _lettersNeeded;
    private int _lettersCollected;

    private bool _startTimer;
    private float _timePassed;

    public List<string> words;
    private List<GameObject> shownNotifs = new();

    private void Awake()
    {
        inst = this;
    }

    private void Update()
    {
        if(_startTimer)
        {
            _timePassed += Time.deltaTime;
        }
    }

    [ContextMenu("Start")]
    public void NextWord()
    {
        StopAllCoroutines();
        _timePassed = 0;
        currentWord = words[Random.Range(0, words.Count)];
        _lettersNeeded = currentWord.Length;
        _lettersCollected = 0;
        wordToDisplay = "";

        for (int i = 0; i < currentWord.Length; i++)
        {
            wordToDisplay += "_";
            Vector3 letterPos = ground.transform.Find(currentWord[i].ToString()).transform.position;
            letterPos.y = 0.5f;

            var letter = letterPool.GetObj();
            letter.transform.position = letterPos;
            letter.TryGetComponent(out LetterHolder letterHolder);

            letterHolder.posInWord = i;
            letterHolder.letter = currentWord[i].ToString();

            letter.SetActive(true);
        }

        wordDisplayText.text = wordToDisplay;
        StartCoroutine(SpawnPeople());
    }

    public void UpdateTime(int pos, string letter)
    {
        _lettersCollected++;

        if(_lettersCollected == 1)
        {
            _startTimer = true;
        }

        if(pos == 0)
        {
            wordToDisplay = wordToDisplay.Remove(pos, 1).Insert(pos, letter.ToUpper());
        }
        else
        {
            wordToDisplay = wordToDisplay.Remove(pos, 1).Insert(pos, letter);
        }

        wordDisplayText.text = wordToDisplay;

        if (_lettersCollected == _lettersNeeded) StartCoroutine(EndTime());
    }

    private IEnumerator SpawnPeople()
    {
        // Spawn people here.
        peoplePool.GetObj().TryGetComponent(out Pedestrian newPeople);
        char c = (char)('a' + Random.Range(0, 26));
        Vector3 offset = ground.transform.Find(c.ToString()).transform.position;

        // 0 = bottom, 1 = top.
        int spawnLocation = Random.Range(0, 2);
        switch (spawnLocation)
        {
            case 0:
                // Spawn Bottom.
                newPeople.SpawnBottom(offset);
                break;
            case 1:
                // Spawn Top.
                newPeople.SpawnTop(offset);
                break;
        }

        float delay = Random.Range(0.5f, peopleSpawnDelayMax);
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnPeople());
    }

    public IEnumerator EndTime()
    {
        _startTimer = false;
        // 1 letter = 1s.
        // Assuming that the fever meter is 100MAX.
        // Difficulty can be increased by decreasing the value of 3, if possible allow player to change difficulty.
        // Use here to add to fever.

        float maxAmountToGive = _lettersNeeded * gainMult;
        float AmountToGive = maxAmountToGive - _timePassed;
        if(!FeverMode.inst.isFever) FeverMode.inst.AddFever(AmountToGive);

        // Calculate star rating.
        float starsRating = (8 - _timePassed) / 5;
        notifPool.GetObj().TryGetComponent(out Notification notif);
        notif.gameObject.SetActive(true);
        notif.NotificationPopUp(starsRating);
        NotifCheck(notif.gameObject);

        // Use here to start new word.
        yield return new WaitForSeconds(0.5f);
        NextWord();
    }

    public void NotifCheck(GameObject newNotif)
    {
        if (shownNotifs.Count < 1)
        {
            shownNotifs.Add(newNotif);
            return;
        }

        shownNotifs.Add(newNotif);

        for (int i = 0; i < shownNotifs.Count; i++)
        {
            if (!shownNotifs[i].activeSelf)
            {
                shownNotifs.RemoveAt(i);
                continue;
            }

            shownNotifs[i].TryGetComponent(out Notification notif);
            notif.StartMovedown();
        }
    }
}
