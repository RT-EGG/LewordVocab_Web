using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LewordVocab.Shared
{
    public partial class WordLetterInputBoxes
    {
        private WordInputBox this[int inIndex]
            => Boxes[inIndex];

        [Parameter]
        public int BoxCount
        {
            get => Boxes.Length;
            set {
                if (value != BoxCount) {
                    Boxes = new WordInputBox[value];
                    for (int i = 0; i < value; ++i) {
                        var newBox = new WordInputBox(i);

                        Boxes[i] = newBox;
                        if (i > 0) {
                            newBox.Previous = Boxes[i - 1];
                            Boxes[i - 1].Next = newBox;
                        }
                    }
                }
            }
        }

        [Parameter]
        public EventCallback<string> OnWordInputCommit
        { get; set; } = EventCallback<string>.Empty;

        private WordInputBox[] Boxes
        { get; set; } = new WordInputBox[0];

        private async Task SearchButtonClick()
        {
            if (OnWordInputCommit.HasDelegate) {
                string word = "";
                for (int i = 0; i < Boxes.Length; ++i) {
                    WordInputBox letterBox = this[i];
                    word += (letterBox.Letter == "") ? "*" : this[i].Letter;
                }

                await OnWordInputCommit.InvokeAsync(word);
            }
        }

        public static string GetID(int inIndex)
            => $"word-character-input-{inIndex}";

        private class WordInputBox
        {
            public WordInputBox(int inIndex)
            {
                Index = inIndex;
            }

            public void OnKeyDown(KeyboardEventArgs inArgs)
            {
                //Console.WriteLine($"Key: {inArgs.Key}");
                //Console.WriteLine($"Code: {inArgs.Code}");

                string key = inArgs.Key.ToUpper();
                if (key.Length == 1) {
                    char letter = key.First();
                    if (UpperLetters.Contains(letter)) {
                        Letter = $"{letter}";
                        MoveFocusNext();
                    }

                } else {
                    switch (key) {
                        case "DELETE":
                        case "BACKSPACE":
                        case "ESCAPE":
                            Letter = "";
                            MoveFocusNext();
                            break;
                        case "TAB":
                            if (inArgs.ShiftKey) {
                                MoveFocusPrevious();
                            } else {
                                MoveFocusNext();
                            }
                            break;
                    }
                }
            }

            private async void MoveFocusPrevious()
            {
                if (Previous != null) {
                    await Previous.ElementReference.FocusAsync();
                }
            }

            private async void MoveFocusNext()
            {
                if (Next != null) {
                    await Next.ElementReference.FocusAsync();
                }
            }

            public readonly int Index;

            public string Letter
            { get; set; } = "";

            public ElementReference ElementReference
            { get; set; } = default;
            public WordInputBox Previous
            { get; set; } = null;
            public WordInputBox Next
            { get; set; } = null;

            private static string UpperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
    }
}
