using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RenderCodeHelper
    {
        private int distance = 0;
        private string spacePrefix = "";

        private string downline = "\n";

        public string RenderCode { get; private set; } = "";

        public RenderCodeHelper(int numberSpace = 4)
        {
            spacePrefix = JoinTexts(numberSpace, " ");
        }

        public void WriteCode(string code, RenderDownLine renderDownLine = RenderDownLine.OneLine)
        {
            RenderCode += JoinTexts(distance, spacePrefix) + code;
            RenderCode += JoinTexts((int)renderDownLine, downline);
        }

        public void WriteCode(string code, params object[] agrs)
        {
            RenderCode += JoinTexts(distance, spacePrefix) + string.Format(code, agrs) + downline;
        }

        public void WriteCode(string code, object agrs)
        {
            RenderCode += JoinTexts(distance, spacePrefix) + string.Format(code, agrs) + downline;
        }

        public void WriteCode(string code, RenderDownLine renderDownLine, params object[] agrs)
        {
            RenderCode += JoinTexts(distance, spacePrefix) + string.Format(code, agrs);
            RenderCode += JoinTexts((int)renderDownLine, downline);
        }

        public void WriteCode(string code, RenderDownLine renderDownLine, object agrs)
        {
            RenderCode += JoinTexts(distance, spacePrefix) + string.Format(code, agrs);
            RenderCode += JoinTexts((int)renderDownLine, downline);
        }

        public void OpenAngleBraket(RenderDownLine renderDownLine = RenderDownLine.OneLine)
        {
            RenderCode += JoinTexts(distance, spacePrefix) + "{";
            RenderCode += JoinTexts((int)renderDownLine, downline);

            distance++;
        }

        public void CloseAngleBraket(RenderDownLine renderDownLine = RenderDownLine.OneLine)
        {
            distance--;
            RenderCode += JoinTexts(distance, spacePrefix) + "}";
            RenderCode += JoinTexts((int)renderDownLine, downline);
        }

        public void DownLine(RenderDownLine renderDownLine = RenderDownLine.OneLine)
        {
            RenderCode += downline;
        }

        public static string JoinTexts(int timesJoin, string text)
        {
            if (string.IsNullOrEmpty(text) == true) throw new Exception("Text can't null or empty");

            string _result = "";
            for (int i = 0; i < timesJoin; i++)
            {
                _result += text;
            }

            return _result;
        }
    }

}
