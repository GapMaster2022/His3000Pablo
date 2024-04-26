using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace His.Negocio
{
    public class DataGridViewExtensions
    {
        private List<DataGridViewRow> m_listaRows = new List<DataGridViewRow>();
        #region Encontrar
        public List<DataGridViewRow> Find( DataGridView dgv, string fieldName, string criterio)
        {
            //comparamos los valores de los paramtros
            if (fieldName==string.Empty || criterio==string.Empty)
                return null;
        //    ' Para que funcione adecuadamente el operador Like, hay que establecer
        //' Option Compare Text a nivel del módulo donde aparezca la función,
        //' o a nivel del proyecto.
            try
            {
                IEnumerable<DataGridViewRow> query  = (from item in dgv.Rows.Cast<DataGridViewRow>()
                                                  where item.Cells[fieldName].Value != DBNull.Value &&
                                                   item.Cells[fieldName].Value.ToString().Contains(criterio)
                                                  select item).ToList<DataGridViewRow>();
                // devolvemos la consulta linq ejecutada
                m_listaRows= query.ToList();
                return m_listaRows;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
              
        //If (fieldName = String.Empty) OrElse _
        //    (criterio = String.Empty) Then Return Nothing

        //' Para que funcione adecuadamente el operador Like, hay que establecer
        //' Option Compare Text a nivel del módulo donde aparezca la función,
        //' o a nivel del proyecto.
        //'
        //Try
        //    Dim query As IEnumerable(Of DataGridViewRow) = _
        //        From item As DataGridViewRow In dgv.Rows.Cast(Of DataGridViewRow)() _
        //        Where ((item.Cells(fieldName).Value IsNot DBNull.Value) AndAlso _
        //              (CStr(item.Cells(fieldName).Value) Like criterio)) _
        //        Select item

        //    ' Devolvemos la consulta LINQ ejecutada.
        //    m_listRows = query.ToList()
    //        Return m_listRows

    //    Catch ex As Exception
    //        Return Nothing

    //    End Try

    //End Function
        //**********************
        public void Find(DataGridView dgv, Int32 position)
        {
            if (m_listaRows == null)
                throw new InvalidOperationException("Operacion no valida, establesca primero los criterios de busqueda");
            DataGridViewRow row = m_listaRows.ElementAtOrDefault(position);
            if (row !=null)
            {
                dgv.CurrentCell= dgv.Rows[row.Index].Cells[0];
                dgv.FirstDisplayedCell= dgv.CurrentCell;
            }

        }
        //Public Sub Find(ByVal dgv As DataGridView, ByVal position As Int32)

        //If m_listRows Is Nothing Then _
        //    Throw New InvalidOperationException( _
        //        "Operación no válida. " & _
        //        "Establezca primero los criterios de búsqueda " & _
        //        "mediante el método Find.")

        //' Seleccionamos el elemento correspondiente a la
        //' posición actual especificada.
        //'
        //Dim row As DataGridViewRow = m_listRows.ElementAtOrDefault(position)

    //    If row IsNot Nothing Then
    //        ' Establecemos la celda actual.
    //        '
    //        dgv.CurrentCell = dgv.Rows(row.Index).Cells(0)
    //        dgv.FirstDisplayedCell = dgv.CurrentCell

    //    End If

    //End Sub
        #endregion

    }
}
