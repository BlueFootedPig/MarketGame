Public Class VoidAssestManager

    Public Class VoidAssetManager
        Implements IAssetManager


        Public Sub AddAsset(resource As Resource) Implements IAssetManager.AddAsset

        End Sub

        Public Function GetAllAssets() As IList(Of Resource) Implements IAssetManager.GetAllAssets
            Dim money As New Resource() With {.Name = Resource.CREDIT, .Shares = Integer.MaxValue}
            Dim returnValue As New List(Of Resource)
            returnValue.Add(money)
            Return returnValue

        End Function

        Public Function GetResource(wantedResource As String) As Resource Implements IAssetManager.GetResource
            If wantedResource = Resource.CREDIT Then Return New Resource() With {.Name = Resource.CREDIT, .Shares = Integer.MaxValue}
            Return Nothing
        End Function

        Public Function HasEnough(item As Resource) As Boolean Implements IAssetManager.HasEnough
            Return True
        End Function

        Public Sub RemoveAsset(resource As Resource) Implements IAssetManager.RemoveAsset

        End Sub
    End Class

End Class
