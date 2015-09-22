Public Class AssetManager
    Implements IAssetManager

    Private assests As New List(Of IResource)

    Public Sub AddAsset(resource As IResource) Implements IAssetManager.AddAsset
        Dim foundResource As IResource = assests.FirstOrDefault(Function(n) n.Name = resource.Name)

        If foundResource Is Nothing Then
            assests.Add(resource)
        Else
            foundResource.Shares += resource.Shares
        End If

    End Sub


    Public Sub RemoveAsset(resource As IResource) Implements IAssetManager.RemoveAsset
        Dim foundResource As IResource = assests.FirstOrDefault(Function(n) n.Name = resource.Name)

        If foundResource Is Nothing Then
            Throw New InvalidOperationException("Asset not found")
        Else
            foundResource.Shares -= resource.Shares
            If foundResource.Shares < 0 Then Throw New InvalidOperationException("Not enough resources to perform this action")
        End If
    End Sub

    Public Function HasEnough(item As IResource) As Boolean Implements IAssetManager.HasEnough
        Dim foundResource As IResource = assests.FirstOrDefault(Function(n) n.Name = item.Name)
        If foundResource Is Nothing Then
            Return False
        Else
            Return foundResource.Shares > item.Shares
        End If
        Return False
    End Function

    Function GetAllAssets() As IList(Of IResource) Implements IAssetManager.GetAllAssets
        Return assests
    End Function

    Function GetResource(resource As String) As IResource Implements IAssetManager.GetResource
        Return assests.FirstOrDefault(Function(n) n.Name = resource)
    End Function

End Class

