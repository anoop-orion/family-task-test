using System;

public class TaskModel
{
    public Guid id {get; set;}
    public FamilyMember member { get; set; }
    public string text { get; set; }
    public bool isDone { get; set; }

    protected virtual void OnClickCallback(object e)
    {
        EventHandler<object> handler = ClickCallback;
        if (handler != null)
        {
            handler(this, e);
        }        
    }
    public event EventHandler<object> ClickCallback;
    public void InvokClickCallback(object e, string callback =null)
    {      
        OnClickCallback(e);
    }
}
