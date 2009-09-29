Public Class cmp_ListViewPlus
    Inherits System.Windows.Forms.ListView

    Private Const REORDER As String = "Reorder"

    Protected Sub New(ByVal e As System.Windows.Forms.ListViewItem)
        MyBase.New()

        Me.Invalidate()

        Me.Update()

    End Sub


    Protected Overrides Sub OnItemDrag(ByVal e As System.Windows.Forms.ItemDragEventArgs)
        MyBase.OnItemDrag(e)
        MyBase.DoDragDrop(REORDER, DragDropEffects.Move)
    End Sub

    Protected Overrides Sub OnDragEnter(ByVal drgevent As System.Windows.Forms.DragEventArgs)
        MyBase.OnDragEnter(drgevent)

        'If Not drgevent.Data.GetDataPresent(DataFormats.Text) Then
        'drgevent.Effect = DragDropEffects.None
        'Return
        'End If

        If drgevent.Data.GetDataPresent(DataFormats.Text) Then
            Dim text As String = drgevent.Data.GetData(REORDER.GetType).ToString
            If text.CompareTo(REORDER) = 0 Then
                drgevent.Effect = DragDropEffects.Move
            Else
                drgevent.Effect = DragDropEffects.None
            End If
        End If

    End Sub

    Protected Overrides Sub OnDragOver(ByVal drgevent As System.Windows.Forms.DragEventArgs)

        'If Not drgevent.Data.GetDataPresent(DataFormats.Text) Then
        'drgevent.Effect = DragDropEffects.None
        'Return
        'End If

        If drgevent.Data.GetDataPresent(DataFormats.Text) Then

            Dim cp As Point = MyBase.PointToClient(New Point(drgevent.X, drgevent.Y))
            Dim hoverItem As ListViewItem = MyBase.GetItemAt(cp.X, cp.Y)

            If hoverItem Is DBNull.Value Then
                drgevent.Effect = DragDropEffects.None
                Return
            End If

            For Each moveItem As ListViewItem In MyBase.SelectedItems
                If moveItem.Index = hoverItem.Index Then
                    drgevent.Effect = DragDropEffects.None
                    hoverItem.EnsureVisible()
                    Return
                End If
            Next

            MyBase.OnDragOver(drgevent)

            Dim text As String = drgevent.Data.GetData(REORDER.GetType).ToString

            If text.CompareTo(REORDER) = 0 Then
                drgevent.Effect = DragDropEffects.Move
                hoverItem.EnsureVisible()
            Else
                drgevent.Effect = DragDropEffects.None
            End If
        End If

    End Sub

    Protected Overrides Sub OnDragDrop(ByVal drgevent As System.Windows.Forms.DragEventArgs)
        MyBase.OnDragDrop(drgevent)

        If drgevent.Data.GetDataPresent(DataFormats.Text) Then

            If MyBase.SelectedItems.Count = 0 Then
                Return
            End If

            Dim cp As Point = MyBase.PointToClient(New Point(drgevent.X, drgevent.Y))
            Dim dragItem As ListViewItem = MyBase.GetItemAt(cp.X, cp.Y)

            If dragItem Is DBNull.Value Then
                Return
            End If

            Dim dropIndex As Integer = dragItem.Index
            If dropIndex > MyBase.SelectedItems(0).Index Then
                dropIndex = dropIndex + 1
            End If

            Dim insertItems As ArrayList = New ArrayList(MyBase.SelectedItems.Count)
            For Each item As ListViewItem In MyBase.SelectedItems
                insertItems.Add(item.Clone)
            Next

            For i As Integer = 0 To (insertItems.Count - 1)
                Dim insertItem As ListViewItem = CType(insertItems(i), ListViewItem)
                MyBase.Items.Insert(dropIndex, insertItem)
            Next

            For Each remoteItem As ListViewItem In MyBase.SelectedItems
                MyBase.Items.Remove(remoteItem)
            Next

        End If

    End Sub

End Class
