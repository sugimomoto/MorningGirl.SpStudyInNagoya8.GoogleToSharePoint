using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorningGirl.SpStudyInNagoya8.GoogleToSharePoint
{
    public class GoogleDialogResponseBody
    {
        public string speech { get; set; }
        public string displayText { get; set; }

        public object data { get; set; }

        public string[] contextOut { get; set; }

        public string source { get; set; }
    }
}