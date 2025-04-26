using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Feedback : MonoBehaviour
{
    [SerializeField] CardFeedbackController testingCardFeedback;
    [SerializeField] bool Trigger_testCardFeedback;
    public enum CardTestingEnum
    {
        FlipCard, Spawn,Destroy,Attack,GetAttacked,Select, Deselect, FollowMouse, UnfollowMouse
    }
    [SerializeField] CardTestingEnum feedbackToTest;
    public enum FeedbackState
    {
        playerSelected, playerFree, NoInput, 
    }
    private void Update()
    {
        if(Trigger_testCardFeedback)
        {
            Trigger_testCardFeedback = false;

            switch (feedbackToTest)
            {
                case CardTestingEnum.FlipCard:
                    testingCardFeedback.FlipCard();
                    break;
                case CardTestingEnum.Spawn:
                    testingCardFeedback.SpawnCard();
                    break;
                case CardTestingEnum.Destroy:
                    testingCardFeedback.DestroyCard();
                    break;
                case CardTestingEnum.Attack:
                    testingCardFeedback.AttackCardBelow();
                    break;
                case CardTestingEnum.GetAttacked:
                    testingCardFeedback.GettingAttacked();
                    break;
                case CardTestingEnum.Select:
                    testingCardFeedback.CardSelected();
                    break;
                case CardTestingEnum.Deselect:
                    testingCardFeedback.CardDeselected();
                    break;
                case CardTestingEnum.FollowMouse:
                    testingCardFeedback.StartFollowingMouse();
                    break;
                case CardTestingEnum.UnfollowMouse:
                    testingCardFeedback.EndFollowingMouse(Camera.main.transform.position + Input.mousePosition);
                    break;
            }
        }

    }
}