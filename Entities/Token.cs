using System.Diagnostics.Contracts;
using System.Xml.Serialization;

public abstract class Token{
    [XmlIgnore]
    public int PlaceInSentence { get; set; }
}