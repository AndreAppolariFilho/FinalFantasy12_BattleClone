using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelState 
{
    // Start is called before the first frame update
    public enum HUDstate
    {
        actions,
        attacks,
        targets
    }
    public enum Event
    {
        ENTER,
        UPDATE,
        EXIT
    }
    public Event actualEvent;
    [SerializeField] protected PanelState nextState;
    public PanelState()
    {
        actualEvent = Event.ENTER;
    }
    public virtual void Enter()
    {
        actualEvent = Event.ENTER;
    }
    public virtual void Update()
    {
        actualEvent = Event.UPDATE;
    }
    public virtual void Exit()
    {
        actualEvent = Event.EXIT;
    }
    public PanelState Process()
    {
        if(actualEvent == Event.ENTER)
        {
            this.Enter();
        }
        if(actualEvent == Event.UPDATE)
        {
            this.Update();
        }
        if(actualEvent == Event.EXIT)
        {
            this.Exit();
        }
        return nextState;
    }
}
public class ActionPanelState : PanelState
{
    public ActionPanelState()
    {
        
    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}

public class AttackPanelState : PanelState
{
    public AttackPanelState()
    {

    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}

public class TargetPanelState : PanelState
{
    public TargetPanelState()
    {

    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
