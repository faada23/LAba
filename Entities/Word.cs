using System.Xml.Serialization;

public class Word
{   
    [XmlText] // Элемент для сериализации
    public string word { get; set; }

    public Word(string word)
    {
        this.word = word;
    }

    public Word() { }
}