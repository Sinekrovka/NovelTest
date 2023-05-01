using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;
using UnityEngine.UIElements;
using System;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace VNCreator
{
#if UNITY_EDITOR
    public class BaseNode : Node
    {
        public NodeData nodeData;
        public NodeViewer visuals;

        public BaseNode(NodeData _data)
        {
            nodeData = _data != null ? _data : new NodeData();
            visuals = new NodeViewer(this);
        }
    }

    public class NodeViewer : VisualElement
    {
        BaseNode node;

        public NodeViewer(BaseNode _node)
        {
            node = _node;

            VisualTreeAsset tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/VNCreator/Editor/Graph/Node/BaseNodeTemplate.uxml");
            tree.CloneTree(this);

            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/VNCreator/Editor/Graph/Node/BaseNodeStyle.uss"));

            /*Character Field*/
            VisualElement charSprDisplay = this.Query<VisualElement>("Char_Img");
            charSprDisplay.style.backgroundImage = node.nodeData.characterSpr ? node.nodeData.characterSpr.texture : null;

            ObjectField charSprField = this.Query<ObjectField>("Icon_Selection").First();
            charSprField.objectType = typeof(Sprite);
            charSprField.value = node.nodeData.characterSpr;
            charSprField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e =>
                {
                    node.nodeData.characterSpr = (Sprite)e.newValue;
                    charSprDisplay.style.backgroundImage = node.nodeData.characterSpr ? node.nodeData.characterSpr.texture : null;
                }
            );
            
            TextField charNameField = this.Query<TextField>("Char_Name");
            charNameField.value = node.nodeData.characterName;
            charNameField.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.characterName = charNameField.value;
                }
            );

            ////////////////////////////////////////////////////
            /* ЭФФЕКТЫ ПЕРСОНАЖА */
            /////////////////////////////////
            ///
            
            /*POSITION*/
            var characterPosition = this.Query<Vector2Field>("Char_Position").First();
            characterPosition.value = node.nodeData.characterPositionOnScreen;
            characterPosition.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.characterPositionOnScreen = characterPosition.value;
                }
            );
            
            /*COLOR*/

            var characterColorActive = this.Query<Toggle>("Char_Color_Active").First();
            characterColorActive.value = node.nodeData.characterColor.active;
            characterColorActive.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.characterColor.active = characterColorActive.value;
                });
            
            
            var characterColor = this.Query<ColorField>("Char_Color").First();
            characterColor.value = node.nodeData.characterColor.color;
            characterColor.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.characterColor.color = characterColor.value;
                });
            
            var characterTimeEffect = this.Query<FloatField>("Color_Time").First();
            characterTimeEffect.value = node.nodeData.characterColor.timeSecondsEffect;

            characterTimeEffect.RegisterValueChangedCallback(e =>
            {
                node.nodeData.characterColor.timeSecondsEffect = characterTimeEffect.value; 
            });
            
            /*MOVEMENT*/

            var characterMovementActive = this.Query<Toggle>("Char_Movement_Active").First();
            var characterMovementDirection = this.Query<Vector2Field>("Char_Movement_Direction").First();
            var characterTimeMovement = this.Query<FloatField>("Movement_Time").First();
            //var characterMovementType = this.Query<EnumField>("Movement_Type").First();

            characterMovementActive.value = node.nodeData.characterMovement.active;
            characterMovementDirection.value = node.nodeData.characterMovement.movementDirection;
            characterTimeMovement.value = node.nodeData.characterMovement.timeSecondEffect;

            characterMovementActive.RegisterValueChangedCallback(e =>
            {
                node.nodeData.characterMovement.active = characterMovementActive.value;
            });
            //characterMovementType = new EnumField(NodeData.TypeMovement.Standart);

            characterMovementDirection.RegisterValueChangedCallback(e =>
            {
                node.nodeData.characterMovement.movementDirection = characterMovementDirection.value;
            });

            characterTimeMovement.RegisterValueChangedCallback(e =>
            {
                node.nodeData.characterMovement.timeSecondEffect = characterTimeMovement.value;
            });
                
            // node.nodeData.characterMovement.typeMovement = characterMovementType.;
            
            /*FADE*/

            var characterFadeActive = this.Query<Toggle>("Char_Fade_Active").First();
            var characterFadeTime = this.Query<FloatField>("Char_Fade_Time").First();

            characterFadeActive.value = node.nodeData.characterFade.active;
            characterFadeTime.value = node.nodeData.characterFade.timeSecondsEffect;

            characterFadeActive.RegisterValueChangedCallback(e =>
            {
                node.nodeData.characterFade.active = characterFadeActive.value;
            });

            characterFadeTime.RegisterValueChangedCallback(e =>
            {
                node.nodeData.characterFade.timeSecondsEffect = characterFadeTime.value;
            });
            
            /////////////////////////////////////////////////////////////////
            ///
            ///
            ///          БЭКГРАУНД
            ///
            //////////////////////////////////////////////////////
            ///
            /// COLOR
            
            var backColorActive = this.Query<Toggle>("Back_Color_Active").First();
            backColorActive.value = node.nodeData.backgroundColor.active;
            backColorActive.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.backgroundColor.active = backColorActive.value;
                });
            
            
            var backColor = this.Query<ColorField>("Back_Color").First();
            backColor.value = node.nodeData.backgroundColor.color;
            backColor.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.backgroundColor.color = backColor.value;
                });
            
            var backTimeEffect = this.Query<FloatField>("Back_Time").First();
            backTimeEffect.value = node.nodeData.backgroundColor.timeSecondsEffect;

            backTimeEffect.RegisterValueChangedCallback(e =>
            {
                node.nodeData.backgroundColor.timeSecondsEffect = backTimeEffect.value; 
            });
            
            ///// FADE
            ///
            var backFadeActive = this.Query<Toggle>("Back_Fade_Active").First();
            var backFadeTime = this.Query<FloatField>("Back_Fade_Time").First();

            backFadeActive.value = node.nodeData.backgroundfade.active;
            backFadeTime.value = node.nodeData.backgroundfade.timeSecondsEffect;

            backFadeActive.RegisterValueChangedCallback(e =>
            {
                node.nodeData.backgroundfade.active = backFadeActive.value;
            });

            backFadeTime.RegisterValueChangedCallback(e =>
            {
                node.nodeData.backgroundfade.timeSecondsEffect = backFadeTime.value;
            });
            

            TextField dialogueField = this.Query<TextField>("Dialogue_Field");
            dialogueField.multiline = true;
            dialogueField.value = node.nodeData.dialogueText;
            dialogueField.RegisterValueChangedCallback(
                e =>
                {
                    node.nodeData.dialogueText = dialogueField.value;
                }
            );

            ObjectField sfxField = this.Query<ObjectField>("Sound_Field").First();
            sfxField.objectType = typeof(AudioClip);
            sfxField.value = node.nodeData.soundEffect;
            sfxField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e =>
                {
                    node.nodeData.soundEffect = (AudioClip)e.newValue;
                }
            );

            ObjectField musicField = this.Query<ObjectField>("Music_Field").First();
            musicField.objectType = typeof(AudioClip);
            musicField.value = node.nodeData.soundEffect;
            musicField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e =>
                {
                    node.nodeData.backgroundMusic = (AudioClip)e.newValue;
                }
            );

            VisualElement backSprDisplay = this.Query<VisualElement>("Back_Img");
            backSprDisplay.style.backgroundImage = node.nodeData.backgroundSpr ? node.nodeData.backgroundSpr.texture : null;

            ObjectField backSprField = this.Query<ObjectField>("Back_Selector").First();
            backSprField.objectType = typeof(Sprite);
            backSprField.value = node.nodeData.backgroundSpr;
            backSprField.RegisterCallback<ChangeEvent<UnityEngine.Object>>(
                e =>
                {
                    node.nodeData.backgroundSpr = (Sprite)e.newValue;
                    backSprDisplay.style.backgroundImage = node.nodeData.backgroundSpr ? node.nodeData.backgroundSpr.texture : null;
                }
            );
        }
    }
#endif
}
