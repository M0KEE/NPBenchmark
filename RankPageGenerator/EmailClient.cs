using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using S22.Imap;


namespace RankPageGenerator {
    public class StdSmtp {
        public static void send(string toAddress, string subject, string body) {
            using (SmtpClient client = new SmtpClient(EmailCfg.HustMail.Smtp)) {
                //client.Port = EmailCfg.SmtpSslPort;
                //client.EnableSsl = true;
                client.Credentials = new NetworkCredential(EmailCfg.Username, EmailCfg.Password);

                using (MailMessage message = new MailMessage()) {
                    message.From = new MailAddress(EmailCfg.MyAddress);
                    message.To.Add(toAddress);
                    message.CC.Add(EmailCfg.CcAddress);
                    message.SubjectEncoding = Encoding.UTF8;
                    message.Subject = subject;
                    message.BodyEncoding = Encoding.UTF8;
                    message.Body = body;

                    client.Send(message);
                }
            }
        }
    }

    public class Ss2Imap {
        public delegate void OnNewMessage(ImapClient client, uint uid, MailMessage msg);


        public const int PollIntervalInMillisecond = 15 * 60 * 1000; // 15 minutes.


        // [NonBlocking]
        public void startPollAsync() {
            Util.run("git", "pull origin gh-pages");
            page.generate();

            pollTimer?.Dispose();
            pollTimer = new Timer((o) => {
                try {
                    checkUnseenMails();
                } catch (Exception e) {
                    Util.log("[error] poll fail due to " + e.Message);
                }
            }, pollTimer, 0, PollIntervalInMillisecond);
        }


        static ImapClient createImapClient() {
            return new ImapClient(EmailCfg.HustMail.Imap, EmailCfg.ImapSslPort, EmailCfg.Username, EmailCfg.Password, AuthMethod.Login, true);
        }

        void checkUnseenMails() {
            lock (this) { // in case it is still running before the next poll.
                Util.run("git", "pull origin gh-pages");

                Util.log("[info] query unseen mails");
                using (ImapClient client = createImapClient()) {
                    bool updated = false;
                    IEnumerable<uint> uids = client.Search(SearchCondition.Unseen()); // .And(SearchCondition.Subject(EmailCfg.SubjectFilter))
                    foreach (var uid in uids) {
                        MailMessage msg = client.GetMessage(uid, FetchOptions.Normal, false);
                        Util.log("[info] qurey received " + msg.Subject);
                        if (msg.Subject.Contains(EmailCfg.SubjectFilter)) { updated |= handleMessage(client, uid, msg); }
                    }

                    if (!updated) { return; }
                }

                // archive.
                Util.Json.save(CommonCfg.RankPath, page.rank);

                // publish.
                page.generate();
            }
        }

        bool handleMessage(ImapClient client, uint uid, MailMessage msg) {
            client.AddMessageFlags(uid, null, MessageFlag.Seen);

            foreach (var file in msg.Attachments) {
                string solution = file.toString();

                string header = solution;
                int firstLineLength = solution.IndexOfAny(Util.LineEndings);
                if (firstLineLength > 0) { header = solution.Substring(0, firstLineLength); }

                DateTime now = DateTime.Now;

                // TODO[szx][0]: handle deserialization failure.
                Submission submission = Util.Json.fromJsonString<Submission>(header);
                submission.email = msg.From.Address;
                submission.date = Util.friendlyDateTime(now);

                Problem problem;
                if (!page.rank.problems.TryGetValue(submission.problem, out problem)) { continue; }
                Instance instance;
                if (!problem.instances.TryGetValue(submission.instance, out instance)) { continue; }

                string dir = CommonCfg.ArchiveDir + submission.problem + "/";
                Directory.CreateDirectory(dir);
                string filePath = dir + Util.compactDateTime(now) + "-" + submission.instance;
                File.WriteAllText(filePath, solution);
                
                if (page.checkers.TryGetValue(problem.checkerPath, out Checker.Check check)) {
                    submission.obj = check(solution);
                } else if (File.Exists(problem.checkerPath)) {
                    string obj = Util.runRead(problem.checkerPath, dir + submission.instance + " " + filePath);
                    if (!double.TryParse(obj, out submission.obj)) { File.Delete(filePath); continue; }
                } else {
                    submission.obj = 0;
                }

                if (submission.obj <= 0) { continue; } // infeasible or trivial solution.

                Util.run("git", "add " + filePath);

                instance.results.Add(new Result { path = filePath, header = submission });

                if (instance.results.Count > CommonCfg.MaxResultsCountPerInstance) {
                    instance.results.Remove(problem.minimize ? instance.results.Max : instance.results.Min);
                }
            }
            client.DeleteMessage(uid);

            return true;
        }


        Timer pollTimer;
        PageGenerator page = new PageGenerator();
    }
}
