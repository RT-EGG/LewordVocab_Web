using System;
using System.Linq;
using System.Threading.Tasks;

namespace LewordVocab.Shared
{
    public partial class MainContents
    {
        private void WordLetterPanelClicked(WordLetterPanelClickEventArgs inArgs)
        {
            if ((inArgs.Panel != null) && (CurrentPanel == inArgs.Panel)) {
                CurrentPanel.Selected = false;
                CurrentPanel = null;
            } else {
                if (CurrentPanel != null) {
                    CurrentPanel.Selected = false;
                }

                CurrentPanel = inArgs.Panel;
                CurrentPanel.Selected = true;
            }
        }

        private async Task KeyboardKeyClicked(VirtualKeyboardKey inKey)
        {
            if (inKey is VirtualKeyboardEnterKey) {
                await ExecuteSearch();
                return;
            }

            if (CurrentPanel != null) {
                var oldKey = Keyboard.GetKey(CurrentPanel.Letter);
                if (oldKey != null) {
                    char letter = oldKey.Letter;
                    if (!Enumerable.Range(0, WordLetterPanels.PanelCount)
                                  .Select(index => WordLetterPanels[index])
                                  .Any(panel => (panel != CurrentPanel) && (panel.Letter == letter))) {
                        oldKey.LetterState = LetterState.Unknown;
                    }
                }

                switch (inKey) {
                    case VirtualKeyboardDeleteKey delete:
                        CurrentPanel.Letter = ' ';
                        break;

                    default:
                        CurrentPanel.Letter = inKey.Letter;
                        inKey.LetterState = LetterState.Correct;
                        break;
                }

                CurrentPanel.Selected = false;
                CurrentPanel = null;
            } else {
                switch (inKey) {
                    case VirtualKeyboardDeleteKey delete:
                        break;

                    default:
                        switch (inKey.LetterState) {
                            case LetterState.Unknown:
                                inKey.LetterState = LetterState.Absent;
                                break;
                            case LetterState.Absent:
                                inKey.LetterState = LetterState.Present;
                                break;
                            case LetterState.Present:
                                inKey.LetterState = LetterState.Unknown;
                                break;
                        }
                        break;
                }
            }
        }

        private async Task ExecuteSearch()
        {
            await Task.Yield();
        }

        private void UpdateKeyboardState()
        {
            string word = "";
            for (int i = 0; i < Math.Min(WordLength, WordLetterPanels.PanelCount); ++i) {
                word += WordLetterPanels[i].Letter;
            }
            word = word.ToUpper();

            foreach (char letter in UpperLetters) {
                var key = Keyboard.GetKey(letter);
                if (key.LetterState == LetterState.Correct) {
                    if (!word.Contains(letter)) {
                        key.LetterState = LetterState.Unknown;
                    }
                }
            }
        }

        private int WordLength
        {
            get => m_WordLength;
            set {
                if (m_WordLength != value) {
                    m_WordLength = value;
                    UpdateKeyboardState();
                }
            }
        }
        
        private WordLetterPanels WordLetterPanels
        { get; set; } = null;
        private VirtualKeyboard Keyboard
        { get; set; } = null;

        private WordLetterPanel CurrentPanel
        { get; set; } = null;

        private int m_WordLength = 5;

        private const string UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    }
}
