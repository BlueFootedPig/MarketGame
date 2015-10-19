﻿Imports Core

Public MustInherit Class IPersistMarketForce

    Public MustOverride Sub SaveMarketForce(marketForce As IMarketForce)

    Public MustOverride Function LoadForce(nameOfForce As String, settings As EngineSettings) As IMarketForce

    Public Shared Function LoadMarketForce(nameOfForce As String, persistance As IPersistMarketForce, settings As EngineSettings) As IMarketForce

        Return persistance.LoadForce(nameOfForce, settings)

    End Function

    Public MustOverride Sub SaveGameSettings(settings As EngineSettings)


End Class


