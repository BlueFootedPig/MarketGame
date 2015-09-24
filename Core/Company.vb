
Imports Core.VoidAssestManager

Public Class Company
    Inherits CraftResource
    Implements IMarketForce

    Shared Property VoidCompany As Company = New Company(New VoidAssetManager())

    Private Assests As IAssetManager

    Friend ProfitPerUnit As Double

    Public Property ProducedResource As IResourceProduction

    
    Public gamingStrategy As New List(Of IStrategy)

    Public Sub New(manager As IAssetManager)
        If manager Is Nothing Then Throw New ArgumentNullException("manager", "An AssetManager must be provided.")
        Assests = manager
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function


    Public Sub AddResource(resourceToAdd As IResource) Implements IMarketForce.AddResource
        If resourceToAdd Is Nothing Then Throw New ArgumentNullException("Resource cannot be Null.")
        If resourceToAdd.Shares = 0 Then Throw New ArgumentException("Resource must have more than 0 shares.")

        Assests.AddAsset(resourceToAdd)
    End Sub

    Public Sub RemoveResource(resourceToRemove As IResource) Implements IMarketForce.RemoveResource
        If resourceToRemove Is Nothing Then Throw New ArgumentNullException("Resource cannot be Null.")
        If resourceToRemove.Shares = 0 Then Throw New ArgumentException("Resource must have more than 0 shares.")

        Assests.RemoveAsset(resourceToRemove)
    End Sub

    Public Function GetAllAssets() As IList(Of IResource) Implements IMarketForce.GetAllAssets
        Return Assests.GetAllAssets()
    End Function

    Public Overridable Sub PerformAction(market As IMarket) Implements IMarketForce.PerformAction
        For Each strat As IStrategy In gamingStrategy
            strat.Execute(Me, market)
        Next

    End Sub



    Public Sub Produce() Implements IMarketForce.Produce
        If ProducedResource IsNot Nothing Then
            ' If hasEnoughToProduce() Then

            ProducedResource.Produce(Assests)


            'If ProducedResource.GetType() = GetType(LuxuryResource) Then
            '    Assests.AddAsset(New LuxuryResource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})
            'Else
            '    Assests.AddAsset(New CraftResource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})
            'End If

            'For Each item As CraftResource In RequiredResources
            '    Dim reqResource As CraftResource = GetAsset(item.Name)
            '    reqResource.Shares -= item.Shares
            'Next
            'End If
        End If
    End Sub



    Public Function GetAsset(resource As String) As IResource Implements IMarketForce.GetAsset
        Return Assests.GetResource(resource)
    End Function


End Class

Public Interface IMarketForce
    Sub Produce()
    Function GetAsset(resource As String) As IResource
    Sub PerformAction(market As IMarket)
    Sub AddResource(resourceToAdd As IResource)
    Sub RemoveResource(resourceToRemove As IResource)
    Function GetAllAssets() As IList(Of IResource)
End Interface
