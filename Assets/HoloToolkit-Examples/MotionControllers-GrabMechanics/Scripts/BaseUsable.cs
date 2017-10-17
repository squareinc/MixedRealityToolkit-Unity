﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace HoloToolkit.Unity.InputModule.Examples.Grabbables
{
    /// <summary>
    /// A usable object is one that can be "used" or activated while being grabbed/carried
    /// A gun and a remote control are examples: first grab, then press a different button to use
    /// </summary>
    public abstract class BaseUsable : MonoBehaviour
    {
        [SerializeField]
        private InteractionSourceHandedness handedness;

        /// <summary>
        /// Assign a controller button to "use" the object
        /// </summary>
        [SerializeField]
        private InteractionSourcePressType pressType;

        private UseStateEnum state;

        public UseStateEnum UseState
        {
            get { return state; }
        }

        protected virtual void UseStart()
        {
            //Child do something
        }

        protected virtual void UseEnd()
        {
            //Child do something
        }

        //Subscribe GrabStart and GrabEnd to InputEvents for Grip

        protected virtual void OnEnable()
        {
            InteractionManager.InteractionSourcePressed += UseInputStart;
            InteractionManager.InteractionSourceReleased += UseInputEnd;
        }

        protected virtual void OnDisable()
        {
            InteractionManager.InteractionSourcePressed -= UseInputStart;
            InteractionManager.InteractionSourceReleased -= UseInputEnd;
        }

        private void UseInputStart(InteractionSourcePressedEventArgs obj)
        {
            if (/*obj.pressType == pressType && (*/handedness == InteractionSourceHandedness.Unknown || handedness == obj.state.source.handedness)
            {

                if (GetComponent<BaseGrabbable>().GrabState == GrabStateEnum.Single)
                {
                    state = UseStateEnum.Active;
                    UseStart();
                }
            }
        }

        private void UseInputEnd(InteractionSourceReleasedEventArgs obj)
        {
            if (/*obj.pressType == pressType && */obj.state.source.handedness == handedness)
            {
                state = UseStateEnum.Inactive;
                UseEnd();
            }
        }
    }
}