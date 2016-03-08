Public Interface IResourceProduction
    Property ProducedResource As IResource
    Property RequiredResources As IList(Of IResource)
    Property LaborLevel As Integer

    Sub Produce(Assests As IAssetManager)

    Function SaveStateObject() As StateObject

End Interface

Public Class ResourceProduction
    Implements IResourceProduction

    Public Sub New(systemSettings As EngineSettings)
        If systemSettings Is Nothing Then Throw New ArgumentException("systemSettings cannot be null.")

        settings = systemSettings
    End Sub

    Public Property ProducedResource As IResource Implements IResourceProduction.ProducedResource
    Public Property RequiredResources As IList(Of IResource) = New List(Of IResource) Implements IResourceProduction.RequiredResources

    Public Property LaborLevel As Integer = 1 Implements IResourceProduction.LaborLevel
    Private Const LABOR_FACTOR As Double = 0.2
    Private settings As EngineSettings

    Function SaveStateObject() As StateObject Implements IResourceProduction.SaveStateObject
        Dim returnStateObject As New StateObject(Me)
        returnStateObject.SetProperty("LaborLevel", LaborLevel)
        returnStateObject.SetProperty("ProducedResource", ProducedResource.GetStateObject())
        returnStateObject.SetProperty("RequiredResources", RequiredResources.Select(Function(n) n.GetStateObject()).ToList())
        Return returnStateObject
    End Function

    Sub Produce(Assests As IAssetManager) Implements IResourceProduction.Produce

        If Not hasEnoughToProduce(Assests) Then Exit Sub

        Assests.AddAsset(ProducedResource.CopyResource())

        For Each item As CraftResource In RequiredResources
            Assests.RemoveAsset(item)

        Next

        Assests.RemoveAsset(getLaborCosts)

    End Sub

    Private Function hasEnoughToProduce(Assests As IAssetManager) As Boolean
        Dim returnValue As Boolean = True

        For Each item As IResource In RequiredResources

            returnValue = returnValue AndAlso Assests.HasEnough(item)
        Next

        returnValue = returnValue AndAlso Assests.HasEnough(getLaborCosts)

        Return returnValue
    End Function

    Private Function getLaborCosts() As IResource

        Dim minWage As Double = settings.minWage
        Dim cash As IResource = IResource.CREDIT.CopyResource()
        cash.Shares = minWage * (LaborLevel - (LaborLevel * LABOR_FACTOR))
        Return cash

    End Function

End Class

Public Class EngineSettings
    Public minWage As Double = 1

End Class

