using System.Xml.Serialization;

[XmlRoot("text")]
public class Text
{   
    [XmlIgnore]
    public string _Text { get; set; }

    [XmlElement("sentence")]
    public List<Sentence> Sentences {get;set;}

    public Text(string text){
        _Text = text;
    }
    public Text(){

    }

    public void Print(){
        Console.WriteLine(_Text);
    } 
}