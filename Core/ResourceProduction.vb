Public Class ResourceProduction
    Implements IResourceProduction

    Public Property ProducedResource As IResource Implements IResourceProduction.ProducedResource
    Public Property RequiredResources As IList(Of IResource) = New List(Of IResource) Implements IResourceProduction.RequiredResources

    'future properties might include cost to produce

    Sub Produce(Assests As IAssetManager) Implements IResourceProduction.Produce

        If Not hasEnoughToProduce(Assests) Then Exit Sub

        If ProducedResource.GetType() = GetType(LuxuryResource) Then
            Assests.AddAsset(New LuxuryResource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})
        Else
            Assests.AddAsset(New CraftResource() With {.Name = ProducedResource.Name, .Shares = ProducedResource.Shares})
        End If

        For Each item As CraftResource In RequiredResources
            Assests.RemoveAsset(item)

        Next

    End Sub

    Private Function hasEnoughToProduce(Assests As IAssetManager) As Boolean
        Dim returnValue As Boolean = True

        For Each item As IResource In RequiredResources

            returnValue = returnValue AndAlso Assests.HasEnough(item)
        Next

        Return returnValue
    End Function

End Class

Public Interface IResourceProduction
    Property ProducedResource As IResource
    Property RequiredResources As IList(Of IResource)
    Sub Produce(Assests As IAssetManager)
End Interface
