using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LewordVocab
{
    public class WordDictionaryLoader
    {
        public static async Task<WordDictionary> LoadData(string inFile)
        {
            return await Task.Run(() => {
                WordDictionary result = new WordDictionary();

                result.AddWords(inFile.Split('\n', StringSplitOptions.RemoveEmptyEntries).SelectMany(line => {
                    string[] splited = line.Split('\t');
                    string words = splited[0];
                    string mean = splited[1];

                    // 意味が同じで綴りが少し異なるだけの単語は、カンマで区切って列挙される
                    return words.Split(',').Select(w => new WordDictionaryItem(w, mean));
                }));

                return result;
            });
        }
    }
}
