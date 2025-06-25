using BepInEx;
using BepInEx.Unity.IL2CPP;
using Locomotion;
using UnityEngine;
using UnityEngine.XR;

[assembly: MelonInfo(typeof(Plugin), "Size Changer", "1.0.0", "Instel")]

namespace CapuchinTemplate
{
    public class Plugin : MelonMod
    {
        float size = 1f;
        bool ismodded = false;
        
        public override void OnMelonInitialize()
        {
            Caputilla.CaputillaManager.Instance.OnModdedJoin += OnJoinedModded;
            Caputilla.CaputillaManager.Instance.OnModdedLeave += OnLeaveModded;
        }

        void OnJoinedModded()
        {
            ismodded = true;
        }

        void OnLeaveModded()
        {
            ismodded = false;
        }

        public override void OnUpdate()
        {
            InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            if (ismodded)
            {
                Player.Instance.transform.localScale = new Vector3(size, size, size);
                if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool righttrigger) && righttrigger)
                {
                    size += 0.1f;
                }
                if (rightHand.TryGetFeatureValue(CommonUsages.gripButton, out bool rightgrip) && rightgrip)
                {
                    size -= 0.1f;
                }
                if (leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftprimary) && leftprimary)
                {
                    size = 0f;
                    Player.Instance.transform.localScale = new Vector3(size, size, size);
                }
            }
            else
            { 
                size = 1f;
            }
        }
    }
}
