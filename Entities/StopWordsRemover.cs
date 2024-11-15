using System.Dynamic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class StopWordsRemover{
    public void RemoveStopWords(Tokenizer tokenizer,Text text,string language){
        string filepath = "";
        if(language == "eng") filepath = @"D:\programmes\vs code projects\LAba\Stopwords\stopwords_en.txt";
        else if (language =="rus") filepath = @"D:\programmes\vs code projects\LAba\Stopwords\stopwords_ru.txt";
        else throw new Exception("Language not found");

        string[] stopWords;

        using (var reader = new StreamReader(filepath, Encoding.UTF8))
        {
            stopWords = reader.ReadToEnd().Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        if (stopWords.Length == 0)
        {
            throw new Exception("Stop words file is empty.");
        }

        // Создаем HashSet для быстрого поиска
        HashSet<string> wordsSet = new HashSet<string>(stopWords, StringComparer.OrdinalIgnoreCase);

        /*string pattern = @"\b(" + string.Join("|", wordsSet.Select(Regex.Escape)) + @")\b";

        text._Text = Regex.Replace(text._Text, pattern, "", RegexOptions.IgnoreCase).Trim();

        text._Text = Regex.Replace(text._Text, @"\s+", " ");*/

        var words = text._Text.Split(new[] { ' ' }, StringSplitOptions.None);

        // Фильтруем слова, удаляя стоп-слова и сохраняя знаки препинания
        var filteredWords = words.Select(word =>
        {
            // Проверяем, является ли слово стоп-словом
            string cleanWord = word.TrimEnd(',', '.', '!', '?', ';', ':');
            if (wordsSet.Contains(cleanWord))
            {
                return ""; // Возвращаем пустую строку, если это стоп-слово
            }
            return word; // Возвращаем слово как есть, если это не стоп-слово
        }).Where(word => !string.IsNullOrEmpty(word)).ToList();

        // Объединяем обратно в строку
        text._Text = string.Join(" ", filteredWords).Trim();

        //Обновляем токенизацию
        tokenizer.Tokenize();
    }
}