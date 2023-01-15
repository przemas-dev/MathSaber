using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("QUIZ")]
public class Quiz
{
    [XmlElement("q")]
    public List<QuestionsXML> questionsXML;
}
