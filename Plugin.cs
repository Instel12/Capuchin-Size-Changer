using BepInEx;
using BepInEx.Unity.IL2CPP;
using Locomotion;
using UnityEngine;
using UnityEngine.XR;

namespace CapuchinTemplate
{
    [BepInPlugin("instel.sizechanger", "Size Changer", "1.0.0")]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            AddComponent<PluginBehaviour>();
        }
    }

    public class PluginBehaviour : MonoBehaviour
    {
        float size = 1f;
        bool ismodded = false;
        void Start()
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

        void Update()
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
