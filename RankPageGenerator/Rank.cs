using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RankPageGenerator {
    [DataContract]
    public class Rank {
        [DataMember] public Dictionary<string, Problem> problems = new Dictionary<string, Problem>();
    }

    [DataContract]
    public class Problem {
        //[DataContract]
        //public class ResultComparer : IComparer<Result> {
        //    public int Compare(Result x, Result y) {
        //        int objDiff = header.obj.CompareTo(other.header.obj) * coef;
        //        return (objDiff != 0) ? objDiff : x.header.date.CompareTo(y.header.date);
        //    }

        //    [DataMember] public int coef = 1;
        //}
        //public bool Minimize { get { return cmp.coef > 0; } set { cmp.coef = value ? 1 : -1; } }
        //[DataMember] public ResultComparer cmp = new ResultComparer();

        [DataMember] public bool minimize = true;
        [DataMember] public string checkerPath = "chekcer.exe";
        [DataMember] public Dictionary<string, Instance> instances = new Dictionary<string, Instance>();
    }

    [DataContract]
    public class Instance {
        [DataMember] public SortedSet<Result> results = new SortedSet<Result>();
    }

    [DataContract]
    public class Result : IComparable<Result> {
        public int CompareTo(Result other) {
            int objDiff = header.obj.CompareTo(other.header.obj);
            return (objDiff != 0) ? objDiff : header.date.CompareTo(other.header.date);
        }

        [DataMember] public string path;
        [DataMember] public Submission header;
    }
}
