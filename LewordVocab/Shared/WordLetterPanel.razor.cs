using Microsoft.AspNetCore.Components;
using System;

namespace LewordVocab.Shared
{
    public partial class WordLetterPanel
    {
        [Parameter]
        public WordLetterPanels Parent
        { get; set; } = null;

        [Parameter]
        public int Index
        { get; set; } = -1;

        public char Letter
        {
            get => m_Letter;
            set {
                if (m_Letter != value) {
                    m_Letter = value;
                    StateHasChanged();
                }
            }
        }
        
        public LetterState LetterState
        {
            get => m_LetterState;
            set {
                if (m_LetterState != value) {
                    m_LetterState = value;
                    StateHasChanged();
                }
            }
        }

        [Parameter]
        public int Height
        { get; set; } = 150;

        [Parameter]
        public EventCallback<WordLetterPanel> OnClick
        { get; set; } = EventCallback<WordLetterPanel>.Empty;

        public bool Selected
        {
            get => m_Selected;
            set {
                if (m_Selected != value) {
                    m_Selected = value;
                    StateHasChanged();
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Parent != null) {
                Parent.SetChild(this);
            }
        }

        private async void PanelClick()
        {
            Console.WriteLine(Letter);
            if (OnClick.HasDelegate) {
                await OnClick.InvokeAsync(this);
            }
        }

        private char m_Letter = ' ';
        private LetterState m_LetterState = LetterState.Absent;
        private bool m_Selected = false;
    }
}
