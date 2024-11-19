using System.Xml.Serialization;

public class Punctuation: Token{
    [XmlText]
    public string character{get; set;}

    public Punctuation(string character){
        this.character = character;
    }

    public Punctuation(){ }
}