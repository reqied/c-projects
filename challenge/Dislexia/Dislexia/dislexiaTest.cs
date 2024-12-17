using NUnit;
using NUnit.Framework;
using Dislexia;

namespace DislexiaTest;

public class DislexiaTests
{
    [TestCase("#dyslexia#vocie trelbemd as she#", ExpectedResult = "voice trembled as she")]
    [TestCase("#dyslexia#to creep down a lnog#", ExpectedResult = "to creep down a long")]
    [TestCase("#dyslexia#a dagorn would be esay#", ExpectedResult = "a dragon would be easy")]
    [TestCase("#dyslexia#pull the door open siad#", ExpectedResult = "pull the door open said")]
    [TestCase("#dyslexia#if i'm not picekd to#", ExpectedResult = "if i'm not picked to")]
    [TestCase("#dyslexia#life he was hgurny he'd#", ExpectedResult = "life he was hungry he'd")]
    [TestCase("#dyslexia#a man's face he wore#", ExpectedResult = "a man's face he wore")]
    [TestCase("#dyslexia#bithrady harry gaonerd#", ExpectedResult = "birthday harry groaned")]
    [TestCase("#dyslexia#dead in his tcraks the#", ExpectedResult = "dead in his tracks the")]
    [TestCase("#dyslexia#a man's face he wore#", ExpectedResult = "a man's face he wore")]
    [TestCase("#dyslexia#if i'm not picekd to#", ExpectedResult = "if i'm not picked to")]
    [TestCase("#dyslexia#hrary managed to grab#", ExpectedResult = "harry managed to grab")]
    [TestCase("#dyslexia#to creep down a lnog#", ExpectedResult = "to creep down a long")]
    [TestCase("#dyslexia#a dagorn would be esay#", ExpectedResult = "a dragon would be easy")]
    [TestCase("#dyslexia#wlel here geos he put#", ExpectedResult = "well here goes he put")]
    [TestCase("#dyslexia#the gaol potss but he's#", ExpectedResult = "the goal spots but he's")]

    public string Clean(string text)
    {
        return Dislexia.Program.Do(text);
    }
}

