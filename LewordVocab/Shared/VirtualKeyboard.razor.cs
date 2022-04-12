using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LewordVocab.Shared
{
    public partial class VirtualKeyboard
    {
        public VirtualKeyboard()
        {
            foreach (char key in "abcdefghijklmnopqrstuvwxyz") {
                Keys.Add(new VirtualKeyboardKey {
                    LetterState = LetterState.Unknown
                });
            }
        }

        [Parameter]
        public EventCallback<VirtualKeyboardKey> OnClickKey
        { get; set; } = EventCallback<VirtualKeyboardKey>.Empty;

        public VirtualKeyboardKey GetKey(char inLetter)
            => Keys.FirstOrDefault(k => k.Letter == inLetter);

        internal void AddChild(VirtualKeyboardKey inKey)
            => Keys.Add(inKey);

        private List<VirtualKeyboardKey> Keys
        { get; } = new List<VirtualKeyboardKey>();

        private async Task ClickKey(VirtualKeyboardKey inKey)
        {
            if (OnClickKey.HasDelegate) {
                await OnClickKey.InvokeAsync(inKey);
            }
        }
    }
}
