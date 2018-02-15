using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class NextSlide : MonoBehaviour {

    public UnityEvent m_MyEvent;
    public float TimeToMove=15f;

    void Start()
    {
        StartCoroutine("perehod_slide");
    }
    public IEnumerator perehod_slide()
    {
        yield return new WaitForSeconds(TimeToMove);
        m_MyEvent.Invoke();
        Debug.Log("m_MyEvent.Invoke();");

    }
}
