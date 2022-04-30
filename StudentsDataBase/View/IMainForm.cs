using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    internal interface IMainForm
    {
        event EventHandler DeleteEvent;
        event EventHandler EditEvent;
        event EventHandler AddEvent;
        event EventHandler UpdateEvent;
        event EventHandler SaveDataEvent;

        DataGridView DataGridView { get; }
        SaveFileDialog SaveFileDialog { get; }
    }
}
