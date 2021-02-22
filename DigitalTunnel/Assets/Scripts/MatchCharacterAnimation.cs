using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCharacterAnimation : MonoBehaviour
{
    private Animator charAnim;
    private Animation charAnimation;
    private Rigidbody rigidbody;
    public float speed; 
    private float initialZ, currentZ;
    public float speedApprox = 0.05f; 
    public float counter = 0.05f;
    public float strtcounter = 0f;
    private bool isMoving = false;

    private void Awake()
    {
        charAnim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isMoving)
            return;
        StopAllCoroutines();
        StartCoroutine(movmntSpeed());
    }
    
    IEnumerator movmntSpeed()
    {
        Debug.Log("updating!"); 
        isMoving = true; 
        strtcounter = counter;
        initialZ = transform.position.x; 
        while (strtcounter >= 0f)
        {
            Debug.Log("subing"); 
            strtcounter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentZ = transform.position.x;
        float delta = currentZ - initialZ;
        speed = Mathf.Abs(delta / counter);
        if (speed <= speedApprox)
        {
            //charAnimation.wrapMode = WrapMode.Once;
            charAnim.SetBool("idling", true); 
        }
        else
        {
            //charAnimation.wrapMode = WrapMode.Loop;
            charAnim.SetBool("idling", false); 
        }
        charAnim.speed = Mathf.Clamp(speed, 0.7f, 1f);
        yield return new WaitForSeconds(counter); 
        isMoving = false;
    }
}
