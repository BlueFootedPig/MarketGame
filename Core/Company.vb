Public Class Company
    Inherits Resource

    Private Assests As New AssetManager()
    Friend ProfitPerUnit As Double


    Public RequiredResources As New List(Of Resource)
    Public Property ProducedResource As Resource
    Public gamingStrategy As New List(Of IStrategy)

    Public Sub AddResource(resourceToAdd As Resource)
        If resourceToAdd Is Nothing Then Throw New ArgumentNullException("Resource cannot be Null.")
        If resourceToAdd.Shares = 0 Then Throw New ArgumentException("Resource must have more than 0 shares.")

        Assests.AddAsset(resourceToAdd)
    End Sub

    Public Sub RemoveResource(resourceToRemove As Resource)
        If resourceToRemove Is Nothing Then Throw New ArgumentNullException("Resource cannot be Null.")
        If resourceToRemove.Shares = 0 Then Throw New ArgumentException("Resource must have more than 0 shares.")

        Assests.RemoveAsset(resourceToRemove)
    End Sub

    Public Function GetAllAssets() As IList(Of Resource)
        Return Assests.GetAllAssets()
    End Function

    Public Overridable Sub PerformAction(market As IMarket)
        For Each strat As IStrategy In gamingStrategy
            strat.Execute(Me, market)
        Next

    End Sub

 

    Public Sub Produce()

        If hasEnoughToProduce() Then
            Assests.AddAsset(New Resource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})

            For Each item As Resource In RequiredResources
                Dim reqResource As Resource = GetAsset(item.Name)
                reqResource.Shares -= item.Shares
            Next
        End If

    End Sub

    Private Function hasEnoughToProduce() As Boolean
        Dim returnValue As Boolean = True

        For Each item As Resource In RequiredResources

            returnValue = returnValue AndAlso Assests.HasEnough(item)
        Next

        Return returnValue
    End Function

    Public Function GetAsset(resource As String) As Resource
        Return Assests.GetResource(resource)
    End Function



End Class
