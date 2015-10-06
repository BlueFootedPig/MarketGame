Public Interface IResourceGenerator
    Function SpendMoney() As Double
    Property Tags As IList(Of String)

End Interface

'Public Class Person
'    Implements IResourceGenerator

'    Public Property Tags As IList(Of String) = New List(Of String) Implements IResourceGenerator.Tags
'    Public Income As Integer


'    Public Sub New(wealthClass As Integer)
'        Income = wealthClass

'        'This select statement needs to be abstracted to handle when we allow changes in number of wealth classes
'        Select Case wealthClass
'            Case 0
'                Tags.Add("Poor")
'            Case 1
'                Tags.Add("Middle")
'            Case 2
'                Tags.Add("Rich")
'        End Select

'    End Sub


'    Public Function SpendMoney() As Double Implements IResourceGenerator.SpendMoney
'        Return Math.Pow(10, Income) / Tags.Count
'    End Function

'End Class

