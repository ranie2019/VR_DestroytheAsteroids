﻿using UnityEngine;
using System;

namespace VRKeys
{
    /// <summary>
    /// Manages placement of the keyboard relative to the user, including
    /// grabbing it to move and resize the keyboard.
    /// </summary>
    public class Placement : MonoBehaviour
    {

        [Serializable]
        public class PlacementSettings
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;
        }

        public float minScale = 0.4f;
        public float maxScale = 1.2f;

        public Mallet leftMallet;
        public Mallet rightMallet;
        public HandCollider leftHandCollider;
        public HandCollider rightHandCollider;

        public PlacementSettings settings;

        private Keyboard keyboard;

        private Mallet.MalletHand positioningHand;

        private Vector3 previousPosition;
        private float previousRotationX;

        private Vector3 initialScale;
        private float applyScale = 1f;

        private float initialHandDistance = -1f;
        private float initialApplyScale = 1f;

        private string prefsKey = "vrkeys:placement";

        void Awake()
        {
            // Tentando obter o componente Keyboard
            keyboard = GetComponent<Keyboard>();

            // Verificação para garantir que o componente Keyboard foi encontrado
            if (keyboard == null)
            {
                Debug.LogError("O componente Keyboard não foi encontrado no objeto.");
                return;  // Impede a execução do código a seguir, se o Keyboard não estiver presente
            }

            // Verifica se as preferências do usuário existem e aplica as configurações
            if (PlayerPrefs.HasKey(prefsKey))
            {
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(prefsKey), settings);
            }

            // Verifica se o keyboardWrapper está atribuído
            if (keyboard.keyboardWrapper != null)
            {
                keyboard.keyboardWrapper.transform.localPosition = settings.position;
                keyboard.keyboardWrapper.transform.localRotation = settings.rotation;
                keyboard.keyboardWrapper.transform.localScale = settings.scale;
            }
            else
            {
                Debug.LogError("O objeto 'keyboardWrapper' não foi atribuído corretamente no componente Keyboard.");
            }

            initialScale = keyboard.keyboardWrapper.transform.localScale;
        }

        void Update()
        {
            if (leftHandCollider == null || rightHandCollider == null || keyboard.keyboardWrapper == null)
            {
                return;
            }

            if (!leftHandCollider.inKeyboard && !rightHandCollider.inKeyboard)
            {
                ResetState();
                return;
            }
            ;

            bool leftGrip = false;
            bool rightGrip = false;
            bool changed = false;

            if (leftMallet.Controller() != null)
            {
                leftGrip = leftMallet.Controller().OnGrip();
            }

            if (rightMallet.Controller() != null)
            {
                rightGrip = rightMallet.Controller().OnGrip();
            }

            // Positioning
            if (leftGrip && rightGrip)
            {
                var handVector = (leftHandCollider.transform.position + rightHandCollider.transform.position) / 2f;
                PositionWith(handVector, leftHandCollider.transform.rotation, Mallet.MalletHand.Both);
            }
            else if (leftGrip)
            {
                PositionWith(leftHandCollider.transform.position, leftHandCollider.transform.rotation, Mallet.MalletHand.Left);
                changed = true;
            }
            else if (rightGrip)
            {
                PositionWith(rightHandCollider.transform.position, rightHandCollider.transform.rotation, Mallet.MalletHand.Right);
                changed = true;
            }

            // Resizing
            if (leftGrip && rightGrip)
            {
                Resize();
                changed = true;
            }

            if (changed)
            {
                SaveChanges();
            }
            else
            {
                ResetState();
            }
        }

        void PositionWith(Vector3 handPosition, Quaternion handRotation, Mallet.MalletHand hand)
        {
            if (positioningHand != hand)
            {
                positioningHand = hand;
                previousPosition = handPosition;
                previousRotationX = handRotation.eulerAngles.x;
            }

            if (keyboard.keyboardWrapper != null)
            {
                var pos = keyboard.keyboardWrapper.transform.localPosition;

                keyboard.keyboardWrapper.transform.localPosition = new Vector3(
                    pos.x + (handPosition.x - previousPosition.x),
                    pos.y + (handPosition.y - previousPosition.y),
                    pos.z + (handPosition.z - previousPosition.z)
                );

                var rot = keyboard.keyboardWrapper.transform.eulerAngles;

                keyboard.keyboardWrapper.transform.localRotation = Quaternion.Euler(
                    rot.x + (handRotation.eulerAngles.x - previousRotationX),
                    rot.y,
                    rot.z
                );

                previousPosition = handPosition;
                previousRotationX = handRotation.eulerAngles.x;
            }
        }

        void Resize()
        {
            if (initialHandDistance == -1f)
            {
                initialHandDistance = Vector3.Distance(leftHandCollider.transform.position, rightHandCollider.transform.position);
                initialApplyScale = applyScale;
                return;
            }

            var curDistance = Vector3.Distance(leftHandCollider.transform.position, rightHandCollider.transform.position);

            applyScale = initialApplyScale + (initialApplyScale * (curDistance - initialHandDistance));

            if (applyScale < minScale)
            {
                applyScale = minScale;
            }
            else if (applyScale > maxScale)
            {
                applyScale = maxScale;
            }

            if (keyboard.keyboardWrapper != null)
            {
                keyboard.keyboardWrapper.transform.localScale = initialScale * applyScale;
            }
        }

        void ResetState()
        {
            initialHandDistance = -1f;
            initialApplyScale = 1f;
            positioningHand = Mallet.MalletHand.None;
        }

        void SaveChanges()
        {
            if (keyboard.keyboardWrapper != null)
            {
                settings.position = keyboard.keyboardWrapper.transform.localPosition;
                settings.rotation = keyboard.keyboardWrapper.transform.localRotation;
                settings.scale = keyboard.keyboardWrapper.transform.localScale;

                PlayerPrefs.SetString(prefsKey, JsonUtility.ToJson(settings));
            }
        }
    }
}
