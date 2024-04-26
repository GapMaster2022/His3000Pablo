using System.Windows.Forms;

namespace System
{
    internal class DataGridViewCellEventArgs
    {
        private Action<object, Windows.Forms.DataGridViewCellEventArgs> gridNotasEvolucion_DoubleClick;

        public DataGridViewCellEventArgs(Action<object, Windows.Forms.DataGridViewCellEventArgs> gridNotasEvolucion_DoubleClick)
        {
            this.gridNotasEvolucion_DoubleClick = gridNotasEvolucion_DoubleClick;
        }
    }
}