using System.Text;
using System.Text.RegularExpressions;


//Correct path to be used
string path = "C:/Users/ShMlangeni/source/repos/CommentsReader/CommentsReader/docs/";
string[] files = System.IO.Directory.GetFiles(path, "*.txt");

StringBuilder codeNameBuilder = new StringBuilder();
List<String> list = new List<string>();

int totalNumberOfCommentsShorter = 0;
int totalNumberOfCommentsMover = 0;
int totalNumberOfCommentsShaker = 0;
int totalNumberOfCommentsUrl = 0;
int totalNumberOfCommentsQuestionMark = 0;


//RegEx 
string urlRegex = @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";
string questionMarkRegex = @".*?([a-z_]*\?+[a-z_]*).*?";


//Concurrency
foreach (string file in files)
{
    Thread newThread = new Thread(new ThreadStart(() => task(file)));
    newThread.Start();
}
Thread.Sleep(3000);


//Stats
foreach (string matcher in list)
{
    MatcherBuilder(true, null, 15, matcher, "match", ref totalNumberOfCommentsShorter);
    MatcherBuilder(false, null, null, matcher, "Mover", ref totalNumberOfCommentsMover);
    MatcherBuilder(false, null, null, matcher, "Shaker", ref totalNumberOfCommentsShaker);
    MatcherBuilder(false, null, null, matcher, urlRegex, ref totalNumberOfCommentsUrl);
    MatcherBuilder(false, null, null, matcher, questionMarkRegex, ref totalNumberOfCommentsQuestionMark);
}

Console.WriteLine(totalNumberOfCommentsShorter);
Console.WriteLine(totalNumberOfCommentsMover);
Console.WriteLine(totalNumberOfCommentsShaker);
Console.WriteLine(totalNumberOfCommentsUrl);
Console.WriteLine(totalNumberOfCommentsQuestionMark);



//System.IO.File.WriteAllText(path+ "txtOutput.txt", codeNameBuilder.ToString());

void MatcherBuilder(bool isCounter, string? Operator, int? maxCharacter, string matcher, string regexParameter, ref int counter)
{
    if (isCounter)
    {
        if (matcher.Count() > maxCharacter)
        {
            counter++;
        }
    }
    else
    {
        if (Regex.IsMatch(matcher, regexParameter))
        {
            counter++;
        }
    }

}

void task(string file)
{

    List<string> codeList = File.ReadLines(file).ToList();
    for (int i = 0; i < codeList.Count; i++)
    {
        codeNameBuilder.AppendFormat("{0} \n", codeList[i]);
        list.Add(codeList[i]);
    }

}