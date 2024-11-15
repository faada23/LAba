using System;
using System.IO;
using System.Text;


public class FileReader
{   
    public string Text { get; set; } = null!;

    public void ReadFile(string path)
    {
        using(var reader = new StreamReader(path, Encoding.UTF8)){
            Text = reader.ReadToEnd();
        }
    }

    public void TestOut(){
        Console.WriteLine(Text);
    }
}