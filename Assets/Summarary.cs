using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Summarary : MonoBehaviour
{
    public static Summarary inst;

    public Animator summaryUI;

    public StartGame sg;
    public Slider stars;
    public List<string> comments;
    public TMP_Text commentTMP, perfectTMP, goodTMP, badTMP, peopleRanOverTMP,compensationTMP,moneyMadeTMP, wordsCompletedTMP, bonusWordsDone;

    public int peopleRanOverRound, peopleRanOverTotal;
    public int wordsCompleted,wordsCompletedTotal, wordsNeeded;
    public int perfect, good, bad, perfectTotal, goodTotal, badTotal;
    public float starRating, newStarRating, roundStarLumSum;
    public float moneyMadeRound, moneyMadeTotal;

    private void Awake()
    {
        inst = this;
        starRating = 0.5f;
        wordsNeeded = 4;
    }

    public void RoundSummary()
    {
        summaryUI.Play("Enter");
        FeverMode.inst.isFever = false;
        FeverMode.inst.paused = true;

        // If the player did not collect enough words, rating for that round would be 2.5 regardless of perfect,ect.
        newStarRating = roundStarLumSum / wordsCompleted;

        if(wordsCompleted < wordsNeeded)
        {
            newStarRating = 0f;
        }

        if(starRating != newStarRating)
        {
            starRating += (newStarRating - starRating) / 2;
        }

        if (starRating < 0.4)
        {
            commentTMP.text = comments[0];
        }
        else if (starRating > 0.4 && starRating < 0.9f)
        {
            commentTMP.text = comments[1];
        }
        else
        {
            commentTMP.text = comments[2];
        }

        // Num of perfect, good, bad deliveries.
        perfectTMP.text = $"- Perfect!! x{perfect}  +${3 * perfect}";
        goodTMP.text = $"- Good! x{good}  +${1.5f * good}";
        badTMP.text = $"- Bad x{bad}  +${0.5f * bad}";
        if (wordsCompleted > wordsNeeded)
        {
            bonusWordsDone.gameObject.SetActive(true);
            bonusWordsDone.text = $"- Bonus words completed! +${(wordsCompleted - wordsNeeded) * 3}";
        }

        // People ran over text.
        peopleRanOverTMP.text = $"You ran over x{peopleRanOverRound} pedestrians this round!";
        if(peopleRanOverRound > 0)
        {
            compensationTMP.gameObject.SetActive(true);
            compensationTMP.text = $"- Compensation -${peopleRanOverRound * 4}";
        }

        // Words Comp text.
        wordsCompletedTMP.text = $"You completed {wordsCompleted}/{wordsNeeded} deliveries!";
        // Money Earned.
        float money = (3*perfect) + (1.5f * good) + (0.5f * bad) - (peopleRanOverRound * 4);
        if (wordsCompleted > wordsNeeded)
        {
            money += (wordsCompleted - wordsNeeded) * 3;
        }

        if(money > 0)
        {
            moneyMadeTMP.text = $"You made ${money} this hour!";
        }
        else
        {
            moneyMadeTMP.text = $"You lost ${money} this hour!";
        }

        // Update star rating slider.
        stars.value = 1 - starRating;

        sg.enabled = true;

        // if the star rating is <0.07 player loses.
    }

    public void NewRound()
    {
        summaryUI.Play("Exit");

        compensationTMP.gameObject.SetActive(false);
        bonusWordsDone.gameObject.SetActive(false);
        moneyMadeRound = 0;
        peopleRanOverRound = 0;
        wordsCompleted = 0;
        wordsNeeded = 0;
        perfect = 0;
        good = 0;
        bad = 0;
        newStarRating = 0;
        wordsNeeded = Random.Range(5, 11);
    }
}
