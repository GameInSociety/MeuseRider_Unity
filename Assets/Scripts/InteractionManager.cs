using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public bool PressAny()
    {
        return PressTrigger(UnityEngine.XR.XRNode.LeftHand)||PressTrigger(UnityEngine.XR.XRNode.RightHand) || Input.GetKeyDown(KeyCode.I);
    }

    public bool PressTrigger(UnityEngine.XR.XRNode hand)
    {
        var handDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(hand, handDevices);

        if (handDevices.Count == 1)
        {
            UnityEngine.XR.InputDevice device = handDevices[0];
            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log($"{hand} trigger button is pressed.");
                return true;
            }
        }
        else if (handDevices.Count > 1)
        {
            Debug.Log($"Found more than one {hand} hand!");
        }

        return false;
    }
}
