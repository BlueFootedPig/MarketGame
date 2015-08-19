Public Class Resource
    Public Property Name As String
    Public Property Shares As Integer = 0
    'Public Property MarketValue As Double = 0

    Public Shared RESOURCE_LUMBER As String = "Lumber"
    Public Shared RESOURCE_IRON As String = "Iron Ore"
    Public Shared RESOURCE_GOLD As String = "Gold"
    Public Shared CREDIT As String = "Widgets"


    Public Overrides Function ToString() As String
        Return Name + ";" + Shares
    End Function

End Class


