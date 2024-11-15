using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Tokenizer
{   
    public Text Text { get; set; }
    public void Tokenize()
    {
        TokenizeString();
        TokenizeWordsAndPunctuation();
    }

    public void TokenizeString(){
        string pattern = @"(?<=[.!?])\s+";
        List<Sentence> sentences = Regex.Split(Text._Text, pattern)
                    .Select(sentence => new Sentence(sentence.Trim()))
                        .ToList();
        Text.Sentences = sentences;
    }

    public void TokenizeWordsAndPunctuation(){
        string wordPattern = @"\w+|[^\w\s]";

        foreach(var sentence in Text.Sentences){
            var words = new List<Word>();
            var punctuation = new List<Punctuation>();

            var matches = Regex.Matches(sentence.Text, wordPattern);
            foreach (Match match in matches)
            {
                if (char.IsPunctuation(match.Value[0]))
                {
                    punctuation.Add(new Punctuation(match.Value));
                }
                else
                {
                    words.Add(new Word(match.Value));
                }
            }

            sentence.Words = words;
            sentence.Punctuation = punctuation;

            if (sentence.Punctuation.Any(x => x.character == "?"))
            {
                sentence.Type = SentenceType.Question;
            }
            else if (sentence.Punctuation.Any(x => x.character == "!"))
            {
                sentence.Type = SentenceType.Exclamation;
            }
            else
            {
                sentence.Type = SentenceType.Default;
            }
        }
    }

    public Tokenizer(Text text){
        Text= text;
    }
}   