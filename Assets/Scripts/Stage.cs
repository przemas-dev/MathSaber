using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Stage")]
public class Stage
{
    [XmlElement("Question")]
    public List<Question> Questions;
}
