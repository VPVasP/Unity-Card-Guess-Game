using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ButtonScriptable[] buttons;
    [SerializeField] private Transform panelToSpawnButtons;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private AudioSource correctSound;
    private int spawnNumber;
    private int score;
    private Button firstButtonClicked;
    private Button secondButtonClicked;
    private AudioSource aud;
    private void Start()
    {
        //we initialize the audiocomponent
        aud = GetComponent<AudioSource>();
        aud.clip = mainMusic;
        aud.loop = true;
        aud.Play();
       //we create a list to shuffle the buttons
        List<int> shuffleButtons = new List<int>();
        for (int i = 0; i < buttons.Length; i++)
        {
            shuffleButtons.Add(i);
        }
        //we call the shuffle method with our shufflebuttonns list
        Shuffle(shuffleButtons);
        //we spawn the buttons based on the list
        SpawnButtons(shuffleButtons);
    }

    //function that handles the shuffle of the list
    private void Shuffle<T>(List<T> list)
    {
        
        for (int i = 0; i < list.Count; i++)
        {
            int randomRangeFromTheList = i + Random.Range(0, list.Count - i);
            T tempList = list[randomRangeFromTheList];
            list[randomRangeFromTheList] = list[i];
            list[i] = tempList;
        }
    }
    private void Update()
    {
        //if all the cards are collected we win the game
        if (score == 32)
        {
            Debug.Log("Game Won");
            SceneManager.LoadScene("Level01");
        }
    }
 
     private void SpawnButtons(List<int> shuffleButtons)
    {
        foreach (int shuffleButtonIndex in shuffleButtons)
        {
            ButtonScriptable buttonScriptable = buttons[shuffleButtonIndex];
            Button buttonToBeInstatiated = Instantiate(buttonPrefab, panelToSpawnButtons);
            buttonToBeInstatiated.image.sprite = buttonScriptable.firstPicture;
            buttonToBeInstatiated.onClick.AddListener(() => OnButtonClick(buttonToBeInstatiated, buttonScriptable));
        }
    }
    //function that handles the button clicking
    private void OnButtonClick(Button currentButton, ButtonScriptable buttonScriptable)
    {
      //if the button can be clicked
        if (IsClickable(currentButton))
        {
            currentButton.image.sprite = buttonScriptable.secondPicture;
            //we check if it's the first or second button clicked
            if (firstButtonClicked == null)
            {
                firstButtonClicked = currentButton;
            }
            else if (secondButtonClicked == null)
            {
                secondButtonClicked = currentButton;
                //we check for the match of the cards after 1 second
                if (firstButtonClicked != secondButtonClicked)
                {
                    StartCoroutine(CheckMatch());
                }
            }
        }
    }
    //bool that checks if our button is clickable
    private bool IsClickable(Button button)
    {
        return secondButtonClicked == null || button == firstButtonClicked || button == secondButtonClicked;
    }
    //coroutine to check if both the buttons are matched
    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1.0f);

        if (firstButtonClicked.image.sprite == secondButtonClicked.image.sprite)
        {
            correctSound.Play();
            score += 1;
            scoreText.text = "Score:  " + score;
            Destroy(firstButtonClicked.gameObject);
            Destroy(secondButtonClicked.gameObject);
        }
        else
        {
            //if the buttons don't match we return them back to the first image which is the card
            firstButtonClicked.image.sprite = buttons[firstButtonClicked.transform.GetSiblingIndex()].firstPicture;
            secondButtonClicked.image.sprite = buttons[secondButtonClicked.transform.GetSiblingIndex()].firstPicture;
        }
        //we reset the clicked buttons
      firstButtonClicked = null;
      secondButtonClicked = null;


    
    }

}
