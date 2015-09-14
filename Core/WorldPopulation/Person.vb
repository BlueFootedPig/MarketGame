Public Class Person
    Public Property Tags As New List(Of String)
    Public Income As Integer


    Public Sub New(wealthClass As Integer)
        Income = wealthClass

        'This select statement needs to be abstracted to handle when we allow changes in number of wealth classes
        Select Case wealthClass
            Case 0
                Tags.Add("Poor")
            Case 1
                Tags.Add("Middle")
            Case 2
                Tags.Add("Rich")
        End Select

    End Sub


    Public Function SpendMoney() As Double
        Return Math.Pow(10, Income) / tags.Count
    End Function

End Class

