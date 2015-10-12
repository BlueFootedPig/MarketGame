Imports Core

Public Class Budget
    Implements IResourceGenerator

    Public Property Tags As IList(Of String) = New List(Of String) Implements IResourceGenerator.Tags
    Public Income As Integer
    Public valueOfBudget As Double

    Public Sub New(tag As String, basicBudget As Double)
        Tags.Add(tag)
        valueOfBudget = basicBudget
    End Sub

    Public Function SpendMoney() As Double Implements IResourceGenerator.SpendMoney
        Return valueOfBudget / Tags.Count
    End Function

End Class

