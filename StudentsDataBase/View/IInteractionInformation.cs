using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    internal interface IInteractionInformation
    {
        string StudentName { get; }
        string Course { get; }
        string Group { get; }
        string Form { get; }

        DialogResult ShowInteractionInformation();
        DialogResult ShowInteractionInformation(string name, int course, int group, string form);
    }
}
