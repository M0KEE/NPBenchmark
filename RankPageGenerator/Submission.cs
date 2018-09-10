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
        [DataMember(Order = 1)] public string author = "提交者姓名 (例如 szx)";
        [DataMember(Order = 2)] public string algorithm = "算法名称 (例如 local search)";
        [DataMember(Order = 3)] public string thread = "算法线程数 (例如 4)";
        [DataMember(Order = 4)] public string cpu = "处理器型号与主频 (例如 Intel Core i5-7400 3.00GHz)"; // model + freq.
        [DataMember(Order = 5)] public string ram = "内存容量与频率 (例如 16G 2400MHz)"; // cap + freq. (wmic memorychip)
        [DataMember(Order = 6)] public string language = "编程语言 (例如 C++)";
        [DataMember(Order = 7)] public string compiler = "编译器 (例如 VC++2017)";
        [DataMember(Order = 8)] public string os = "操作系统 (例如 Windows 10)";

        // fields to be set by solver SDK.
        [DataMember(Order = 21)] public string problem = "问题名称 (例如 TPP)";
        [DataMember(Order = 22)] public string instance = "算例名称 (例如 n200e6000h60.txt)";
        [DataMember(Order = 23)] public string duration = "求解耗时 (例如 123.45s)";

        // fields to be set by server.
        [DataMember(Order = 31)] public double obj = 678.9;
        [DataMember(Order = 32)] public string email = "提交者邮箱 (例如 rem@hust.edu.cn)";
        [DataMember(Order = 33)] public string date = "提交时间 (例如 2018-09-09 17:20:00.000)";
    }
}
