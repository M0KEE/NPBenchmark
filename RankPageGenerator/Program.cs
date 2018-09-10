using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankPageGenerator {
    class Program {
        static void Main(string[] args) {
            //generateSubmissionSample();
            //generateRankSample();
            //generateRankPageSample();

            Ss2Imap imap = new Ss2Imap();
            imap.startPollAsync();

            Console.WriteLine("Press Esc to exit.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
        }

        static void generateSubmissionSample() {
            Submission submission = new Submission();
            Util.Json.save("submission.json", submission);
            Submission submission2 = Util.Json.load<Submission>("submission.json");
        }

        static void generateRankSample() {
            Problem problem = new Problem { minimize = false };
            problem.checkerPath = "TPP-Checker.exe";

            Instance instance = new Instance();
            instance.results.Add(new Result { path = "3.txt", header = new Submission { obj = 3 } });
            instance.results.Add(new Result { path = "1.txt", header = new Submission { obj = 1 } });
            instance.results.Add(new Result { path = "2.txt", header = new Submission { obj = 2 } });

            problem.instances.Add("TestInstance", instance);

            Rank rank = new Rank();
            rank.problems.Add("TestProblem", problem);

            Util.Json.save(CommonCfg.RankPath, rank);
        }

        static void generateRankPageSample() {
            PageGenerator pg = new PageGenerator();
            pg.generate();
        }
    }
}
