Public Interface IAssetManager
    Sub AddAsset(resource As IResource)
    Sub RemoveAsset(resource As IResource)
    Function HasEnough(item As IResource) As Boolean
    Function GetAllAssets() As IList(Of IResource)
    Function GetResource(resource As String) As IResource
End Interface
