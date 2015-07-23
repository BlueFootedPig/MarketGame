Public Class Company
    Inherits Resource

    Public Assests As New AssetManager()

    Public RequiredResources As New List(Of Resource)
    Friend ProfitPerUnit As Double

    Public Property ProducedResource As Resource

    Public gamingStrategy As New List(Of Strategy)

    Public Overridable Sub PerformAction(market As Market)
        For Each strat As Strategy In gamingStrategy
            strat.Execute(Me, market)
        Next

    End Sub


    Private Sub Produce(market As Market)

        If hasEnoughToProduce() Then
            Assests.AddAsset(New Resource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})

            For Each item As Resource In RequiredResources
                Dim reqResource As Resource = RequiredResources.FirstOrDefault(Function(n) n.Name = item.Name)
                reqResource.Shares -= item.Shares
            Next
        End If

    End Sub

    Private Function hasEnoughToProduce() As Boolean
        Dim returnValue As Boolean = True

        For Each item As Resource In RequiredResources

            hasEnoughToProduce = hasEnoughToProduce AndAlso Assests.HasEnough(item)
        Next

        Return returnValue
    End Function

End Class
