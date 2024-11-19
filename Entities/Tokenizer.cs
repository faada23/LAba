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

    
    public void TokenizeForSerialize()
    {   
        foreach (var sentence in Text.Sentences)
        {   
           sentence.Tokens = sentence.Words.Cast<Token>()
            .Concat(sentence.Punctuation.Cast<Token>())
            .OrderBy(token => token.PlaceInSentence)
            .ToList();
        }
    }
    


    public void TokenizeWordsAndPunctuation(){
        string wordPattern = @"\w+|[^\w\s]";

        foreach(var sentence in Text.Sentences){
            var words = new List<Word>();
            var punctuation = new List<Punctuation>();

            var matches = Regex.Matches(sentence.Text, wordPattern);

            int counter = 0;

            foreach (Match match in matches)
            {
                if (char.IsPunctuation(match.Value[0]))
                {   
                    var punc = new Punctuation(match.Value);
                    punc.PlaceInSentence = counter;
                    punctuation.Add(punc);
                
                }
                else
                {   
                    var word = new Word(match.Value);
                    word.PlaceInSentence = counter;
                    words.Add(word);
                }
                counter++;
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