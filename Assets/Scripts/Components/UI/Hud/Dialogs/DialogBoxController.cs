using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;
        [Space] [SerializeField] private float _typeSpeed = 0.09f;


        [Header("Audios")]
        [SerializeField] private AudioClip _typing;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;


        [Space][SerializeField] protected DialogContent _dialogContent;
        
        private DialogData _data;
        private int _currentSentense;
        private AudioSource _sfxSource;
        protected virtual DialogContent CurrentContent => _dialogContent;


        protected Sentence CurrentSentence => _data.Sentence[_currentSentense];

        private Coroutine _typingCoroutine;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;
            _currentSentense = 0;
            CurrentContent.Text.text = string.Empty;

            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool("IsOpen", true);
        }


        protected virtual void OnStartDialogAnimation()
        {
            _typingCoroutine = StartCoroutine(TypeDialogText());
        }

        private void StopTypingAnimation()
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
                _typingCoroutine = null;
            }
        }

        public void OnContinue()
        {
            StopTypingAnimation();

            _currentSentense++;

            var isDialogComplete = _currentSentense >= _data.Sentence.Length;
            if (isDialogComplete)
            {
                HideDialog();
            }
            else
            {
                OnStartDialogAnimation();
            }
        }

        public void OnSkip()
        {
            if(_typingCoroutine == null) return;
            StopTypingAnimation();

             CurrentContent.Text.text = _data.Sentence[_currentSentense].Value;
        }

        private void HideDialog()
        {
            _animator.SetBool("IsOpen", false);
            _sfxSource.PlayOneShot(_close);
        }

        private IEnumerator TypeDialogText()
        {
            CurrentContent.Text.text = string.Empty;
            var sentenses = _data.Sentence[_currentSentense];
            foreach (var letter in sentenses.Value)
            {
                CurrentContent.Text.text +=letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_typeSpeed);
            }

            _typingCoroutine = null;
        }
    }
}
