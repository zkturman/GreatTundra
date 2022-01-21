using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PausibleObject
{
    public bool ObjectIsPaused { get; }
    public void SetObjectPauseFlag(bool pauseStatusToSet);
}
