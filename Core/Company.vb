
Imports Core.VoidAssestManager

Public Class Company
    Inherits Resource
    Implements IMarketForce

    Shared Property VoidCompany As Company = New Company(New VoidAssetManager())

    Private Assests As IAssetManager

    Friend ProfitPerUnit As Double

    Public RequiredResources As New List(Of Resource)
    Public Property ProducedResource As Resource
    Public gamingStrategy As New List(Of IStrategy)

    Public Sub New(manager As IAssetManager)
        If manager Is Nothing Then Throw New ArgumentNullException("manager", "An AssetManager must be provided.")
        Assests = manager
    End Sub

    Public Sub AddResource(resourceToAdd As Resource) Implements IMarketForce.AddResource
        If resourceToAdd Is Nothing Then Throw New ArgumentNullException("Resource cannot be Null.")
        If resourceToAdd.Shares = 0 Then Throw New ArgumentException("Resource must have more than 0 shares.")

        Assests.AddAsset(resourceToAdd)
    End Sub

    Public Sub RemoveResource(resourceToRemove As Resource) Implements IMarketForce.RemoveResource
        If resourceToRemove Is Nothing Then Throw New ArgumentNullException("Resource cannot be Null.")
        If resourceToRemove.Shares = 0 Then Throw New ArgumentException("Resource must have more than 0 shares.")

        Assests.RemoveAsset(resourceToRemove)
    End Sub

    Public Function GetAllAssets() As IList(Of Resource) Implements IMarketForce.GetAllAssets
        Return Assests.GetAllAssets()
    End Function

    Public Overridable Sub PerformAction(market As IMarket) Implements IMarketForce.PerformAction
        For Each strat As IStrategy In gamingStrategy
            strat.Execute(Me, market)
        Next

    End Sub



    Public Sub Produce() Implements IMarketForce.Produce
        If ProducedResource IsNot Nothing Then
            If hasEnoughToProduce() Then
                Assests.AddAsset(New Resource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})

                For Each item As Resource In RequiredResources
                    Dim reqResource As Resource = GetAsset(item.Name)
                    reqResource.Shares -= item.Shares
                Next
            End If
        End If
    End Sub

    Private Function hasEnoughToProduce() As Boolean
        Dim returnValue As Boolean = True

        For Each item As Resource In RequiredResources

            returnValue = returnValue AndAlso Assests.HasEnough(item)
        Next

        Return returnValue
    End Function

    Public Function GetAsset(resource As String) As Resource Implements IMarketForce.GetAsset
        Return Assests.GetResource(resource)
    End Function


End Class

Public Interface IMarketForce
    Sub Produce()
    Function GetAsset(resource As String) As Resource
    Sub PerformAction(market As IMarket)
    Sub AddResource(resourceToAdd As Resource)
    Sub RemoveResource(resourceToRemove As Resource)
    Function GetAllAssets() As IList(Of Resource)
End Interface
