using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class HandPunch : MonoBehaviour
{
    public OVRHand hand;
    public GameObject punchCollider;

    private void Update()
    {
        float indexPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);
        float middlePinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Middle);
        float ringPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Ring);
        float pinkyPinch = hand.GetFingerPinchStrength(OVRHand.HandFinger.Pinky);

        // ≥◊ º’∞°∂Ù¿Ã ¥Ÿ ¡¢»˜∏È ¡÷∏‘ ¡Â ∞…∑Œ ∆«¡§
        if (indexPinch > 0.8f && middlePinch > 0.8f && ringPinch > 0.8f && pinkyPinch > 0.8f)
        {
            Debug.Log("¡÷∏‘ ¡Á!");
            punchCollider.SetActive(true);
        }
        else
        {
            punchCollider.SetActive(false);
        }
    }
}
