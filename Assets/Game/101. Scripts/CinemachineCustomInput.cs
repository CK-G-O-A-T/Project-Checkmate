﻿#define CINEMACHINE_UNITY_INPUTSYSTEM

#if CINEMACHINE_UNITY_INPUTSYSTEM
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XInput;

namespace Cinemachine
{
    /// <summary>
    /// This is an add-on to override the legacy input system and read input using the
    /// UnityEngine.Input package API.  Add this behaviour to any CinemachineVirtualCamera 
    /// or FreeLook that requires user input, and drag in the the desired actions.
    /// </summary>
    public class CinemachineCustomInput : MonoBehaviour, AxisState.IInputAxisProvider
    {
        /// <summary>
        /// Leave this at -1 for single-player games.
        /// For multi-player games, set this to be the player index, and the actions will
        /// be read from that player's controls
        /// </summary>
        [Tooltip("Leave this at -1 for single-player games.  "
            + "For multi-player games, set this to be the player index, and the actions will "
            + "be read from that player's controls")]
        public int PlayerIndex = -1;

        /// <summary>Vector2 action for XY movement</summary>
        [Tooltip("Vector2 action for XY movement")]
        public InputActionReference XYAxis;

        /// <summary>Float action for Z movement</summary>
        [Tooltip("Float action for Z movement")]
        public InputActionReference ZAxis;

        float deltaTime = 0.016f;

        void Update()
        {
            deltaTime = Time.deltaTime;
        }

        //bool fixedUpdated;
        //private void OnGUI()
        //{
        //    GUI.Label(new Rect(0, 0, 100, 50), fixedUpdated ? "FixedUpdate" : "LateUpdate");
        //}

        //void FixedUpdate()
        //{
        //    deltaTime = Time.fixedDeltaTime;
        //}

        //void LateUpdate()
        //{
        //    deltaTime = Time.deltaTime;
        //}



        public virtual float GetAxisValue(int axis)
        {
            if (TimeManager.Instance != null && !TimeManager.Instance.IsPause)
            {
                var action = ResolveForPlayer(axis, axis == 2 ? ZAxis : XYAxis);

                var deltaCorrection = 1f;

                if (action.controls[0].device is Mouse)
                {
                    // 현재 FixedUpdate인지 감지
                    if (deltaTime != Time.deltaTime)
                    {
                        deltaCorrection = Time.fixedDeltaTime / deltaTime;
                    }
                }
                else if (action.controls[0].device is Gamepad)
                {
                    // 현재 LateUpdate인지 감지
                    if (deltaTime == Time.deltaTime)
                    {
                        deltaCorrection = deltaTime / 0.016f;
                    }
                }

                if (float.IsNaN(deltaCorrection) || float.IsInfinity(deltaCorrection))
                {
                    deltaCorrection = 0;
                }
                //fixedUpdated = deltaTime != Time.deltaTime;
                //Debug.Log($"{action.controls[0].device.GetType()} {deltaCorrection}");
                //Debug.Log($"action.ReadValue<Vector2>().x : {action.ReadValue<Vector2>().x}");
                //Debug.Log($"action.ReadValue<Vector2>().y : {action.ReadValue<Vector2>().y}");
                if (action != null)
                {
                    switch (axis)
                    {
                        case 0: return action.ReadValue<Vector2>().x * deltaCorrection;
                        case 1: return action.ReadValue<Vector2>().y * deltaCorrection;
                        case 2: return action.ReadValue<float>() * deltaCorrection;
                    }
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        const int NUM_AXES = 3;
        InputAction[] m_cachedActions;

        /// <summary>
        /// In a multi-player context, actions are associated with specific players
        /// This resolves the appropriate action reference for the specified player.
        /// 
        /// Because the resolution involves a search, we also cache the returned 
        /// action to make future resolutions faster.
        /// </summary>
        /// <param name="axis">Which input axis (0, 1, or 2)</param>
        /// <param name="actionRef">Which action reference to resolve</param>
        /// <returns>The cached action for the player specified in PlayerIndex</returns>
        protected InputAction ResolveForPlayer(int axis, InputActionReference actionRef)
        {
            if (axis < 0 || axis >= NUM_AXES)
                return null;
            if (actionRef == null || actionRef.action == null)
                return null;
            if (m_cachedActions == null || m_cachedActions.Length != NUM_AXES)
                m_cachedActions = new InputAction[NUM_AXES];
            if (m_cachedActions[axis] != null && actionRef.action.id != m_cachedActions[axis].id)
                m_cachedActions[axis] = null;
            if (m_cachedActions[axis] == null)
            {
                m_cachedActions[axis] = actionRef.action;
                if (PlayerIndex != -1)
                {
                    var user = InputUser.all[PlayerIndex];
                    m_cachedActions[axis] = user.actions.First(x => x.id == actionRef.action.id);
                }
            }
            // Auto-enable it if disabled
            if (m_cachedActions[axis] != null && !m_cachedActions[axis].enabled)
                m_cachedActions[axis].Enable();

            return m_cachedActions[axis];
        }

        // Clean up
        protected virtual void OnDisable()
        {
            m_cachedActions = null;
        }
    }
}
#else
using UnityEngine;

namespace Cinemachine
{
    [AddComponentMenu("")] // Hide in menu
    public class CinemachineInputProvider : MonoBehaviour { }
}
#endif
