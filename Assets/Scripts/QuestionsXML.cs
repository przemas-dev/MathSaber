using System.Xml.Serialization;

public class QuestionsXML
{
    [XmlElement("question")]
    public string question;

    [XmlElement("ans1")]
    public string ans1;

    [XmlElement("ans2")]
    public string ans2;

    [XmlElement("ans3")]
    public string ans3;

    [XmlElement("correct")]
    public int correct;
}