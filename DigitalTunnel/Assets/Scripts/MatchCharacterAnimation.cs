using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class MatchCharacterAnimation : MonoBehaviour
{
    private Animator charAnim;
    private Animation charAnimation;
    public float speed;
    private float initialZ, currentZ;
    public float idlingLimit = 0.15f; 
    public float counter = 0.05f;
    public float strtcounter = 0f;
    public float dirlimit = 0.025f; 
    private bool isMoving = false;
    public bool currDer = true;
    public bool initDir = true;

    // for outputs 
    public float outputSpeed = 0f;

    private void Awake()
    {
        charAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
            return;
        StopAllCoroutines();
        StartCoroutine(MovmntSpeed());
    }
    
    IEnumerator MovmntSpeed()
    {
        isMoving = true; 
        strtcounter = counter;
        initialZ = transform.position.x; 
        // this while loop might be redundant (waiting 2 times) 
        while (strtcounter >= 0f)
        {
            strtcounter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        currentZ = transform.position.x;
        float delta = (currentZ - initialZ) *10f; // mult by mag factor 
        currDer = delta > dirlimit? true : false;
        charAnim.SetBool("dirRL", currDer);
        if (currDer != initDir)
        {
            Debug.Log("turning"); 
            charAnim.SetTrigger("turning");
            initDir = currDer; 
        }
        speed = Mathf.Abs(delta / counter);
        if (speed <= idlingLimit)
        {
            charAnim.SetBool("idling", true); 
        }
        else
        {
            Debug.Log("moving"); 
            charAnim.SetBool("idling", false); 
        }
        charAnim.speed = Mathf.Clamp(speed, 0.5f, 1f);
        outputSpeed = charAnim.speed; 
        //yield return new WaitForSeconds(counter); // this may be reduntant (either this or the while loop above) waiting 2 times 
        isMoving = false;
    }
}
