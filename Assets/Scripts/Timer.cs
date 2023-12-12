using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private Text uiText;
    public int Duration;
    public int remainingDuration;

    public PlayerControl playerControlVariable;
    public GameManager gameManagerVariable;

    public bool timerOn;

    private void Start()
    {
        //Being(Duration);
        playerControlVariable = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    public void Being(int Second)
    {
        if (timerOn == false)
        {
            StartCoroutine(UpdateTimer());
        }
        remainingDuration = Second;
        
    }

    public IEnumerator UpdateTimer()
    {
        timerOn = true;
        while (remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    public void OnEnd()
    {
        gameManagerVariable = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        print("Game Over! Score was " + playerControlVariable.score);
        timerOn = false;
        gameManagerVariable.GameOver();
    }
   
    void Update()
    {
        
    }
}
