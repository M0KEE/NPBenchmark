using System;


namespace RankPageGenerator {
    public class CommonCfg {
        public const string RankPath = "rank.json";
        public const string RankPagePath = "index.html";

        public const int MaxResultsCountPerInstance = 100;

        public const string AuthorInstruction =
@"submit your solution files by sending emails to <a href='mailto:rem@hust.edu.cn'>rem@hust.edu.cn</a>.
<br>the subject of the email should include the keyword <b>[NPBenchmark]</b>.
<br>each attachment of the email should be a solution file in the format specified by the corresponding problem description.
";

        public const string SdkSpecification =
@"the sdk should append a single-line header to the solution file.
<br>the sdk should set the 'problem', 'instance' and 'duration' fields in the solution header and guide users to fill other fields.
<br>the checker should determine the problem and instance by reading the solution header.
<br>the checker should print the obj value to the standard output if the solution is valid, otherwise anything except a number should be printed.
";
    }

    public class EmailCfg {
        public const int Pop3Port = 110;
        public const int ImapPort = 143;
        public const int SmtpPort = 25;
        public const int SmtpPort2 = 2525;

        public const int Pop3SslPort = 995;
        public const int ImapSslPort = 993;
        public const int SmtpSslPort = 465;

        public class HustMail {
            public const string Pop3 = "mail.hust.edu.cn";
            public const string Imap = "mail.hust.edu.cn";
            public const string Smtp = "mail.hust.edu.cn";
        }

        public const string SaveDir = "archive/";
        public const string SubjectFilter = "[NPBenchmark]";

        public const string MyAddress = "rem@hust.edu.cn";
        //public const string ToAddress = "";
        public const string CcAddress = "";

        public const string Username = MyAddress;
        public const string Password = "";
    }
}
