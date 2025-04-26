using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardController : MonoBehaviour
{
    Animator cardAnimator;
 
    private void Awake()
    {
        cardAnimator = GetComponent<Animator>();
    }
    Coroutine followCoroutine;
    public void StartFollowingMouse()
    {
        if (followCoroutine != null) { return; }
        followCoroutine = StartCoroutine(FollowingMouse());
    }
    IEnumerator FollowingMouse()
    {
        LayerMask layerMask = new LayerMask();
        int ignoreAllButLayer = 1 << LayerMask.NameToLayer("Ground");
        layerMask = ignoreAllButLayer;
        while (true)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit RayHit))
            {
                Vector3 targetPos = RayHit.point;
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.1f);
                Debug.Log(targetPos);
            }
            else
            {
                Debug.Log("no ground found" + Input.mousePosition);
            }

            yield return null;
        }
    }
    public void EndFollowingMouse(Vector2 placementPos)
    {
        transform.position = placementPos;
        if (followCoroutine != null) { StopCoroutine(followCoroutine); }
    }

    public void FlipCard()
    {
        cardAnimator.SetBool("isFlipped", !cardAnimator.GetBool("isFlipped"));
        cardAnimator.SetTrigger("Flip");
    }
}
