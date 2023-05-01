using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace VNCreator
{
    public class VNCreator_DisplayUI : DisplayBase
    {
        [Header("Text")]
        public TextMeshProUGUI characterNameTxt;
        public TextMeshProUGUI dialogueTxt;
        [Header("Visuals")]
        public Image characterImg;
        public Image backgroundImg;
        [Header("Audio")]
        public AudioSource musicSource;
        public AudioSource soundEffectSource;
        [Header("Buttons")]
        public Button nextBtn;
        public Button previousBtn;
        public Button saveBtn;
        public Button menuButton;
        [Header("Choices")] 
        [SerializeField] private GameObject buttonContainer;
        public Button choiceBtn1;
        public Button choiceBtn2;
        public Button choiceBtn3;
        [Header("End")]
        public GameObject endScreen;
        [Header("Main menu")]
        [Scene]
        public string mainMenu;

        [SerializeField] private bool _isSecondCharacter;

        void Start()
        {
            nextBtn.onClick.AddListener(delegate { NextNode(0); });
            if(previousBtn != null)
                previousBtn.onClick.AddListener(Previous);
            if(saveBtn != null)
                saveBtn.onClick.AddListener(Save);
            if (menuButton != null)
                menuButton.onClick.AddListener(ExitGame);

            if(choiceBtn1 != null)
                choiceBtn1.onClick.AddListener(delegate { NextNode(0); });
            if(choiceBtn2 != null)
                choiceBtn2.onClick.AddListener(delegate { NextNode(1); });
            if(choiceBtn3 != null)
                choiceBtn3.onClick.AddListener(delegate { NextNode(2); });

            if (!_isSecondCharacter)
            {
                endScreen.SetActive(false);
            }
            LoadNode(); 
        }

        protected override void NextNode(int _choiceId)
        {
            if (lastNode && !_isSecondCharacter)
            {
                endScreen.SetActive(true);
                return;
            }

            base.NextNode(_choiceId);
            LoadNode();
        }

        private void LoadNode()
        {
            ParseCharacterSprite();
            
            if (!_isSecondCharacter)
            {
                ParseCharacterName();
                ParseBackgroundSprite();
                ParseSounds();
                ParseButtons();
                StartCoroutine(ParseDialogue());
            }
        }

        #region ParseNode
            
        IEnumerator ParseDialogue()
        {
            dialogueTxt.text = string.Empty;
            if (GameOptions.isInstantText)
            {
                dialogueTxt.text = currentNode.dialogueText;
            }
            else
            {
                char[] _chars = currentNode.dialogueText.ToCharArray();
                string fullString = string.Empty;
                for (int i = 0; i < _chars.Length; i++)
                {
                    fullString += _chars[i];
                    dialogueTxt.text = fullString;
                    yield return new WaitForSeconds(0.01f/ GameOptions.readSpeed);
                }
            }
        }
        
        private void ParseButtons()
        {
            if (currentNode.choices <= 1) 
            {
                nextBtn.gameObject.SetActive(true);

                buttonContainer.SetActive(false);
                previousBtn.gameObject.SetActive(loadList.Count != 1);
            }
            else
            {
                nextBtn.gameObject.SetActive(false);
                buttonContainer.SetActive(true);

                choiceBtn1.transform.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choiceOptions[0];
                choiceBtn2.transform.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choiceOptions[1];

                if (currentNode.choices == 3)
                {
                    choiceBtn3.gameObject.SetActive(true);
                    choiceBtn3.transform.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choiceOptions[2];
                }
                else
                {
                    choiceBtn3.gameObject.SetActive(false);
                }
            }
        }

        private void ParseSounds()
        {
            if (currentNode.backgroundMusic != null)
                VNCreator_MusicSource.instance.Play(currentNode.backgroundMusic);
            if (currentNode.soundEffect != null)
                VNCreator_SfxSource.instance.Play(currentNode.soundEffect);
        }

        private void ParseCharacterName()
        {
            characterNameTxt.text = currentNode.characterName;
        }

        private void ParseCharacterSprite()
        {
            if (currentNode.characterSpr != null)
            {
                characterImg.sprite = currentNode.characterSpr;
                characterImg.color = Color.white;
                characterImg.rectTransform.anchoredPosition = currentNode.characterPositionOnScreen;
                CheckEffectsCharacter();
            }
            else
            {
                characterImg.color = new Color(1, 1, 1, 0);
            }
        }

        private void ParseBackgroundSprite()
        {
            if (currentNode.backgroundSpr != null)
            {
                backgroundImg.sprite = currentNode.backgroundSpr;
                CheckEffectsBackground();
            }
        }
        
        #endregion

        protected override void Previous()
        {
            base.Previous();
            if (!_isSecondCharacter)
            {
                LoadNode();
            }
        }

        void ExitGame()
        {
            SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }


        #region Effects

        private void CheckEffectsCharacter()
        {
            if (currentNode.characterColor.active)
            {
                Effector.Instance.SetImageColor(characterImg, currentNode.characterColor.color, 
                    currentNode.characterColor.timeSecondsEffect);
            }

            if (currentNode.characterFade.active)
            {
                Effector.Instance.SetImageFade(characterImg, currentNode.characterFade.timeSecondsEffect);
            }

            if (currentNode.characterMovement.active)
            {
                Effector.Instance.SetImageMovement(characterImg.rectTransform, 
                    currentNode.characterMovement.movementDirection, 
                    currentNode.characterPositionOnScreen, 
                    currentNode.characterMovement.timeSecondEffect);
            }
        }

        private void CheckEffectsBackground()
        {
            if (currentNode.backgroundColor.active)
            {
                Effector.Instance.SetImageColor(backgroundImg, currentNode.backgroundColor.color, 
                    currentNode.backgroundColor.timeSecondsEffect);
            }

            if (currentNode.backgroundfade.active)
            {
                Effector.Instance.SetImageFade(backgroundImg, currentNode.backgroundfade.timeSecondsEffect);
            }
        }

        #endregion
    }
}