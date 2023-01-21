

using System.Xml.Serialization;

public class Question
{
    [XmlElement("QuestionText")]
    public string QuestionText;
    
    [XmlArray("AnswerTexts")]
    public string[] AnswerTexts;
    
    [XmlElement("CorrectAnswer")]
    public int CorrectAnswer;
    
    [XmlIgnore]
    public AnswerCube[] AnswerCubes = new AnswerCube[3];
}