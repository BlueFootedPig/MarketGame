Imports Core

Public MustInherit Class IPersistMarketForce

    Public MustOverride Sub Save(marketForce As IMarketForce)

    Public MustOverride Function LoadForce(nameOfForce As String, settings As EngineSettings) As IMarketForce

    Public Shared Function Load(nameOfForce As String, persistance As IPersistMarketForce, settings As EngineSettings) As IMarketForce

        Return persistance.LoadForce(nameOfForce, settings)

    End Function

End Class


