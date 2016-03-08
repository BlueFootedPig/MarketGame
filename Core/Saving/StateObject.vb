Public Class StateObject

    Public Sub New(ob As Object)
        StateObjectType = ob.GetType()
    End Sub

    Private propertyDictionary As New Dictionary(Of String, Object)

    Public Function GetProperty(propertyName As String) As Object
        If Not propertyDictionary.ContainsKey(propertyName) Then Throw New ArgumentException("propertyName is not contained in this state object.")
        Return propertyDictionary(propertyName)

    End Function

    Public Sub SetProperty(propertyName As String, value As Object)
        If propertyDictionary.ContainsKey(propertyName) Then
            propertyDictionary(propertyName) = value
        Else
            propertyDictionary.Add(propertyName, value)
        End If
    End Sub

    Public ReadOnly StateObjectType As Type

End Class
