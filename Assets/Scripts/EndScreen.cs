using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public static EndScreen inst;

    public StartGame sg;
    public Slider stars;
    public List<string> comments;
    public TMP_Text commentTMP, perfectTMP, goodTMP, badTMP, peopleRanOverTMP, compensationTMP, moneyMadeTMP, wordsCompletedTMP, bonusWordsDone;

    public int peopleRanOverTotal;
    public int wordsCompletedTotal, wordsNeededTotal;
    public int perfectTotal, goodTotal, badTotal;
    public float starRating;

    private void Awake()
    {
        inst = this;
        gameObject.SetActive(false);
    }

    public void EndGame()
    {
        AudioManager.Instance.StopAll();

        peopleRanOverTotal = Summarary.inst.peopleRanOverTotal;
        wordsCompletedTotal = Summarary.inst.wordsCompletedTotal;
        wordsNeededTotal = Summarary.inst.wordsNeededTotal;
        perfectTotal = Summarary.inst.perfectTotal;
        goodTotal = Summarary.inst.goodTotal;
        badTotal = Summarary.inst.badTotal;
        starRating = Summarary.inst.starRating;

        gameObject.SetActive(true);

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

        if(starRating < 0.1)
        {
            commentTMP.text = "I'm sorry but I don't think you're cut for this job..";
        }

        // Num of perfect, good, bad deliveries.
        perfectTMP.text = $"- Perfect!! x{perfectTotal}  +${3 * perfectTotal}";
        goodTMP.text = $"- Good! x{goodTotal}  +${1.5f * goodTotal}";
        badTMP.text = $"- Bad x{badTotal}  +${0.5f * badTotal}";

        if (wordsCompletedTotal > wordsNeededTotal)
        {
            bonusWordsDone.gameObject.SetActive(true);
            bonusWordsDone.text = $"- Bonus words completed! +${(wordsCompletedTotal - wordsNeededTotal) * 3}";
        }

        // People ran over text.
        peopleRanOverTMP.text = $"You did not run over anybody today great job!";
        if (peopleRanOverTotal > 0)
        {
            peopleRanOverTMP.text = $"You ran over x{peopleRanOverTotal} pedestrians today!";
            compensationTMP.gameObject.SetActive(true);
            compensationTMP.text = $"- Compensation -${peopleRanOverTotal * 4}";
        }

        // Words Comp text.
        wordsCompletedTMP.text = $"You completed {wordsCompletedTotal} deliveries today!";
        // Money Earned.
        float money = (3 * perfectTotal) + (1.5f * goodTotal) + (0.5f * badTotal) - (peopleRanOverTotal * 4);
        if (wordsCompletedTotal > wordsNeededTotal)
        {
            money += (wordsCompletedTotal - wordsNeededTotal) * 3;
        }

        if (money > 0)
        {
            moneyMadeTMP.text = $"You made ${money} today!";
        }
        else
        {
            moneyMadeTMP.text = $"You lost ${money} today!";
        }

        // Update star rating slider.
        stars.value = 1 - starRating;

        sg.enabled = true;
    }
}
