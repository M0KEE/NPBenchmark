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
            problem.checkerPath = "Luck";

            Instance instance = new Instance();
            instance.results.Add(new Result { path = "", header = new Submission { obj = int.MaxValue } });

            problem.instances.Add("Test.txt", instance);

            Rank rank = new Rank();
            rank.problems.Add("Luck", problem);

            Util.Json.save(CommonCfg.RankPath, rank);
        }

        static void generateRankPageSample() {
            PageGenerator pg = new PageGenerator();
            pg.generate();
        }
    }
}
