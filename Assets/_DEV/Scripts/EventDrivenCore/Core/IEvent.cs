using System;

namespace LastHand
{
  public abstract class IEvent
  {
    private readonly string name;

    protected IEvent(string name)
    {
      this.name = name;
    }

    protected void ThrowSubscribeException()
    {
      UnityEngine.Debug.LogException(new Exception("Event duplicate subscription detected: " + name));
    }
  }
}
