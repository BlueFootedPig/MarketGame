Public Class CraftResource
    Implements IResource
    Public Property Name As String Implements IResource.Name
    Public Property Shares As Integer = 0 Implements IResource.Shares
    Public Property Level As Integer = 0 Implements IResource.Level

    Public Shared RESOURCE_LUMBER As String = "Lumber"
    Public Shared RESOURCE_IRON As String = "Iron Ore"
    Public Shared RESOURCE_GOLD As String = "Gold"
    Public Shared CREDIT As String = "Widgets"

    Public Function CopyResource() As IResource Implements IResource.CopyResource
        Dim returnValue As New CraftResource
        returnValue.Name = Name
        returnValue.Shares = Shares
        returnValue.Level = Level

        Return returnValue
    End Function


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

Public Interface IResource
    Property Name As String
    Property Shares As Integer
    Property Level As Integer

    Function CopyResource() As IResource
End Interface

