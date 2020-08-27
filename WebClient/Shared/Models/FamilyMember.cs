using System;

public class FamilyMember
{
    public Guid id {get; set;}
    public string firstname { get; set; }
    public string lastname { get; set; }
    public string email { get; set; }
    public string role { get; set; }
    public string avtar { get; set; }

    protected virtual void OnClickCallback(object e)
    {
        EventHandler<object> handler = ClickCallback;
        if (handler != null)
        {
            handler(this, e);
        }
    }
    public event EventHandler<object> ClickCallback;
    public void InvokClickCallback(object e)
    {
        OnClickCallback(e);
    }
}
