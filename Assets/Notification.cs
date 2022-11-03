using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private RectTransform trsf;
    private Image mask;
    private Slider starsSlider;

    // Start is called before the first frame update

    private void Awake()
    {
        trsf = GetComponent<RectTransform>();
        starsSlider = GetComponentInChildren<Slider>();
        mask = GetComponent<Image>();
        _anim = GetComponent<Animator>();
        _textRating = transform.Find(S_TEXTRATING).GetComponent<TMP_Text>();
        _comment = transform.Find(S_COMMENT).GetComponent<TMP_Text>();
    }

    public void NotificationPopUp(float starRating)
    {
        if (starRating < 0.4)
        {
            _textRating.text = "Bad";
            int index = Random.Range(0, badReviews.Count);
            _comment.text = $"'{badReviews[index]}'";
        }
        else if(starRating > 0.4 && starRating < 0.9f)
        {
            _textRating.text = "Good!";
            int index = Random.Range(0, goodReviews.Count);
            _comment.text = $"'{goodReviews[index]}'";
        }
        else
        {
            _textRating.text = "Perfect!!";
            int index = Random.Range(0, perfectReviews.Count);
            _comment.text = $"'{perfectReviews[index]}'";
        }

        starsSlider.value = 1 - starRating;

        //set all the ratings and stuff
        _anim.enabled = true;
        _anim.Play("Notifcation slide in");
        Invoke("StartSlideOut", 10);

    }

    private void StartSlideOut()
    {
        StartCoroutine(SlideOut());
    }

    public void StartMovedown()
    {
        StartCoroutine(MoveDown());
    }

    public void StartEndPos()
    {
        _anim.enabled = false;
        transform.localPosition = new Vector2(2, -17);
    }

    private IEnumerator MoveDown()
    {
        Vector2 target = new(trsf.localPosition.x + 200, trsf.localPosition.y - 220);
        float timeToTake = 0.4f;
        float timePassed = 0;

        while (timePassed < timeToTake)
        {
            trsf.localPosition = Vector2.Lerp(trsf.localPosition, target, timePassed / timeToTake);

            timePassed += Time.deltaTime;
            yield return null;
        }

        trsf.localPosition = target;
    }

    private IEnumerator SlideOut()
    {
        Vector2 target = new(560, trsf.localPosition.y);
        float timeToTake = 0.8f;
        float timePassed = 0;

        while(timePassed < timeToTake)
        {
            mask.color = Color.Lerp(mask.color, new Color(255, 255, 255, 0), timePassed / timeToTake) ;
            trsf.localPosition = Vector2.Lerp(trsf.localPosition, target, timePassed / timeToTake);

            timePassed += Time.deltaTime;
            yield return null;
        }

        trsf.localPosition = target;
        mask.color = new Color(255, 255, 255, 1);
        gameObject.SetActive(false);
    }
}
