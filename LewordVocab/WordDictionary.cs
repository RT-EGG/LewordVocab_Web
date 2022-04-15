using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LewordVocab
{
    public class WordDictionary
    {
        public void AddWord(WordDictionaryItem inItem)
            => Items.Add(inItem);
        public void AddWords(IEnumerable<WordDictionaryItem> inItems)
            => Items.AddRange(inItems);
        public void Clear()
            => Items.Clear();

        public async Task<IEnumerable<WordDictionaryItem>> Search(string inCorrects, string inPresents, string inAbsents)
        {
            int length = inCorrects.Length;
            Regex regex = new Regex(inCorrects.Replace('*', '.'), RegexOptions.IgnoreCase);

            return await Task.Run(() => {
                return Items.Where(item => item.Word.Length == length) // 文字数判定
                        .Where(item => !ContainsSpecialCharacter(item.Word)) // 特殊文字は除外する
                        .Where(item => regex.IsMatch(item.Word)) // 確定文字にマッチする
                        .Where(item => inPresents.All(letter => item.Word.Contains(letter))) // 候補文字が全て含まれている
                        .Where(item => inAbsents.All(letter => !item.Word.Contains(letter))) // 除外文字が全て含まれていない
                        ;
            });            
        }

        private bool ContainsSpecialCharacter(string inWord)
        {
            const string NormalCharacters = "abcdefghijklmnopqrstuvwxyz";
            return inWord.Any(letter => !NormalCharacters.Contains(letter));
        }

        private List<WordDictionaryItem> Items
        { get; } = new List<WordDictionaryItem>();
    }
}
