Public Class Site2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lbtnGridView_Click(sender As Object, e As EventArgs) Handles lbtnGridView.Click
        If Session("gridView") = False Then
            Session("gridView") = True
        Else
            Session("gridView") = False
        End If
        Response.Redirect("Webform1.aspx")
    End Sub

End Class