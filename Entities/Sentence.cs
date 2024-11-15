using System.Xml.Serialization;

public class Sentence {
    [XmlIgnore]
    public string Text { get; set; }

    [XmlElement("word")] // Элемент для каждого слова
    public List<Word> Words { get; set; }

    [XmlIgnore]
    public List<Punctuation> Punctuation { get; set; }

    [XmlIgnore]
    public SentenceType Type { get; set; }

    public Sentence(string text)
    {
        Text = text;
        Words = new List<Word>();
        Punctuation = new List<Punctuation>();
    }

    public Sentence() 
    {
        Words = new List<Word>();
        Punctuation = new List<Punctuation>();
    }
}