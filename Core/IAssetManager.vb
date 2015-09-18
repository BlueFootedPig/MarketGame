Public Interface IAssetManager
    Sub AddAsset(resource As Resource)
    Sub RemoveAsset(resource As Resource)
    Function HasEnough(item As Resource) As Boolean
    Function GetAllAssets() As IList(Of Resource)
    Function GetResource(resource As String) As Resource
End Interface
