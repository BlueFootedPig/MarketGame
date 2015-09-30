Public Class Budget
    Implements IResourceGenerator

    Public Property Tags As IList(Of String) = New List(Of String) Implements IResourceGenerator.Tags
    Public Income As Integer
    Public budget As Double

    Public Function SpendMoney() As Double Implements IResourceGenerator.SpendMoney
        Return budget
    End Function

End Class

