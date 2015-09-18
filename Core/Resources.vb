Public Class Resource
    Public Property Name As String
    Public Property Shares As Integer = 0
    Public Property Level As Integer = 0

    Public Shared RESOURCE_LUMBER As String = "Lumber"
    Public Shared RESOURCE_IRON As String = "Iron Ore"
    Public Shared RESOURCE_GOLD As String = "Gold"
    Public Shared CREDIT As String = "Widgets"




    Public Overrides Function ToString() As String
        Return Name & ";" & Shares
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.ToString() = obj.ToString()
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.ToString().GetHashCode()
    End Function
End Class


