using System.Xml.Serialization;

public class XMLSerializer
{
    /*    public void XmlSerialize(string path,Text text)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Text));
        using (StreamWriter writer = new StreamWriter(path))
        {
            serializer.Serialize(writer, text);
        }
         
    }*/

    public string SentenceCustomXml(Sentence sentence)
    {
        using (StringWriter stringWriter = new StringWriter())
        {
            stringWriter.WriteLine("<sentence>");
            foreach (var token in sentence.Tokens)
        {
            if (token is Word word)
            {
                stringWriter.WriteLine($"<word>{word.word}</word>");
            }
            else if (token is Punctuation punctuation)
            {
                stringWriter.WriteLine($"<punctuation>{punctuation.character}</punctuation>");
            }
        }
            stringWriter.WriteLine("</sentence>");
            return stringWriter.ToString();
        }
    }

    public void TextCustomXml(string path,Text text)
    {
        using (StreamWriter Writer = new StreamWriter(path))
        {
            Writer.WriteLine("<text>");
            foreach (var sentence in text.Sentences)
            {
                Writer.Write(SentenceCustomXml(sentence));
            }
            Writer.WriteLine("</text>");
        }
    }
    
}