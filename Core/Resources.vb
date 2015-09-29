Public Class CraftResource
    Inherits IResource

    'Public Property Name As String Implements IResource.Name
    'Public Property Shares As Integer = 0 Implements IResource.Shares
    'Public Property Level As Integer = 0 Implements IResource.Level

    Public Shared RESOURCE_LUMBER As String = "Lumber"
    Public Shared RESOURCE_IRON As String = "Iron Ore"
    Public Shared RESOURCE_GOLD As String = "Gold"
    Public Shared CREDIT As String = "Widgets"

    Public Overrides Function CopyResource() As IResource
        Dim returnValue As New CraftResource
        returnValue.Name = Name
        returnValue.Shares = Shares
        returnValue.Level = Level

        Return returnValue
    End Function


   

    Public Overrides Function Equals(obj As Object) As Boolean
        Return Me.ToString() = obj.ToString()
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.ToString().GetHashCode()
    End Function

End Class

Public MustInherit Class IResource
    Property Name As String
    Property Shares As Integer
    Property Level As Integer

    Public Shared ReadOnly Property CREDIT As IResource
        Get
            Return New CraftResource() With {.Name = "Widgets", .Shares = 0, .Level = 1}
        End Get
    End Property



    MustOverride Function CopyResource() As IResource

    Public Overrides Function ToString() As String
        Return Name & ";" & Shares
    End Function
End Class

