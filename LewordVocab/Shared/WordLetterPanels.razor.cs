using LewordVocab.Utils;
using Microsoft.AspNetCore.Components;
using System;

namespace LewordVocab.Shared
{
    public class WordLetterPanelClickEventArgs
    {
        public WordLetterPanelClickEventArgs(int inLetterIndex, WordLetterPanel inPanel)
        {
            LetterIndex = inLetterIndex;
            Panel = inPanel;
        }

        public readonly int LetterIndex;
        public readonly WordLetterPanel Panel;
    }

    public partial class WordLetterPanels
    {
        public WordLetterPanel this[int inIndex]
            => Panels[inIndex];

        [Parameter]
        public int MaxWidth
        { get; set; } = 300;
        [Parameter]
        public int MaxHeight
        { get; set; } = 300;

        [Parameter]
        public int PanelCount
        {
            get => Panels.Length;
            set {
                if (value != PanelCount) {
                    Panels = new WordLetterPanel[value];
                    StateHasChanged();
                }
            }
        }

        [Parameter]
        public EventCallback<WordLetterPanelClickEventArgs> OnLetterPanelClick
        { get; set; } = EventCallback<WordLetterPanelClickEventArgs>.Empty;

        public void SetChild(WordLetterPanel inChild)
            => Panels[inChild.Index] = inChild;

        private async void LetterPanelClick(WordLetterPanel inPanel)
        {
            if (OnLetterPanelClick.HasDelegate) {
                int index = Panels.IndexOf(inPanel);
                await OnLetterPanelClick.InvokeAsync(new WordLetterPanelClickEventArgs(index, inPanel));
            }
        }


        private WordLetterPanel[] Panels
        { get; set; } = new WordLetterPanel[0];

        //private async Task SearchButtonClick()
        //{
        //    if (OnWordInputCommit.HasDelegate) {
        //        string word = "";
        //        for (int i = 0; i < Boxes.Length; ++i) {
        //            WordInputBox letterBox = this[i];
        //            word += (letterBox.Letter == "") ? "*" : this[i].Letter;
        //        }

        //        await OnWordInputCommit.InvokeAsync(word);
        //    }
        //}
    }
}
