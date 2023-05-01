using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace VNCreator
{
    [Serializable]
    public class NodeData
    {
        public string guid;
        [Header("Character Settings")]
        public Sprite characterSpr;
        public Vector2 characterPositionOnScreen;
        public string characterName;
        public string dialogueText;
        public EffectCharacterMovement characterMovement;
        public EffectImageDoFade characterFade;
        public EffectImageColor characterColor;
        [Header("Background Settings")]
        public Sprite backgroundSpr;
        public EffectImageColor backgroundColor;
        public EffectImageDoFade backgroundfade;
        [Header("Sounds")]
        public AudioClip soundEffect;
        public AudioClip backgroundMusic;
        [Space]
        public List<string> choiceOptions;
        [Space]
        public bool startNode;
        public bool endNode;
        public int choices = 1;
        public Rect nodePosition;
        
        

        public NodeData()
        {
            guid = Guid.NewGuid().ToString();
            characterMovement = new EffectCharacterMovement();
            characterColor = new EffectImageColor();
            characterFade = new EffectImageDoFade();
            backgroundColor = new EffectImageColor();
            backgroundfade = new EffectImageDoFade();
        }
       
        [Serializable]
        public class EffectCharacterMovement
        {
            public bool active;
            public Vector2 movementDirection;
            public float timeSecondEffect;
            public TypeMovement typeMovement;

            public EffectCharacterMovement()
            {
                active = false;
                movementDirection=Vector2.zero;
                timeSecondEffect = 0;
                typeMovement = TypeMovement.Standart;
            }
        }

        [Serializable]
        public enum TypeMovement
        {
            Standart,
            Linear,
        }

        /*[Serializable]
        public class EffectCharacterShake
        {
            public bool active;
        }*/
        
        [Serializable]
        public class  EffectImageDoFade
        {
            public bool active;
            public float timeSecondsEffect;

            public EffectImageDoFade()
            {
                active = false;
                timeSecondsEffect = 0;
            }
        }

        [Serializable]
        public class EffectImageColor
        {
            public bool active;
            public Color color;
            public float timeSecondsEffect;

            public EffectImageColor()
            {
                active = false;
                color = Color.white;
                timeSecondsEffect = 0f;
            }
        }
        
    }
}