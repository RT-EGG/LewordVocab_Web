using System.Collections.Generic;

namespace LewordVocab.Shared
{
    public partial class WordTable
    {
        public IEnumerable<WordDictionaryItem> Items
        {
            get => m_Items;
            set {
                m_Items = value;
                StateHasChanged();
            }
        }

        private IEnumerable<WordDictionaryItem> m_Items = null;
    }
}
