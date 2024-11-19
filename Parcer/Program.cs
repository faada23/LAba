using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Parcer;
class Program
{   
    public static Tokenizer tokenizer = null!;
    public static XMLSerializer xmlSerializer = null!;

    static void Main(string[] args)
    {   
        FileReader fr = new FileReader();
        fr.ReadFile(@"D:\programmes\vs code projects\LAba\text.txt");
        xmlSerializer = new XMLSerializer();

        Text text = new Text(fr.Text);

        tokenizer = new Tokenizer(text);
        tokenizer.Tokenize();
        
        StringsByWordsCount(text);

        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        StringsByLength(text);

        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        SearchWordsByLength(text,3);

        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        RemoveWordsByLength(text,4);

        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        ReplaceWordsByLength(text,sentenceNum: 1,wordLength: 3,"newWord");

        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        //language : eng , rus
        DeleteStopWords(tokenizer,text,"eng");

        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------------");

        XmlSerialize(text);
    }


    public static void StringsByWordsCount(Text text){

        var sortedSentences = text.Sentences
        .OrderBy(s => s.Words.Count)
        .ToList(); 

        foreach (var sentence in sortedSentences)
        {
            Console.WriteLine(sentence.Text);
        }
    }

    public static void StringsByLength(Text text){
        var sortedSentences = text.Sentences
        .OrderBy(s => s.Text.Length)
        .ToList();

        foreach (var sentence in sortedSentences){
            Console.WriteLine(sentence.Text);
        }
    }

    public static void SearchWordsByLength(Text text,int wordLength){

        //Получаем только вопросные предложения
        var QuestionSentences = text.Sentences.Where(s => s.Type == SentenceType.Question).ToList();

        foreach (var sentence in QuestionSentences){

            //Убираем повторяющиеся слова
            var unqiueWords = sentence.Words.Distinct().ToList();

            foreach (var word in unqiueWords){

                if(word.word.Length == wordLength){

                    Console.WriteLine(word.word);
                }
            }

        }
    }

    public static void RemoveWordsByLength(Text text,int wordLength){
        string pattern = $@"\b[бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz]\w{{{wordLength - 1}}}\b";
        foreach(var sentence in text.Sentences){
            sentence.Text = Regex.Replace(sentence.Text, pattern, "", RegexOptions.IgnoreCase).Trim();
            sentence.Text = Regex.Replace(sentence.Text, @"\s+", " ");
            sentence.Text = Regex.Replace(sentence.Text, @"\s+([.,!?;:])", "$1");
        }

        //Обновляем токенизацию предложений на слова и пунктуацию
        tokenizer.TokenizeWordsAndPunctuation();

        text._Text = string.Join(" ", text.Sentences.Select(x => x.Text));

        text.Print();
    }

    public static void ReplaceWordsByLength(Text text,int sentenceNum,int wordLength,string newWord){
        string pattern = $@"\b\w{{{wordLength}}}\b";

        //заменяем слова
        text.Sentences[sentenceNum].Text = Regex.Replace(text.Sentences[sentenceNum].Text, pattern, newWord, RegexOptions.IgnoreCase);


        //Обновляем токенизацию предложений на слова и пунктуацию
        tokenizer.TokenizeWordsAndPunctuation();

        text._Text = string.Join(" ", text.Sentences.Select(x => x.Text));

        Console.WriteLine(text.Sentences[sentenceNum].Text);
    
    }

    public static void DeleteStopWords(Tokenizer tokenizer,Text text,string language){
        //language : eng , rus
        StopWordsRemover stopWordsRemover = new StopWordsRemover();
        stopWordsRemover.RemoveStopWords(tokenizer,text,language);

        text.Print();
    }

    public static void XmlSerialize(Text text){
        tokenizer.TokenizeForSerialize();
        
        xmlSerializer.TextCustomXml(@"D:\programmes\vs code projects\LAba\text.xml",text);
    }
}


