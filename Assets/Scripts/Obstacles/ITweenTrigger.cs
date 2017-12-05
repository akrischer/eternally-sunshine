using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ITweenTrigger : MonoBehaviour {

    public bool cycle;
    public bool cycleFromBeginning;
    public List<string> itweenEventIds;

    private int currentEventIdPtr;

	public void TriggerITween()
    {
        if (currentEventIdPtr < itweenEventIds.Count)
        {
            TriggerEvent(itweenEventIds[currentEventIdPtr]);
        } else if (cycle)
        {
            if (cycleFromBeginning)
            {
                currentEventIdPtr = 0;
                TriggerEvent(itweenEventIds[currentEventIdPtr]);
            } else
            {
                currentEventIdPtr -= 2; // Add 2 because the ptr is already past the last element in the list
                TriggerEvent(itweenEventIds[currentEventIdPtr]);
            }
        }
    }

    public void TriggerITweenByName(string eventId)
    {
        iTweenEvent e = iTweenEvent.GetEvent(gameObject, eventId);
        if (e != null)
        {
            e.Play();
        } else
        {
            Debug.LogWarning("No iTween event id found with the name, '" + eventId + "'", gameObject);
        }
    }

    private void TriggerEvent(string eventId)
    {
        iTweenEvent e = iTweenEvent.GetEvent(gameObject, eventId);
        e.Play();
        currentEventIdPtr++;
    }
}
