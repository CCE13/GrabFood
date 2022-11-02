using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour
{
    public List<string> perfectReviews;
    public List<string> goodReviews;
    public List<string> badReviews;

    private Animator _anim;
    private TMP_Text _textRating;
    private TMP_Text _comment;

    private const string S_COMMENT = "Comment";
    private const string S_TEXTRATING = "Text Rating";

    // Start is called before the first frame update

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _textRating = transform.Find(S_TEXTRATING).GetComponent<TMP_Text>();
        _comment = transform.Find(S_COMMENT).GetComponent<TMP_Text>();
    }

    public void NotificationPopUp(float starRating)
    {
        if (starRating < 2)
        {
            _textRating.text = "bad";
            int index = Random.Range(0, badReviews.Count);
            _comment.text = $"'{badReviews[index]}'";
        }
        else if(starRating > 2 && starRating < 4.5f)
        {
            _textRating.text = "good";
            int index = Random.Range(0, goodReviews.Count);
            _comment.text = $"'{goodReviews[index]}'";
        }
        else
        {
            _textRating.text = "Perfect";
            int index = Random.Range(0, perfectReviews.Count);
            _comment.text = $"'{perfectReviews[index]}'";
        }

        //set all the ratings and stuff
        _anim.Play("Notifcation slide in");

    }
}
