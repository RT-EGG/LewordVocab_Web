namespace LewordVocab
{
    public struct WordDictionaryItem
    {
        public WordDictionaryItem(string inWord, string inMean)
        {
            Word = inWord;
            Mean = inMean;
        }

        public readonly string Word;
        public readonly string Mean;
    }
}
