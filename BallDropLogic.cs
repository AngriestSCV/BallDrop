
using System;
using System.Reflection;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace AngryLabs.Props.BallDrop
{
    public class BallDropLogic: UdonSharpBehaviour
    {
        public Animator animator;

        public void Trigger()
        {
            if(animator != null)
            {
                animator.SetTrigger("dropBall");
            }

            SendCustomEventDelayedSeconds(nameof(UnTrigger), 0.1f);
        }

        public void UnTrigger()
        {
            if(animator != null)
            {
                animator.ResetTrigger("dropBall");
            }
        }

    }
}