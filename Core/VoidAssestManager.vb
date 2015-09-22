Public Class VoidAssestManager

    Public Class VoidAssetManager
        Implements IAssetManager


        Public Sub AddAsset(resource As IResource) Implements IAssetManager.AddAsset

        End Sub

        Public Function GetAllAssets() As IList(Of IResource) Implements IAssetManager.GetAllAssets
            Dim money As New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = Integer.MaxValue}
            Dim returnValue As New List(Of IResource)
            returnValue.Add(money)
            Return returnValue

        End Function

        Public Function GetResource(wantedResource As String) As IResource Implements IAssetManager.GetResource
            If wantedResource = CraftResource.CREDIT Then Return New CraftResource() With {.Name = CraftResource.CREDIT, .Shares = Integer.MaxValue}
            Return Nothing
        End Function

        Public Function HasEnough(item As IResource) As Boolean Implements IAssetManager.HasEnough
            Return True
        End Function

        Public Sub RemoveAsset(resource As IResource) Implements IAssetManager.RemoveAsset

        End Sub
    End Class

End Class
