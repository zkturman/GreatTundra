using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PausibleObject
{
    public bool PauseStatus { get; }
    public void SetObjectPauseFlag(bool pauseStatusToSet);
}
