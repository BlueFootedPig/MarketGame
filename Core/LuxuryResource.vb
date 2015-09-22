Public Class LuxuryResource
    Inherits CraftResource

    Private _prefferedCustomers As New List(Of String)

    Private Shared _allUsedTags As New Dictionary(Of String, Integer)

    Public Sub AddPrefferedCustomer(customer As String)
        If String.IsNullOrEmpty(customer) Then Throw New ArgumentNullException("customer", "'cusomter' parameter cannot be null or blank.")

        If Not _prefferedCustomers.Contains(customer) Then _prefferedCustomers.Add(customer)


        register(customer)
    End Sub


    Private Sub register(customer As String)
        If _allUsedTags.ContainsKey(customer) Then
            _allUsedTags(customer) += 1
        Else
            _allUsedTags.Add(customer, 1)
        End If
    End Sub


    Public ReadOnly Property PrefferedCustomers As IList(Of String)
        Get
            Return _prefferedCustomers
        End Get
    End Property

    Public Shared ReadOnly Property TotalTags As Integer
        Get
            Return _allUsedTags.Keys.Count
        End Get
    End Property

End Class
