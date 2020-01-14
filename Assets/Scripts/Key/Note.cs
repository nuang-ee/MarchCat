using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Note : MonoBehaviour
{
    private Toggle toggle;
    private PatternHandler patternHandler;

    private void Start()
    {
        toggle = GetComponentInChildren<Toggle>();
        patternHandler = GetComponentInParent<PatternHandler>();

        EventTrigger trigger = toggle.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry1 = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entry1.callback.AddListener((data) => { OnMouseEnter_fun((PointerEventData)data); });
        print(trigger);
        print(trigger.triggers);
        print(entry1);
        trigger.triggers.Add(entry1);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { OnMouseExit_fun(); });
        trigger.triggers.Add(entry2);

        toggle.onValueChanged.AddListener(delegate
        {
            patternHandler.SetSequencerNote(toggle);
        });
    }
    void OnMouseEnter_fun(PointerEventData data)
    {
        print("THIS!!!");
        patternHandler.SetPositionText(toggle);
    }

    void OnMouseExit_fun()
    {
        patternHandler.cleanPositionText();
    }

    private void Update()
    {
        
    }

}
