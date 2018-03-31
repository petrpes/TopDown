using UnityEngine;

public static class MonoExtentions
{
    public static void SafeSetEnabled(this MonoBehaviour mono, bool enabled)
    {
        if (mono != null)
        {
            mono.enabled = enabled;
        }
    }
}

