using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RankPageGenerator {
    [DataContract]
    public class Submission {
        // fields to be filled by authors manually.
        [DataMember(Order = 1)] public string author = "提交者姓名";
        [DataMember(Order = 2)] public string algorithm = "算法名称";
        [DataMember(Order = 3)] public string thread = "算法线程数";
        [DataMember(Order = 4)] public string cpu = "处理器型号与主频"; // model + freq.
        [DataMember(Order = 5)] public string ram = "内存容量与频率"; // cap + freq. (wmic memorychip)
        [DataMember(Order = 6)] public string language = "编程语言";
        [DataMember(Order = 7)] public string compiler = "编译器";
        [DataMember(Order = 8)] public string os = "操作系统";

        // fields to be set by solver SDK.
        [DataMember(Order = 21)] public string problem = "问题名称";
        [DataMember(Order = 22)] public string instance = "算例名称";
        [DataMember(Order = 23)] public string duration = "求解耗时";

        // fields to be set by server.
        [DataMember(Order = 31)] public double obj = 123.45;
        [DataMember(Order = 32)] public string email = "提交者邮箱";
        [DataMember(Order = 33)] public string date = "提交时间";
    }
}
