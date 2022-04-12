using Microsoft.AspNetCore.Components;

namespace LewordVocab.Shared
{
    public partial class VirtualKeyboardKey
    {
        [Parameter]
        public VirtualKeyboard Parent
        { get; set; } = null;

        [Parameter]
        public EventCallback<VirtualKeyboardKey> OnClick
        { get; set; } = EventCallback<VirtualKeyboardKey>.Empty;

        [Parameter]
        public char Letter
        { get; set; }

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

        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Parent != null) {
                Parent.AddChild(this);
            }
        }

        protected async void KeyClick()
        {
            if (OnClick.HasDelegate) {
                await OnClick.InvokeAsync(this);
            }
        }

        private LetterState m_LetterState = LetterState.Unknown;
    }
}
